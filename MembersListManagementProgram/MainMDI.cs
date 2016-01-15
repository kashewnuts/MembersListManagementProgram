using System;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class MainMDI : Form
    {
        /// <summary>
        /// 初期化処理
        /// </summary>
        public MainMDI()
        {
            InitializeComponent();
            Login();
        }

        /// <summary>
        ///  すべての MDI 子フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 保有してるすべての MDI 子フォームを取得する
            Form[] hMdiChildren = this.MdiChildren;

            // すべての MDI 子フォームを閉じる
            foreach (Form hMdiChild in hMdiChildren)
            {
                hMdiChild.Close();
            }
            // ログイン画面表示
            Login();
        }

        /// <summary>
        /// ログイン画面表示
        /// </summary>
        private void Login()
        {
            LoginForm f = new LoginForm();
            f.MdiParent = this;
            f.Show();
        }
    }
}
