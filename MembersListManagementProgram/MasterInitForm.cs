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
        public string InitId { get; set; }
        public MasterInitForm(string InitId)
        {
            InitializeComponent();
            this.InitId = InitId;
            this.Text = getFormTitle();
        }

        // Loadイベントハンドラ
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
            OleDbConnection cn = new OleDbConnection();
            // 接続文字列を設定して接続する
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;

            // DataSetに取得する
            string sql = null;
            string tableString = this.InitId.Equals(CommonConstants.BUMON) ? "M_DEPT" : "M_EMP";
            //if (this.InitId.Equals(CommonConstants.BUMON))
            //{
            //    sql = "SELECT CD_CO, CD_DEPT, NM_DEPT, TXT_REM FROM M_DEPT";
            //}
            //else
            //{
            //    sql = "SELECT CD_CO, CD_EMP, NM_EMP, CD_DEPT, TXT_ZIP, TXT_ADDR1, TXT_ADDR2, TXT_ADDR3, TXT_TEL, TXT_FAX, TXT_REM FROM M_EMP";
            //}
            sql = String.Format("SELECT * FROM {0}", tableString);
            dAdp = new OleDbDataAdapter(sql, cn);
            dAdp.Fill(dSet, tableString);

            // 表示するレコードをDataViewに取得し、DataGridViewに関連付ける
            dView = new DataView(dSet.Tables[tableString], "", "", DataViewRowState.CurrentRows);
            this.dataGridView1.DataSource = dSet.Tables[tableString];
        }

        // 新規作成
        private void createButton_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.CREATE_MODE);
        }

        // 編集
        private void editButton_Click(object sender, EventArgs e)
        {
            // TODO: 選択した行がわかるように情報渡す。
            showDialog(CommonConstants.UPDATE_MODE);
        }

        // 参照
        private void viewButton_Click(object sender, EventArgs e)
        {
            // TODO: 選択した行がわかるように情報渡す。
            showDialog(CommonConstants.VIEW_MODE);
        }

        // Daiologを開く
        private void showDialog(string mode)
        {
            if (this.InitId.Equals(CommonConstants.BUMON)) 
            {
                DepartmentMasterEditForm f = new DepartmentMasterEditForm(mode);
                f.ShowDialog(this);
            }
            else
            {
                MembersMasterEditForm f = new MembersMasterEditForm(mode);
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
