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
        private string editMode { get; set; }
        public DepartmentMasterEditForm(string editMode)
        {
            InitializeComponent();
            this.editMode = editMode;
            this.Text = getFormTitle();
        }

        private void DepartmentMasterInitForm_Load(object sender, EventArgs e)
        {
            // ボタン表示・非表示切り替え
            switchVisibleButton();
            // TODO: 編集、参照時データ取得処理実装
        }

        // タイトル取得
        private string getFormTitle()
        {
            if (this.editMode.Equals(CommonConstants.CREATE_MODE)) return "部門マスタ新規作成画面";
            else if (this.editMode.Equals(CommonConstants.UPDATE_MODE)) return "部門マスタ編集画面";
            else return "部門マスタ参照画面";
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
                this.Controls.Remove(this.deleteButton);
                this.Controls.Remove(this.button4);
                this.textBox1.ReadOnly = true;
                this.textBox1.Enabled = false;
                this.textBox2.ReadOnly = true;
                this.textBox2.Enabled = false;
                this.textBox3.ReadOnly = true;
                this.textBox3.Enabled = false;
                this.textBox4.ReadOnly = true;
                this.textBox4.Enabled = false;
            }
        }

        // 終了
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 登録
        private void button4_Click(object sender, EventArgs e)
        {
            excuteSql(getSqlString());
        }

        // 削除
        private void deleteButton_Click(object sender, EventArgs e)
        {
            excuteSql(String.Format("DELETE FROM M_DEPT WHERE CD_CO='{0}' AND CD_DEPT='{1}' ", "1", "1"));
        }

        private void excuteSql(string sql)
        {
            OleDbConnection cn = new OleDbConnection();
            OleDbCommand com;
            // 接続文字列を設定して接続する
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                cn.Open();
                com = new OleDbCommand(sql, cn);
                com.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("正常に終了しました。", "通知");
                this.Close();
            }
            catch (DbException ex)
            {
                if (ex.ErrorCode == -2147217873)    // ORA-00001: 一意性違反
                {
                    cn.Close();
                    MessageBox.Show("既に登録されています。別のデータを登録してください。", "通知");
                }
                else
                {
                    cn.Close();
                    MessageBox.Show("エラーが発生しました。入力項目を見なおしてください。", "通知");
                }
            }
            catch (Exception ex)
            {
                cn.Close();
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
            string sql = null;
            if (this.editMode.Equals(CommonConstants.CREATE_MODE))
            {
                sql = "INSERT INTO M_DEPT VALUES('{0}', '{1}', '{2}', '{3}', '{4}', SYSDATE, '{5}', SYSDATE, 'Y')";
                sql = String.Format(sql, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, "k_yoshida", "k_yoshida");
            }
            else if (this.editMode.Equals(CommonConstants.UPDATE_MODE))
            {
                sql = "UPDATE M_DEPT SET CD_CO='{0}', CD_DEPT='{1}', NM_DEPT='{2}', TXT_REM='{3}', CD_UPDATE='{4}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{5}' AND CD_DEPT='{6}'";
                sql = String.Format(sql, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, "k_yoshida", "1", "1");
            }
            return sql;
        }
    }
}
