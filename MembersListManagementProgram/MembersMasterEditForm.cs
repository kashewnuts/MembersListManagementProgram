using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class MembersMasterEditForm : Form
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
        public MembersMasterEditForm(string strEditMode, params string[] args)
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
        private void MembersMasterEditForm_Load(object sender, EventArgs e)
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
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) return "社員マスタ新規作成画面";
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE)) return "社員マスタ編集画面";
            else return "社員マスタ参照画面";
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
                // ボタン処理
                this.Controls.Remove(this.btnRegister);
                this.Controls.Remove(this.btnDelete);

                // テキストボックス処理
                this.txtCd_Co.ReadOnly = true;
                this.txtCd_Co.Enabled = false;
                this.txtCd_Emp.ReadOnly = true;
                this.txtCd_Emp.Enabled = false;
                this.txtNm_Emp.ReadOnly = true;
                this.txtNm_Emp.Enabled = false;
                this.txtTxt_Passwd.ReadOnly = true;
                this.txtTxt_Passwd.Enabled = false;
                this.txtCd_Dept.ReadOnly = true;
                this.txtCd_Dept.Enabled = false;
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
            excuteSql(String.Format("UPDATE M_EMP SET DTM_UPDATE=SYSDATE, FLG_ACTIVE='N' WHERE CD_CO='{0}' AND CD_EMP='{1}'", m_strPrimaryKey1, m_strPrimaryKey2));
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 検索
        /// </summary>
        private void excuteSearch()
        {
        //    SqlDbIf db = new SqlDbIf();
        //    db.connect();
        //    string strSql = "SELECT CD_CO, CD_EMP, NM_EMP, TXT_PASSWD, CD_DEPT, TXT_ZIP, TXT_ADDR1, TXT_ADDR2, TXT_ADDR3, TXT_TEL, TXT_FAX, TXT_REM FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}'";
        //    List<object> records = db.searchSql(String.Format(strSql, m_strPrimaryKey1, m_strPrimaryKey2));

        //    foreach (List<string> r in records)
        //    {
        //        this.txtCd_Co.Text = "";
        //        //this.txtCd_Co.Text = r.ToArray.GetString(0);
        //        //this.txtCd_Emp.Text = r.GetString(1);
        //        //this.txtNm_Emp.Text = r.GetString(2);
        //        //this.txtTxt_Passwd.Text = dr.GetString(3);
        //        //this.txtCd_Dept.Text = dr.GetString(4);
        //        //this.txtTxt_Zip.Text = dr.IsDBNull(5) ? null : dr.GetString(5);
        //        //this.txtTxt_Addr1.Text = dr.IsDBNull(6) ? null : dr.GetString(6);
        //        //this.txtTxt_Addr2.Text = dr.IsDBNull(7) ? null : dr.GetString(7);
        //        //this.txtTxt_Addr3.Text = dr.IsDBNull(8) ? null : dr.GetString(8);
        //        //this.txtTxt_Tel.Text = dr.IsDBNull(9) ? null : dr.GetString(9);
        //        //this.txtTxt_Fax.Text = dr.IsDBNull(10) ? null : dr.GetString(10);
        //        //this.txtTxt_Rem.Text = dr.IsDBNull(11) ? null : dr.GetString(11);
        //    }
        //}

            OleDbDataReader dr;
            OleDbCommand cmd;
            OleDbConnection conn = new OleDbConnection();
            // 接続文字列を設定して接続する
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                conn.Open();
                string strSql = "SELECT CD_CO, CD_EMP, NM_EMP, TXT_PASSWD, CD_DEPT, TXT_ZIP, TXT_ADDR1, TXT_ADDR2, TXT_ADDR3, TXT_TEL, TXT_FAX, TXT_REM FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}'";
                cmd = new OleDbCommand(String.Format(strSql, m_strPrimaryKey1, m_strPrimaryKey2), conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    this.txtCd_Co.Text = dr.GetString(0);
                    this.txtCd_Emp.Text = dr.GetString(1);
                    this.txtNm_Emp.Text = dr.GetString(2);
                    this.txtTxt_Passwd.Text = dr.GetString(3);
                    this.txtCd_Dept.Text = dr.GetString(4);
                    this.txtTxt_Zip.Text = dr.IsDBNull(5) ? null : dr.GetString(5);
                    this.txtTxt_Addr1.Text = dr.IsDBNull(6) ? null : dr.GetString(6);
                    this.txtTxt_Addr2.Text = dr.IsDBNull(7) ? null : dr.GetString(7);
                    this.txtTxt_Addr3.Text = dr.IsDBNull(8) ? null : dr.GetString(8);
                    this.txtTxt_Tel.Text = dr.IsDBNull(9) ? null : dr.GetString(9);
                    this.txtTxt_Fax.Text = dr.IsDBNull(10) ? null : dr.GetString(10);
                    this.txtTxt_Rem.Text = dr.IsDBNull(11) ? null : dr.GetString(11);
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
                MessageBox.Show("正常に処理を完了しました", "通知");
                this.Close();
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
            MainMDI f = (MainMDI)this.MdiParent;
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE))
            {
                strSql = "INSERT INTO M_EMP VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', SYSDATE, '{13}', SYSDATE, 'Y')";
                strSql = String.Format(
                    strSql, txtCd_Co.Text, txtCd_Emp.Text, txtNm_Emp.Text, txtTxt_Passwd.Text, txtCd_Dept.Text, txtTxt_Zip.Text,
                    txtTxt_Addr1.Text, txtTxt_Addr2.Text, txtTxt_Addr3.Text, txtTxt_Tel.Text, txtTxt_Fax.Text, txtTxt_Rem.Text, f.txtUserName.Text, f.txtUserName.Text);
            }
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE))
            {
                strSql = "UPDATE M_EMP SET CD_CO='{0}', CD_EMP='{1}', NM_EMP='{2}', TXT_PASSWD='{3}', CD_DEPT='{4}', TXT_ZIP='{5}', TXT_ADDR1='{6}', TXT_ADDR2='{7}', TXT_ADDR3='{8}', TXT_TEL='{9}', TXT_FAX='{10}', TXT_REM='{11}', CD_UPDATE='{12}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{13}' AND CD_EMP='{14}'";
                strSql = String.Format(
                    strSql, txtCd_Co.Text, txtCd_Emp.Text, txtNm_Emp.Text, txtTxt_Passwd.Text, txtCd_Dept.Text, txtTxt_Zip.Text,
                    txtTxt_Addr1.Text, txtTxt_Addr2.Text, txtTxt_Addr3.Text, txtTxt_Tel.Text, txtTxt_Fax.Text, txtTxt_Rem.Text, f.txtUserName.Text, m_strPrimaryKey1, m_strPrimaryKey2);
            }
            return strSql;
        }
    }
}
