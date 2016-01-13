using System;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class MembersListManagementForm : Form
    {
        // プロパティ
        public string m_strUserId { get; set; }

        // 初期化処理
        public MembersListManagementForm(string strUserId)
        {
            InitializeComponent();
            this.m_strUserId = strUserId;
        }

        // Load Event Handler
        private void MembersListManagementForm_Load(object sender, EventArgs e)
        {
        }

        // 部門マスタ管理画面へ遷移
        private void button1_Click(object sender, EventArgs e)
        {
            MasterInitForm f = new MasterInitForm(CommonConstants.BUMON);
            f.MdiParent = this.MdiParent;
            f.Show();
        }

        // 社員マスタ管理画面へ遷移
        private void button2_Click(object sender, EventArgs e)
        {
            MasterInitForm f = new MasterInitForm(CommonConstants.SYAIN);
            f.MdiParent = this.MdiParent;
            f.Show();
        }

    }
}
