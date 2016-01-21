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
		private void finishToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// 保有してるすべての MDI 子フォームを取得する
			Form[] hMdiChildren = this.MdiChildren;

			// すべての MDI 子フォームを閉じる
			Array.ForEach(hMdiChildren, c => c.Close());

			// ログイン画面表示
			this.lblUserName.Text = "サインイン";
			Login();
		}

		/// <summary>
		/// ログイン画面表示
		/// </summary>
		private void Login()
		{
			var f = new LoginForm();
			f.MdiParent = this;
			f.Show();
		}

		/// <summary>
		/// コマンド キーを処理
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			// Ctrl+Tabキー押下時Formの切替処理をしない
			if ((keyData & Keys.Tab) == Keys.Tab && (keyData & Keys.Control) == Keys.Control)
			{
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
