using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        private void MembersMasterEditForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            // ボタン表示・非表示切り替え
            SwitchVisibleButton();
            // 編集、参照ボタン押下時時データ取得
            if (!this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) ExcuteSearch();
        }

        /// <summary>
        /// タイトル取得
        /// </summary>
        /// <returns></returns>
        private string GetFormTitle()
        {
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) return "社員マスタ新規作成画面";
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE)) return "社員マスタ編集画面";
            else return "社員マスタ参照画面";
        }

        /// <summary>
        /// ボタン表示・非表示切替
        /// </summary>
        private void SwitchVisibleButton()
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
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            ExcuteSql(GetSqlString());
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            MainMDI parentForm = (MainMDI)this.MdiParent;
            ExcuteSql(
                String.Format("UPDATE M_EMP SET SET CD_UPDATE='{0}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='N' WHERE CD_CO='{1}' AND CD_EMP='{2}'",
                parentForm.txtUserName.Text, m_strPrimaryKey1, m_strPrimaryKey2));
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 検索
        /// </summary>
        private void ExcuteSearch()
        {
            OleDbIf db = new OleDbIf();
            try
            {
                db.Connect();
                string strSql = "SELECT CD_CO, CD_EMP, NM_EMP, TXT_PASSWD, CD_DEPT, TXT_ZIP, TXT_ADDR1, TXT_ADDR2, TXT_ADDR3, TXT_TEL, TXT_FAX, TXT_REM FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}'";
                DataTable tbl = db.ExecuteSql(String.Format(strSql, this.m_strPrimaryKey1, this.m_strPrimaryKey2));

                int i = 0;
                this.txtCd_Co.Text = tbl.Rows[0][i++].ToString();
                this.txtCd_Emp.Text = tbl.Rows[0][i++].ToString();
                this.txtNm_Emp.Text = tbl.Rows[0][i++].ToString();
                this.txtTxt_Passwd.Text = tbl.Rows[0][i++].ToString();
                this.txtCd_Dept.Text = tbl.Rows[0][i++].ToString();
                this.txtTxt_Zip.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
                this.txtTxt_Addr1.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
                this.txtTxt_Addr2.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
                this.txtTxt_Addr3.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
                this.txtTxt_Tel.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
                this.txtTxt_Fax.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
                this.txtTxt_Rem.Text = (tbl.Rows[0][i] == null) ? null : tbl.Rows[0][i++].ToString();
            }
            finally
            {
                db.Disconnect();
            }
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="strSql"></param>
        private void ExcuteSql(string strSql)
        {
            OleDbIf db = new OleDbIf();
            try
            {
                db.Connect();
                db.ExecuteSql(strSql);
                this.Close();
            }
            finally
            {
                db.Disconnect();
            }
        }

        /// <summary>
        /// SQL文取得
        /// </summary>
        /// <returns></returns>
        private string GetSqlString()
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
