﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class LoginForm : Form
    {
        // メンバ変数
        private bool flgLogin = false;

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
            bool result = false;
            OleDbDataAdapter dAdp;
            DataRow dRow;
            DataSet dSet = new DataSet();
            OleDbConnection cn = new OleDbConnection();
            // 接続文字列を設定して接続する
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                cn.Open();
                string sql = "SELECT * FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}' AND TXT_PASSWD='{2}' AND FLG_ACTIVE='Y'";
                dAdp = new OleDbDataAdapter(String.Format(sql, textBox1.Text, textBox2.Text, textBox3.Text), cn);
                dAdp.Fill(dSet, "M_EMP");
                result = (dSet.Tables["M_EMP"].Rows.Count > 0) ? true : false;
                // Userクラスに値セット
                dRow = dSet.Tables["M_EMP"].Rows[0];
                User.nm_emp = dRow["nm_emp"].ToString();

                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "通知");
            }
            finally
            {
                if (cn != null) cn.Close();
            }
            return result;
        }
    }
}
