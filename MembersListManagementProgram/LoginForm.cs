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
        /// Load Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 会社コード設定
            GetCd_Co();
            // KeyEvent処理
            this.KeyPress += LoginForm_KeyPress;
        }

        /// <summary>
        /// 会社コード指定
        /// </summary>
        private void GetCd_Co()
        {
            using (var db = new OleDbIf())
            {
                //表示される値はDataTableのNAME列
                cmbCdCo.DisplayMember = "NM_CO_SHORT";
                //対応する値はDataTableのID列
                cmbCdCo.ValueMember = "CD_CO";
                // DB処理
                db.Connect();
                DataTable tbl = db.ExecuteSql("SELECT CD_CO, NM_CO_SHORT FROM M_CO WHERE FLG_ACTIVE='Y'");
                cmbCdCo.DataSource = tbl;
            }
        }

        /// <summary>
        /// KeyEvent処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLlogin_Click(object sender, EventArgs e)
        {
            if (ExcuteSearch())
            {
                // 親フォーム(MDIフォーム)にログインユーザー名をセット
                var parentForm = this.MdiParent as MainMDI;
                parentForm.lblUserName.Text = strUserName;
                // メニュー画面表示
                var f = new MenuForm(txtCd_Emp.Text);
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
            using (var db = new OleDbIf())
            {
                db.Connect();
                string strSql = "SELECT NM_EMP FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}' AND TXT_PASSWD='{2}' AND FLG_ACTIVE='Y'";
                DataTable tbl = db.ExecuteSql(String.Format(strSql, cmbCdCo.SelectedValue.ToString(), txtCd_Emp.Text, txtTxt_Passwd.Text));
                bResult = (tbl.Rows.Count > 0) ? true : false;
                if (bResult)
                {
                    strUserName = tbl.Rows[0]["NM_EMP"].ToString();
                }
            }
            return bResult;
        }
    }
}
