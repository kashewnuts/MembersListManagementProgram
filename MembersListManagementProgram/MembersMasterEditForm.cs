using System;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class MembersMasterEditForm : Form
    {
        // プロパティ
        private string m_strEditMode { get; set; }
        private string m_strPrimaryKey1 { get; set; }
        private string m_strPrimaryKey2 { get; set; }

        // 初期化処理
        public MembersMasterEditForm(string strEditMode, params string[] args)
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
        private void MembersMasterEditForm_Load(object sender, EventArgs e)
        {
            // ボタン表示・非表示切り替え
            switchVisibleButton();
            // 編集、参照ボタン押下時時データ取得
            if (!this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) excuteSearch();
        }

        // タイトル取得
        private string getFormTitle()
        {
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE)) return "社員マスタ新規作成画面";
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE)) return "社員マスタ編集画面";
            else return "社員マスタ参照画面";
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

        // 登録
        private void registerButton_Click(object sender, EventArgs e)
        {
            excuteSql(getSqlString());
        }

        // 削除
        private void deleteButton_Click(object sender, EventArgs e)
        {
            excuteSql(String.Format("UPDATE M_EMP SET DTM_UPDATE=SYSDATE, FLG_ACTIVE='N' WHERE CD_CO='{0}' AND CD_EMP='{1}'", m_strPrimaryKey1, m_strPrimaryKey2));
        }

        // 終了
        private void cancelButton_Click(object sender, EventArgs e)
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
                string strSql = "SELECT CD_CO, CD_EMP, NM_EMP, TXT_PASSWD, CD_DEPT, TXT_ZIP, TXT_ADDR1, TXT_ADDR2, TXT_ADDR3, TXT_TEL, TXT_FAX, TXT_REM FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}'";
                cmd = new OleDbCommand(String.Format(strSql, m_strPrimaryKey1, m_strPrimaryKey2), conn);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    this.textBox1.Text = dr.GetString(0);
                    this.textBox2.Text = dr.GetString(1);
                    this.textBox3.Text = dr.GetString(2);
                    this.textBox4.Text = dr.GetString(3);
                    this.textBox5.Text = dr.GetString(4);
                    this.textBox6.Text = dr.IsDBNull(5) ? null : dr.GetString(5);
                    this.textBox7.Text = dr.IsDBNull(6) ? null : dr.GetString(6);
                    this.textBox8.Text = dr.IsDBNull(7) ? null : dr.GetString(7);
                    this.textBox9.Text = dr.IsDBNull(8) ? null : dr.GetString(8);
                    this.textBox10.Text = dr.IsDBNull(9) ? null : dr.GetString(9);
                    this.textBox11.Text = dr.IsDBNull(10) ? null : dr.GetString(10);
                    this.textBox12.Text = dr.IsDBNull(11) ? null : dr.GetString(11);
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
                MessageBox.Show("正常に処理を完了しました", "通知");
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
                if (conn != null) conn.Close();
            }
        }

        // SQL文取得
        private string getSqlString()
        {
            string strSql = null;
            if (this.m_strEditMode.Equals(CommonConstants.CREATE_MODE))
            {
                strSql = "INSERT INTO M_EMP VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', SYSDATE, '{13}', SYSDATE, 'Y')";
                strSql = String.Format(
                    strSql, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text,
                    textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, "k_yoshida", "k_yoshida");
            }
            else if (this.m_strEditMode.Equals(CommonConstants.UPDATE_MODE))
            {
                strSql = "UPDATE M_EMP SET CD_CO='{0}', CD_EMP='{1}', NM_EMP='{2}', TXT_PASSWD='{3}', CD_DEPT='{4}', TXT_ZIP='{5}', TXT_ADDR1='{6}', TXT_ADDR2='{7}', TXT_ADDR3='{8}', TXT_TEL='{9}', TXT_FAX='{10}', TXT_REM='{11}', CD_UPDATE='{12}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{13}' AND CD_EMP='{14}'";
                strSql = String.Format(
                    strSql, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text,
                    textBox7.Text, textBox8.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, "k_yoshida", m_strPrimaryKey1, m_strPrimaryKey2);
            }
            return strSql;
        }
    }
}
