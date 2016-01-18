using System;
using System.Data;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class LoginForm : Form
    {
        // メンバ変数
        private string strUserName;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLlogin_Click(object sender, EventArgs e)
        {
            if (ExcuteSearch())
            {
                // 親フォーム(MDIフォーム)にログインユーザー名をセット
                MainMDI parentForm = (MainMDI)this.MdiParent;
                parentForm.txtUserName.Text = strUserName;
                // メニュー画面表示
                MenuForm f = new MenuForm(txtCd_Emp.Text);
                f.MdiParent = this.MdiParent;
                f.Show();
            }
            else
            {
                MessageBox.Show("ログイン情報に誤りがあります。");
            }
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public bool ExcuteSearch()
        {
            bool bResult = false;
            using (OleDbIf db = new OleDbIf())
            {
                db.Connect();
                string strSql = "SELECT * FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}' AND TXT_PASSWD='{2}' AND FLG_ACTIVE='Y'";
                DataTable tbl = db.ExecuteSql(String.Format(strSql, txtCd_Co.Text, txtCd_Emp.Text, txtTxt_Passwd.Text));
                bResult = (tbl.Rows.Count > 0) ? true : false;
                if (bResult)
                {
                    strUserName = tbl.Rows[0]["nm_emp"].ToString();
                }
            }
            return bResult;
        }
    }
}
