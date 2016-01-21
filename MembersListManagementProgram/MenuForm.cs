using System;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class MenuForm : Form
    {
        /// <summary>
        /// プロパティ
        /// </summary>
        public string m_strUserId { get; set; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="strUserId"></param>
        public MenuForm(string strUserId)
        {
            InitializeComponent();
            this.m_strUserId = strUserId;
        }

        /// <summary>
        /// Load Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// 部門マスタ管理画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDept_Click(object sender, EventArgs e)
        {
            var f = new ListViewForm(CommonConstants.MasterMode.BUMON);
            f.MdiParent = this.MdiParent;
            f.Show();
        }

        /// <summary>
        /// 社員マスタ管理画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmp_Click(object sender, EventArgs e)
        {
            var f = new ListViewForm(CommonConstants.MasterMode.SYAIN);
            f.MdiParent = this.MdiParent;
            f.Show();
        }

    }
}
