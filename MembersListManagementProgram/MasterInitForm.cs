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
    public partial class MasterInitForm : Form
    {
        // プロパティ
        public string InitId { get; set; }

        // 初期化処理
        public MasterInitForm(string InitId)
        {
            InitializeComponent();
            this.InitId = InitId;
            this.Text = getFormTitle();
            this.editButton.Enabled = false;
            this.viewButton.Enabled = false;
        }

        // Load Event Handler
        private void MembersMasterInitForm_Load(object sender, EventArgs e)
        {
        }

        // タイトル取得
        private string getFormTitle()
        {
            return this.InitId.Equals(CommonConstants.BUMON) ? "部門マスタ管理画面" : "社員マスタ管理画面";
        }

        // 検索
        private void searchButton_Click(object sender, EventArgs e)
        {
            DataView dView;
            OleDbDataAdapter dAdp;
            DataSet dSet = new DataSet();
            dSet.Clear();
            OleDbConnection cn = new OleDbConnection();
            // 接続文字列を設定して接続する
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;

            // DataSetに取得する
            string sql = null;
            string tableString = this.InitId.Equals(CommonConstants.BUMON) ? "M_DEPT" : "M_EMP";
            // TODO: 全項目ではなく、指定したい項目のみ表示
            //if (this.InitId.Equals(CommonConstants.BUMON))
            //{
            //    sql = "SELECT CD_CO, CD_DEPT, NM_DEPT, TXT_REM FROM M_DEPT";
            //}
            //else
            //{
            //    sql = "SELECT CD_CO, CD_EMP, NM_EMP, CD_DEPT, TXT_ZIP, TXT_ADDR1, TXT_ADDR2, TXT_ADDR3, TXT_TEL, TXT_FAX, TXT_REM FROM M_EMP";
            //}
            sql = String.Format("SELECT * FROM {0} WHERE FLG_ACTIVE='Y'", tableString);
            dAdp = new OleDbDataAdapter(sql, cn);
            dAdp.Fill(dSet, tableString);

            // 表示するレコードをDataViewに取得し、DataGridViewに関連付ける
            dView = new DataView(dSet.Tables[tableString], "", "", DataViewRowState.CurrentRows);
            this.dataGridView1.DataSource = dSet.Tables[tableString];

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
            showDialog(CommonConstants.CREATE_MODE);
        }

        // 編集
        private void editButton_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.UPDATE_MODE, this.dataGridView1.CurrentRow.Cells[0].Value.ToString(), this.dataGridView1.CurrentRow.Cells[1].Value.ToString());
        }

        // 参照
        private void viewButton_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.VIEW_MODE, this.dataGridView1.CurrentRow.Cells[0].Value.ToString(), this.dataGridView1.CurrentRow.Cells[1].Value.ToString());
        }

        // Daiologを開く
        private void showDialog(string mode, params string[] args)
        {
            if (this.InitId.Equals(CommonConstants.BUMON)) 
            {
                DepartmentMasterEditForm f = new DepartmentMasterEditForm(mode, args);
                f.ShowDialog(this);
            }
            else
            {
                MembersMasterEditForm f = new MembersMasterEditForm(mode, args);
                f.ShowDialog(this);
            }
        }

        // 終了
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
