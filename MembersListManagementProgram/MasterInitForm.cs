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

        private void MembersMasterInitForm_Load(object sender, EventArgs e)
        {
        }

        // タイトル取得
        private string getFormTitle()
        {
            return this.InitId.Equals(CommonConstants.BUMON) ? "部門マスタ管理画面" : "社員マスタ管理画面";
        }

        // 検索
        private void button1_Click(object sender, EventArgs e)
        {

        }

        // 新規作成
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.InitId.Equals(CommonConstants.BUMON))
            {
                DepartmentMasterEditForm f = new DepartmentMasterEditForm(CommonConstants.CREATE_MODE);
                f.ShowDialog(this);
            }
            else
            {
                MembersMasterEditForm f = new MembersMasterEditForm(CommonConstants.CREATE_MODE);
                f.ShowDialog(this);
            }
        }

        // 編集
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.InitId.Equals(CommonConstants.BUMON))
            {
                DepartmentMasterEditForm f = new DepartmentMasterEditForm(CommonConstants.UPDATE_MODE);
                f.ShowDialog(this);
            }
            else
            {
                MembersMasterEditForm f = new MembersMasterEditForm(CommonConstants.UPDATE_MODE);
                f.ShowDialog(this);
            }
        }

        // 参照
        private void button4_Click(object sender, EventArgs e)
        {
            if (this.InitId.Equals(CommonConstants.BUMON))
            {
                DepartmentMasterEditForm f = new DepartmentMasterEditForm(CommonConstants.VIEW_MODE);
                f.ShowDialog(this);
            }
            else
            {
                MembersMasterEditForm f = new MembersMasterEditForm(CommonConstants.VIEW_MODE);
                f.ShowDialog(this);
            }
        }

        // 終了
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
