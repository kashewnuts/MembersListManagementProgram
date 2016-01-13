using System;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;


namespace MembersListManagementProgram
{
    public partial class DepartmentMasterEditForm : Form
    {
        // プロパティ
        private string m_strEditMode { get; set; }
        private string m_strPrimaryKey1 { get; set; }
        private string m_strPrimaryKey2 { get; set; }

        // 初期化処理
        public DepartmentMasterEditForm(string strEditMode, params string[] args)
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

        // Load Event Handler
        private void DepartmentMasterInitForm_Load(object sender, EventArgs e)
        {
            // ボタン表示・非表示切り替え
            switchVisibleButton();
            // 編集、参照ボタン押下時時データ取得
            if (!this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) excuteSearch();
        }

        // タイトル取得
        private string getFormTitle()
        {
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) return "部門マスタ新規作成画面";
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE)) return "部門マスタ編集画面";
            else return "部門マスタ参照画面";
        }

        // ボタン表示・非表示切替
        private void switchVisibleButton()
        {
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE))
            {
                this.Controls.Remove(this.deleteButton);
            }
            else if (this.m_strEditMode.Equals(CommonConstants.VIEW_MODE))
            {
                this.Controls.Remove(this.deleteButton);
                this.Controls.Remove(this.registerButton);
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

        // 登録
        private void registerButton_Click(object sender, EventArgs e)
        {
            excuteSql(getSqlString());
        }

        // 削除
        private void deleteButton_Click(object sender, EventArgs e)
        {
            excuteSql(String.Format("UPDATE M_DEPT SET DTM_UPDATE=SYSDATE, FLG_ACTIVE='N' WHERE CD_CO='{0}' AND CD_DEPT='{1}'", m_strPrimaryKey1, m_strPrimaryKey2));
        }

        // 終了
        private void canselButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 検索
        private void excuteSearch()
        {
            OleDbDataReader dr;
            OleDbCommand cmd;
            OleDbConnection conn = new OleDbConnection();
            // 接続文字列を設定して接続する
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                conn.Open();
                string strSql = "SELECT CD_CO, CD_DEPT, NM_DEPT, TXT_REM FROM M_DEPT WHERE CD_CO='{0}' AND CD_DEPT='{1}'";
                cmd = new OleDbCommand(String.Format(strSql, this.m_strPrimaryKey1, this.m_strPrimaryKey2), conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    this.textBox1.Text = dr.GetString(0);
                    this.textBox2.Text = dr.GetString(1);
                    this.textBox3.Text = dr.GetString(2);
                    this.textBox4.Text = dr.IsDBNull(3) ? null : dr.GetString(3);
                }
                dr.Close();
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
        }

        // SQL実行
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
                conn.Close();
                MessageBox.Show("正常に処理を完了しました。", "通知");
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            catch (DbException ex)
            {
                if (ex.ErrorCode == -2147217873)    // ORA-00001: 一意性違反
                {
                    conn.Close();
                    MessageBox.Show("既に登録されています。別のデータを登録してください。", "通知");
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("エラーが発生しました。入力項目を見なおしてください。", "通知");
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message, "通知");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        // SQL文取得
        private string getSqlString()
        {
            string strSql = null;
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE))
            {
                strSql = "INSERT INTO M_DEPT VALUES('{0}', '{1}', '{2}', '{3}', '{4}', SYSDATE, '{5}', SYSDATE, 'Y')";
                strSql = String.Format(strSql, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, "k_yoshida", "k_yoshida");
            }
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE))
            {
                strSql = "UPDATE M_DEPT SET CD_CO='{0}', CD_DEPT='{1}', NM_DEPT='{2}', TXT_REM='{3}', CD_UPDATE='{4}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{5}' AND CD_DEPT='{6}'";
                strSql = String.Format(strSql, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, "k_yoshida", m_strPrimaryKey1, m_strPrimaryKey2);
            }
            return strSql;
        }
    }
}
