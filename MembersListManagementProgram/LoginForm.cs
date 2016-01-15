﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class LoginForm : Form
    {
        // メンバ変数
        private string strUserName;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLlogin_Click(object sender, EventArgs e)
        {
            if (excuteSearch())
            {
                // 親フォーム(MDIフォーム)にログインユーザー名をセット
                MainMDI parentForm = (MainMDI)this.MdiParent;
                parentForm.txtUserName.Text = strUserName;
                // メニュー画面表示
                MenuForm f = new MenuForm(txtCd_Emp.Text);
                f.MdiParent = this.MdiParent;
                f.Show();
            }
            else
            {
                MessageBox.Show("ログイン情報に誤りがあります。");
            }
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public bool excuteSearch()
        {
            bool bResult = false;
            OleDbIf db = new OleDbIf();

            try
            {
                db.connect();
                string strSql = "SELECT * FROM M_EMP WHERE CD_CO='{0}' AND CD_EMP='{1}' AND TXT_PASSWD='{2}' AND FLG_ACTIVE='Y'";
                DataTable tbl = db.executeSql(String.Format(strSql, txtCd_Co.Text, txtCd_Emp.Text, txtTxt_Passwd.Text));
                bResult = (tbl.Rows.Count > 0) ? true : false;
                if (bResult)
                {
                    strUserName = tbl.Rows[0]["nm_emp"].ToString();
                }
            }
            finally
            {
                db.disconnect();
            }
            return bResult;
        }
    }
}
