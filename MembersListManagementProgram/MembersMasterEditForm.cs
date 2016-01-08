using System;
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
    public partial class MembersMasterEditForm : Form
    {
        private string editMode { get; set; }
        public MembersMasterEditForm(string editMode)
        {
            InitializeComponent();
            this.editMode = editMode;
            this.Text = getFormTitle();
        }


        private void MembersMasterEditForm_Load(object sender, EventArgs e)
        {
            // ボタン表示・非表示切り替え
            switchVisibleButton();
        }

        // タイトル取得
        private string getFormTitle()
        {
            if (this.editMode.Equals(CommonConstants.CREATE_MODE)) return "社員マスタ新規作成画面";
            else if (this.editMode.Equals(CommonConstants.UPDATE_MODE)) return "社員マスタ編集画面";
            else return "社員マスタ参照画面";
        }

        // ボタン表示・非表示切替
        private void switchVisibleButton()
        {
            if (this.editMode.Equals(CommonConstants.CREATE_MODE))
            {
                this.Controls.Remove(this.deleteButton);
            }
            else if (this.editMode.Equals(CommonConstants.VIEW_MODE))
            {
                // ボタン処理
                this.Controls.Remove(this.registerButton);
                this.Controls.Remove(this.deleteButton);

                // テキストボックス処理
                this.textBox1.ReadOnly = true;
                this.textBox1.Enabled = false;
                this.textBox2.ReadOnly = true;
                this.textBox2.Enabled = false;
                this.textBox3.ReadOnly = true;
                this.textBox3.Enabled = false;
                this.textBox4.ReadOnly = true;
                this.textBox4.Enabled = false;
                this.textBox5.ReadOnly = true;
                this.textBox5.Enabled = false;
                this.textBox6.ReadOnly = true;
                this.textBox6.Enabled = false;
                this.textBox7.ReadOnly = true;
                this.textBox7.Enabled = false;
                this.textBox8.ReadOnly = true;
                this.textBox8.Enabled = false;
                this.textBox9.ReadOnly = true;
                this.textBox9.Enabled = false;
                this.textBox10.ReadOnly = true;
                this.textBox10.Enabled = false;
                this.textBox11.ReadOnly = true;
                this.textBox11.Enabled = false;
                this.textBox12.ReadOnly = true;
                this.textBox12.Enabled = false;
            }
        }

        // 新規登録
        private void registerButton_Click(object sender, EventArgs e)
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
                cn.Close();
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
            string sql = "INSERT INTO M_EMP VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', SYSDATE, '{13}', SYSDATE, 'Y')";
            sql = String.Format(
                sql, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text,
                textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, "k_yoshida", "k_yoshida");
            return sql;
        }

        // 削除
        private void deleteButton_Click(object sender, EventArgs e)
        {

        }

        // 終了
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
