﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoginForm loginForm = new LoginForm();
            loginForm.MdiParent = this;
            loginForm.Show();
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
            foreach (Form hMdiChild in hMdiChildren)
            {
                hMdiChild.Close();
                hMdiChild.Dispose();
            }

            // ログイン画面表示
            LoginForm loginForm = new LoginForm();
            loginForm.MdiParent = this;
            loginForm.Show();
        }
    }
}
