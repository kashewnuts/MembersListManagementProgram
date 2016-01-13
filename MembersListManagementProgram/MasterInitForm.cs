using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class MasterInitForm : Form
    {

        // プロパティ
        public string m_strInitId { get; set; }

        // 初期化処理
        public MasterInitForm(string strInitId)
        {
            InitializeComponent();
            this.m_strInitId = strInitId;
            this.Text = getFormTitle();
            LoginForm f = new LoginForm();
            this.label1.Text = String.Format("ログインユーザー名：{0}", User.nm_emp);
            this.editButton.Enabled = false;
            this.viewButton.Enabled = false;
        }

        // Load Event Handler
        private void MasterInitForm_Load(object sender, EventArgs e)
        {

        }

        // タイトル取得
        private string getFormTitle()
        {
            return this.m_strInitId.Equals(CommonConstants.BUMON) ? "部門マスタ管理画面" : "社員マスタ管理画面";
        }

        // 検索
        private void searchButton_Click(object sender, EventArgs e)
        {
            DataView dv;
            OleDbDataAdapter da;
            DataSet ds = new DataSet();
            ds.Clear();
            OleDbConnection conn = new OleDbConnection();
            // 接続文字列を設定して接続する
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;

            // DataSetに取得する
            string strSql = null;
            string strTable = this.m_strInitId.Equals(CommonConstants.BUMON) ? "M_DEPT" : "M_EMP";
            // TODO: 全項目ではなく、指定したい項目のみ表示
            //if (this.InitId.Equals(CommonConstants.BUMON))
            //{
            //    sql = "SELECT CD_CO, CD_DEPT, NM_DEPT, TXT_REM FROM M_DEPT";
            //}
            //else
            //{
            //    sql = "SELECT CD_CO, CD_EMP, NM_EMP, CD_DEPT, TXT_ZIP, TXT_ADDR1, TXT_ADDR2, TXT_ADDR3, TXT_TEL, TXT_FAX, TXT_REM FROM M_EMP";
            //}
            strSql = String.Format("SELECT * FROM {0} WHERE FLG_ACTIVE='Y'", strTable);
            da = new OleDbDataAdapter(strSql, conn);
            da.Fill(ds, strTable);

            // 表示するレコードをDataViewに取得し、DataGridViewに関連付ける
            dv = new DataView(ds.Tables[strTable], "", "", DataViewRowState.CurrentRows);
            this.dataGridView1.DataSource = ds.Tables[strTable];

            // ボタン活性化
            if (this.dataGridView1.RowCount != 0)
            {
                this.editButton.Enabled = true;
                this.viewButton.Enabled = true;
            }
        }

        // 新規作成
        private void createButton_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.CREATE_MODE, sender, e);
        }

        // 編集
        private void editButton_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.UPDATE_MODE, sender, e, this.dataGridView1.CurrentRow.Cells[0].Value.ToString(), this.dataGridView1.CurrentRow.Cells[1].Value.ToString());
        }

        // 参照
        private void viewButton_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.VIEW_MODE, sender, e, this.dataGridView1.CurrentRow.Cells[0].Value.ToString(), this.dataGridView1.CurrentRow.Cells[1].Value.ToString());
        }

        // Daiologを開く
        private void showDialog(string mode, object sender, EventArgs e, params string[] args)
        {
            if (this.m_strInitId.Equals(CommonConstants.BUMON)) 
            {
                // 部門管理画面処理
                DepartmentMasterEditForm f = new DepartmentMasterEditForm(mode, args);
                f.MdiParent = this.MdiParent;
                f.Show();
                if (f.DialogResult == DialogResult.OK && this.dataGridView1.RowCount != 0) searchButton_Click(sender, e);
            }
            else
            {
                // 社員管理画面処理
                MembersMasterEditForm f = new MembersMasterEditForm(mode, args);
                f.MdiParent = this.MdiParent;
                f.Show();
                if (f.DialogResult == DialogResult.OK && this.dataGridView1.RowCount != 0) searchButton_Click(sender, e);
            }
        }

        // 終了
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
