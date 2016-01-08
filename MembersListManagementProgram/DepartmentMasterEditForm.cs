using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;

namespace MembersListManagementProgram
{
    public partial class DepartmentMasterEditForm : Form
    {
        public DepartmentMasterEditForm()
        {
            InitializeComponent();
        }

        private void DepartmentMasterInitForm_Load(object sender, EventArgs e)
        {
        }

        // 終了
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 新規登録
        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection cn = new OleDbConnection();
            OleDbCommand com;
            // 接続文字列を設定して接続する
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                cn.Open();
                com = new OleDbCommand(getSqlString(), cn);
                com.ExecuteNonQuery();
                MessageBox.Show("追加しました。", "通知");
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
                if (cn != null) cn.Close();
            }
        }

        // SQL取得
        private string getSqlString()
        {
            string sql = "INSERT INTO M_DEPT VALUES('{0}', '{1}', '{2}', '{3}', '{4}', SYSDATE, '{5}', SYSDATE, 'Y')";
            sql = String.Format(sql, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, "k_yoshida", "k_yoshida");
            return sql;
        }
    }
}
