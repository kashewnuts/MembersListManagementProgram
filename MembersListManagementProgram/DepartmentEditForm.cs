﻿using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MembersListManagementProgram
{
	public partial class DepartmentEditForm : Form
	{
		/// <summary>
		/// プロパティ
		/// </summary>
		// 編集モード(1: 新規作成, 2: 更新, 3: 参照)
		private string m_strEditMode { get; set; }
		// 全画面より取得する主キー値
		private string m_strPrimaryKey1 { get; set; }
		private string m_strPrimaryKey2 { get; set; }
		// 部門コード一覧画面で部門変更時用
		public string m_strCd_Dept { get; set; }
		public string m_strNm_Dept { get; set; }

		/// <summary>
		/// 初期化処理
		/// </summary>
		/// <param name="strEditMode"></param>
		/// <param name="args"></param>
		public DepartmentEditForm(string strEditMode, params string[] args)
		{
			InitializeComponent();
			this.m_strEditMode = strEditMode;
			this.Text = GetFormTitle();
			if (args.Count() == 2)
			{
				this.m_strPrimaryKey1 = args[0];
				this.m_strPrimaryKey2 = args[1];
			}
		}

		/// <summary>
		/// Load Event Handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DepartmentEditForm_Load(object sender, EventArgs e)
		{
			// 画面サイズ指定
			this.WindowState = FormWindowState.Maximized;
			// ボタン表示・非表示切り替え
			SwitchVisibleButton();
			// 会社コード取得
			cmbCdCo.DataSource = GetCdCo();
			// 編集、参照ボタン押下時時データ取得
			if (!this.m_strEditMode.Equals(CommonConstants.EditMode.CREATE_MODE)) ExcuteSearch();
			// 部門コード変更時部門名取得
			txtCd_Dept.LostFocus += txtCd_Dept_LostFocus;
			// KeyEvent処理
			this.KeyPress += DepartmentEditForm_KeyPress;
		}

		/// <summary>
		/// タイトル取得
		/// </summary>
		/// <returns></returns>
		private string GetFormTitle()
		{
			if (this.m_strEditMode.Equals(CommonConstants.EditMode.CREATE_MODE)) return "部門マスタ新規作成画面";
			else if (this.m_strEditMode.Equals(CommonConstants.EditMode.UPDATE_MODE)) return "部門マスタ編集画面";
			else return "部門マスタ参照画面";
		}

		/// <summary>
		/// ボタン表示・非表示切替
		/// </summary>
		private void SwitchVisibleButton()
		{
			if (this.m_strEditMode.Equals(CommonConstants.EditMode.CREATE_MODE))
			{
				this.Controls.Remove(this.btnDelete);
			}
			else if (this.m_strEditMode.Equals(CommonConstants.EditMode.VIEW_MODE))
			{
				this.Controls.Remove(this.btnDelete);
				this.Controls.Remove(this.btnRegister);
				this.cmbCdCo.Enabled = false;
				this.txtCd_Dept.ReadOnly = true;
				this.txtCd_Dept.Enabled = false;
				this.txtNm_Dept.ReadOnly = true;
				this.txtNm_Dept.Enabled = false;
				this.txtNm_Dept.ReadOnly = true;
				this.txtNm_Dept.Enabled = false;
				this.txtTxt_Rem.ReadOnly = true;
				this.txtTxt_Rem.Enabled = false;
			}
		}

		/// <summary>
		/// 会社コード取得
		/// </summary>
		private DataTable GetCdCo()
		{
			// 会社コード設定
			using (var db = new OleDbIf())
			{
				//表示される値はDataTableのNAME列
				cmbCdCo.DisplayMember = "NM_CO_SHORT";
				//対応する値はDataTableのID列
				cmbCdCo.ValueMember = "CD_CO";
				// DB処理
				db.Connect();
				DataTable tbl = db.ExecuteSql("SELECT CD_CO, NM_CO_SHORT FROM M_CO WHERE FLG_ACTIVE='Y'");
				return tbl;
			}
		}

		/// <summary>
		/// 登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRegister_Click(object sender, EventArgs e)
		{
			DataTable tbl = new DataTable();
			using (var db = new OleDbIf())
			{
				var sb = new StringBuilder();
				sb.AppendLine("SELECT COUNT(*) FROM M_DEPT WHERE CD_CO='{0}' AND CD_DEPT='{1}' AND FLG_ACTIVE='Y'");
				string strSql = sb.ToString();
				db.Connect();
				tbl = db.ExecuteSql(String.Format(strSql, this.cmbCdCo.SelectedValue, this.txtCd_Dept.Text));
			}
			// 一意性エラーチェック
			if (this.m_strEditMode.Equals(CommonConstants.EditMode.CREATE_MODE) && tbl.Rows.Count > 0)
			{
				MessageBox.Show("既に登録されています。", "通知");
			}
			else
			{
				if (DialogResult.Yes == MessageBox.Show("登録します。よろしいですか？", "通知", MessageBoxButtons.YesNo))
				{
					ExcuteSql(GetSqlString());
				}
			}
		}

		/// <summary>
		/// 削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == MessageBox.Show("削除します。よろしいですか？", "通知", MessageBoxButtons.YesNo))
			{
				var parentForm = this.MdiParent as MainMDI;
				ExcuteSql(
					String.Format("UPDATE M_DEPT SET CD_UPDATE='{0}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='N' WHERE CD_CO='{1}' AND CD_DEPT='{2}'",
					parentForm.lblUserName.Text, m_strPrimaryKey1, m_strPrimaryKey2));
			}
		}

		/// <summary>
		/// 終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == MessageBox.Show("終了しますか？", "通知", MessageBoxButtons.YesNo))
			{
				this.Close();
			}
		}

		/// <summary>
		/// 検索
		/// </summary>
		private void ExcuteSearch()
		{
			using (var db = new OleDbIf())
			{
				db.Connect();
				string strSql = "SELECT CD_CO, CD_DEPT, NM_DEPT, TXT_REM FROM M_DEPT WHERE CD_CO='{0}' AND CD_DEPT='{1}' AND FLG_ACTIVE='Y'";
				DataTable tbl = db.ExecuteSql(String.Format(strSql, this.m_strPrimaryKey1, this.m_strPrimaryKey2));

				int i = 0;
				this.cmbCdCo.SelectedValue = tbl.Rows[0][i++].ToString();
				this.txtCd_Dept.Text = tbl.Rows[0][i++].ToString();
				this.txtNm_Dept.Text = tbl.Rows[0][i++].ToString();
				this.txtTxt_Rem.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
			}
		}

		/// <summary>
		/// SQL実行
		/// </summary>
		/// <param name="strSql"></param>
		private void ExcuteSql(string strSql)
		{
			var db = new OleDbIf();
			try
			{
				db.Connect();
				db.BeginTransaction();
				db.ExecuteSql(strSql);
				db.CommitTransaction();
				MessageBox.Show("処理が完了しました。", "通知");
			}
			catch (Exception)
			{
				db.RollbackTransaction();
				MessageBox.Show("登録に失敗しました。", "通知");
			}
			finally
			{
				this.Close();
				db.Dispose();
			}
		}

		/// <summary>
		/// SQL文取得
		/// </summary>
		/// <returns></returns>
		private string GetSqlString()
		{
			string strSql = null;
			var f = this.MdiParent as MainMDI;
			if (this.m_strEditMode.Equals(CommonConstants.EditMode.CREATE_MODE))
			{
				strSql = "INSERT INTO M_DEPT VALUES('{0}', '{1}', '{2}', '{3}', '{4}', SYSDATE, '{5}', SYSDATE, 'Y')";
				strSql = String.Format(strSql, cmbCdCo.SelectedValue, txtCd_Dept.Text, txtNm_Dept.Text, txtTxt_Rem.Text, f.lblUserName.Text, f.lblUserName.Text);
			}
			else if (this.m_strEditMode.Equals(CommonConstants.EditMode.UPDATE_MODE))
			{
				strSql = "UPDATE M_DEPT SET CD_CO='{0}', CD_DEPT='{1}', NM_DEPT='{2}', TXT_REM='{3}', CD_UPDATE='{4}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{5}' AND CD_DEPT='{6}'";
				strSql = String.Format(strSql, cmbCdCo.SelectedValue, txtCd_Dept.Text, txtNm_Dept.Text, txtTxt_Rem.Text, f.lblUserName.Text, m_strPrimaryKey1, m_strPrimaryKey2);
			}
			return strSql;
		}

		/// <summary>
		/// 部門コード変更時部門名取得
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtCd_Dept_LostFocus(object sender, EventArgs e)
		{
			if ("".Equals(txtCd_Dept.Text))
			{
				this.txtNm_Dept.Text = null;
			}
			else
			{
				// TODO: 毎回SQLを投げるとネットワーク的によろしくないので、どこかに値を保持しておきたい。
				using (var db = new OleDbIf())
				{
					db.Connect();
					string strSql = "SELECT NM_DEPT FROM M_DEPT WHERE CD_CO='{0}' AND CD_DEPT='{1}' AND FLG_ACTIVE='Y'";
					DataTable tbl = db.ExecuteSql(String.Format(strSql, cmbCdCo.SelectedValue, txtCd_Dept.Text));
					this.txtNm_Dept.Text = (tbl.Rows.Count > 0) ? tbl.Rows[0]["NM_DEPT"].ToString() : null;
				}

			}
		}

		/// <summary>
		/// KeyEvent処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DepartmentEditForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Escape
				&& DialogResult.Yes == MessageBox.Show("終了しますか？", "通知", MessageBoxButtons.YesNo))
			{
				this.Close();
			}
		}
	}
}
