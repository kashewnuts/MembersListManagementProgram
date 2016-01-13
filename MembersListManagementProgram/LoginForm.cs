using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class LoginForm : Form
    {
        // 初期化処理
        public LoginForm()
        {
            InitializeComponent();
        }

        // Login
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (excuteSearch())
            {
                using (MembersListManagementForm f = new MembersListManagementForm(textBox2.Text))
                {
                    // テキストボックスの値をクリア
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();

                    // 管理画面へ遷移
                    this.Hide();
                    f.ShowDialog(this);
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("ログイン情報に誤りがあります。");
            }
        }

        // 検索
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
                // Userクラスに値セット
                row = ds.Tables["M_EMP"].Rows[0];
                User.nm_emp = row["nm_emp"].ToString();

                conn.Close();
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
