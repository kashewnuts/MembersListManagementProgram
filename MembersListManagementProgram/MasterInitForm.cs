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

        }

        // 新規作成
        private void createButton_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.CREATE_MODE);
        }

        // 編集
        private void editButton_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.UPDATE_MODE);
        }

        // 参照
        private void viewButton_Click(object sender, EventArgs e)
        {
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
