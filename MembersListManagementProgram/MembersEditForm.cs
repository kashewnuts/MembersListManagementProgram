using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
	public partial class MembersEditForm : Form
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
		public MembersEditForm(string strEditMode, params string[] args)
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
		private void MembersEditForm_Load(object sender, EventArgs e)
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
			// 部門コード一覧画面で部門変更時、値反映
			this.Activated += MembersEditForm_Activated;
			// KeyEvent処理
			this.KeyPress += MembersEditForm_KeyPress;

		}

		/// <summary>
		/// タイトル取得
		/// </summary>
		/// <returns></returns>
		private string GetFormTitle()
		{
			if (this.m_strEditMode.Equals(CommonConstants.EditMode.CREATE_MODE)) return "社員マスタ新規作成画面";
			else if (this.m_strEditMode.Equals(CommonConstants.EditMode.UPDATE_MODE)) return "社員マスタ編集画面";
			else return "社員マスタ参照画面";
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
				// ボタン処理
				this.Controls.Remove(this.btnRegister);
				this.Controls.Remove(this.btnDelete);

				// テキストボックス処理
				this.cmbCdCo.Enabled = false;
				this.txtCd_Emp.ReadOnly = true;
				this.txtCd_Emp.Enabled = false;
				this.txtNm_Emp.ReadOnly = true;
				this.txtNm_Emp.Enabled = false;
				this.txtTxt_Passwd.ReadOnly = true;
				this.txtTxt_Passwd.Enabled = false;
				this.txtCd_Dept.ReadOnly = true;
				this.txtCd_Dept.Enabled = false;
				this.txtNm_Dept.ReadOnly = true;
				this.txtNm_Dept.Enabled = false;
				this.btnDept.Enabled = false;
				this.txtTxt_Zip.ReadOnly = true;
				this.txtTxt_Zip.Enabled = false;
				this.txtTxt_Addr1.ReadOnly = true;
				this.txtTxt_Addr1.Enabled = false;
				this.txtTxt_Addr2.ReadOnly = true;
				this.txtTxt_Addr2.Enabled = false;
				this.txtTxt_Addr3.ReadOnly = true;
				this.txtTxt_Addr3.Enabled = false;
				this.txtTxt_Tel.ReadOnly = true;
				this.txtTxt_Tel.Enabled = false;
				this.txtTxt_Fax.ReadOnly = true;
				this.txtTxt_Fax.Enabled = false;
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
				db.Connect();
				var sb = new StringBuilder();
				sb.AppendLine("SELECT COUNT(*) FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}' AND FLG_ACTIVE='Y'");
				string strSql = sb.ToString();
				tbl = db.ExecuteSql(String.Format(strSql, this.cmbCdCo.SelectedValue, this.txtCd_Emp.Text));
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
				MainMDI parentForm = (MainMDI)this.MdiParent;
				ExcuteSql(
					String.Format("UPDATE M_EMP SET CD_UPDATE='{0}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='N' WHERE CD_CO='{1}' AND CD_EMP='{2}'",
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
				var sb = new StringBuilder();
				sb.AppendLine("SELECT ME.CD_CO, ME.CD_EMP, ME.NM_EMP, ME.TXT_PASSWD, ME.CD_DEPT, MD.NM_DEPT, ME.TXT_ZIP, ME.TXT_ADDR1, ME.TXT_ADDR2, ME.TXT_ADDR3, ME.TXT_TEL, ME.TXT_FAX, ME.TXT_REM ");
				sb.AppendLine("  FROM M_EMP ME");
				sb.AppendLine(" INNER JOIN M_DEPT MD");
				sb.AppendLine("	ON ME.CD_CO = MD.CD_CO AND ME.CD_DEPT = MD.CD_DEPT AND ME.FLG_ACTIVE = MD.FLG_ACTIVE");
				sb.AppendLine(" WHERE ME.CD_CO='{0}' AND ME.CD_EMP='{1}' AND ME.FLG_ACTIVE='Y'");
				string strSql = sb.ToString();
				DataTable tbl = db.ExecuteSql(String.Format(strSql, this.m_strPrimaryKey1, this.m_strPrimaryKey2));

				int i = 0;
				this.cmbCdCo.SelectedValue = tbl.Rows[0][i++].ToString();
				this.txtCd_Emp.Text = tbl.Rows[0][i++].ToString();
				this.txtNm_Emp.Text = tbl.Rows[0][i++].ToString();
				this.txtTxt_Passwd.Text = tbl.Rows[0][i++].ToString();
				this.txtCd_Dept.Text = tbl.Rows[0][i++].ToString();
				this.txtNm_Dept.Text = tbl.Rows[0][i++].ToString();
				this.txtTxt_Zip.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
				this.txtTxt_Addr1.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
				this.txtTxt_Addr2.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
				this.txtTxt_Addr3.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
				this.txtTxt_Tel.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
				this.txtTxt_Fax.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
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
				MessageBox.Show("処理に失敗しました。", "通知");
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
				strSql = "INSERT INTO M_EMP VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', SYSDATE, '{13}', SYSDATE, 'Y')";
				strSql = String.Format(
					strSql, cmbCdCo.SelectedValue, txtCd_Emp.Text, txtNm_Emp.Text, txtTxt_Passwd.Text, txtCd_Dept.Text, txtTxt_Zip.Text,
					txtTxt_Addr1.Text, txtTxt_Addr2.Text, txtTxt_Addr3.Text, txtTxt_Tel.Text, txtTxt_Fax.Text, txtTxt_Rem.Text, f.lblUserName.Text, f.lblUserName.Text);
			}
			else if (this.m_strEditMode.Equals(CommonConstants.EditMode.UPDATE_MODE))
			{
				strSql = "UPDATE M_EMP SET CD_CO='{0}', CD_EMP='{1}', NM_EMP='{2}', TXT_PASSWD='{3}', CD_DEPT='{4}', TXT_ZIP='{5}', TXT_ADDR1='{6}', TXT_ADDR2='{7}', TXT_ADDR3='{8}', TXT_TEL='{9}', TXT_FAX='{10}', TXT_REM='{11}', CD_UPDATE='{12}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{13}' AND CD_EMP='{14}'";
				strSql = String.Format(
					strSql, cmbCdCo.SelectedValue, txtCd_Emp.Text, txtNm_Emp.Text, txtTxt_Passwd.Text, txtCd_Dept.Text, txtTxt_Zip.Text,
					txtTxt_Addr1.Text, txtTxt_Addr2.Text, txtTxt_Addr3.Text, txtTxt_Tel.Text, txtTxt_Fax.Text, txtTxt_Rem.Text, f.lblUserName.Text, m_strPrimaryKey1, m_strPrimaryKey2);
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
					var sb = new StringBuilder();
					sb.AppendLine("SELECT MD.NM_DEPT FROM M_EMP ME");
					sb.AppendLine(" INNER JOIN M_DEPT MD ");
					sb.AppendLine("	ON ME.CD_CO = MD.CD_CO AND ME.CD_DEPT = MD.CD_DEPT AND ME.FLG_ACTIVE = MD.FLG_ACTIVE");
					sb.AppendLine(" WHERE ME.CD_CO='{0}' AND ME.CD_EMP='{1}' AND ME.FLG_ACTIVE='Y'");
					string strSql = sb.ToString();
					DataTable tbl = db.ExecuteSql(String.Format(strSql, cmbCdCo.SelectedValue, txtCd_Emp.Text));
					this.txtNm_Dept.Text = (tbl.Rows.Count > 0) ? tbl.Rows[0]["NM_DEPT"].ToString() : null;
				}

			}
		}

		/// <summary>
		/// 部門コード一覧画面で部門変更時、値反映
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MembersEditForm_Activated(object sender, EventArgs e)
		{
			if (m_strCd_Dept != null && !"".Equals(m_strCd_Dept)) txtCd_Dept.Text = m_strCd_Dept;
			if (m_strNm_Dept != null && !"".Equals(m_strNm_Dept)) txtNm_Dept.Text = m_strNm_Dept;
		}

		/// <summary>
		/// KeyEvent処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MembersEditForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Escape
				&& DialogResult.Yes == MessageBox.Show("終了しますか？", "通知", MessageBoxButtons.YesNo))
			{
				this.Close();
			}
		}

		/// <summary>
		/// 部門一覧画面表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDept_Click(object sender, EventArgs e)
		{
			using (var f = new DepartmentSelectForm(this.cmbCdCo.SelectedValue.ToString(), this))
			{
				f.ShowDialog(this);
			}
			txtCd_Dept.Text = m_strCd_Dept;
			txtNm_Dept.Text = m_strNm_Dept;
		}
	}
}
