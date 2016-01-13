using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class LoginForm : Form
    {
        public string m_strUserName { get; set; }
        private string strUserName = null;

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
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (excuteSearch())
            {
                // メニュー画面表示
                MenuForm f = new MenuForm(textBox2.Text);
                //this.m_strUserName = strUserName;
                f.MdiParent = this.MdiParent;
                f.Show();
            }
            else
            {
                MessageBox.Show("ログイン情報に誤りがあります。");
            }
        }

        public string getUserName()
        {
            return strUserName;
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        private bool excuteSearch()
        {
            bool bResult = false;
            OleDbDataAdapter da;
            DataRow row;
            DataSet ds = new DataSet();
            OleDbConnection conn = new OleDbConnection();
            // 接続文字列を設定して接続する
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                conn.Open();
                string strSql = "SELECT * FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}' AND TXT_PASSWD='{2}' AND FLG_ACTIVE='Y'";
                da = new OleDbDataAdapter(String.Format(strSql, textBox1.Text, textBox2.Text, textBox3.Text), conn);
                da.Fill(ds, "M_EMP");
                bResult = (ds.Tables["M_EMP"].Rows.Count > 0) ? true : false;
                // ユーザー名表示
                row = ds.Tables["M_EMP"].Rows[0];
                strUserName = row["nm_emp"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "通知");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return bResult;
        }
    }
}
