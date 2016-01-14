using System;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;


namespace MembersListManagementProgram
{
    public partial class DepartmentMasterEditForm : Form
    {
        /// <summary>
        /// プロパティ
        /// </summary>
        private string m_strEditMode { get; set; }
        private string m_strPrimaryKey1 { get; set; }
        private string m_strPrimaryKey2 { get; set; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="strEditMode"></param>
        /// <param name="args"></param>
        public DepartmentMasterEditForm(string strEditMode, params string[] args)
        {
            InitializeComponent();
            this.m_strEditMode = strEditMode;
            this.Text = getFormTitle();
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
        private void DepartmentMasterInitForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // ボタン表示・非表示切り替え
            switchVisibleButton();
            // 編集、参照ボタン押下時時データ取得
            if (!this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) excuteSearch();
        }

        /// <summary>
        /// タイトル取得
        /// </summary>
        /// <returns></returns>
        private string getFormTitle()
        {
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) return "部門マスタ新規作成画面";
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE)) return "部門マスタ編集画面";
            else return "部門マスタ参照画面";
        }

        /// <summary>
        /// ボタン表示・非表示切替
        /// </summary>
        private void switchVisibleButton()
        {
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE))
            {
                this.Controls.Remove(this.btnDelete);
            }
            else if (this.m_strEditMode.Equals(CommonConstants.VIEW_MODE))
            {
                this.Controls.Remove(this.btnDelete);
                this.Controls.Remove(this.btnRegister);
                this.txtCd_Co.ReadOnly = true;
                this.txtCd_Co.Enabled = false;
                this.txtCd_Dept.ReadOnly = true;
                this.txtCd_Dept.Enabled = false;
                this.txtNm_Dept.ReadOnly = true;
                this.txtNm_Dept.Enabled = false;
                this.txtTxt_Rem.ReadOnly = true;
                this.txtTxt_Rem.Enabled = false;
            }
        }

        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            excuteSql(getSqlString());
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            excuteSql(String.Format("UPDATE M_DEPT SET DTM_UPDATE=SYSDATE, FLG_ACTIVE='N' WHERE CD_CO='{0}' AND CD_DEPT='{1}'", m_strPrimaryKey1, m_strPrimaryKey2));
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canselButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 検索
        /// </summary>
        private void excuteSearch()
        {
            OleDbDataReader dr;
            OleDbCommand cmd;
            OleDbConnection conn = new OleDbConnection();
            // 接続文字列を設定して接続する
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                conn.Open();
                string strSql = "SELECT CD_CO, CD_DEPT, NM_DEPT, TXT_REM FROM M_DEPT WHERE CD_CO='{0}' AND CD_DEPT='{1}'";
                cmd = new OleDbCommand(String.Format(strSql, this.m_strPrimaryKey1, this.m_strPrimaryKey2), conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    this.txtCd_Co.Text = dr.GetString(0);
                    this.txtCd_Dept.Text = dr.GetString(1);
                    this.txtNm_Dept.Text = dr.GetString(2);
                    this.txtTxt_Rem.Text = dr.IsDBNull(3) ? null : dr.GetString(3);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "通知");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="strSql"></param>
        private void excuteSql(string strSql)
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd;
            // 接続文字列を設定して接続する
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                conn.Open();
                cmd = new OleDbCommand(strSql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("正常に処理を完了しました。", "通知");
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            catch (DbException ex)
            {
                if (ex.ErrorCode == -2147217873)    // ORA-00001: 一意性違反
                {
                    MessageBox.Show("既に登録されています。別のデータを登録してください。", "通知");
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。入力項目を見なおしてください。", "通知");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "通知");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        /// <summary>
        /// SQL文取得
        /// </summary>
        /// <returns></returns>
        private string getSqlString()
        {
            string strSql = null;
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE))
            {
                strSql = "INSERT INTO M_DEPT VALUES('{0}', '{1}', '{2}', '{3}', '{4}', SYSDATE, '{5}', SYSDATE, 'Y')";
                strSql = String.Format(strSql, txtCd_Co.Text, txtCd_Dept.Text, txtNm_Dept.Text, txtTxt_Rem.Text, "k_yoshida", "k_yoshida");
            }
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE))
            {
                strSql = "UPDATE M_DEPT SET CD_CO='{0}', CD_DEPT='{1}', NM_DEPT='{2}', TXT_REM='{3}', CD_UPDATE='{4}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{5}' AND CD_DEPT='{6}'";
                strSql = String.Format(strSql, txtCd_Co.Text, txtCd_Dept.Text, txtNm_Dept.Text, txtTxt_Rem.Text, "k_yoshida", m_strPrimaryKey1, m_strPrimaryKey2);
            }
            return strSql;
        }
    }
}
