using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm f = new LoginForm();
            // TODO: ログイン処理の結果によって、処理を分ける。
            // success→管理画面へ遷移する。
            // fail→ログイン画面のままで、エラーメッセージを表示する。
            if (DialogResult.OK == f.ShowDialog())
            {
                Application.Run(new MembersListManagementForm());
            }
            //else if (DialogResult.Cancel == )
        }
    }
}
