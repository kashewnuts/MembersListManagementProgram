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
            DialogResult result = f.ShowDialog();
            if (DialogResult.OK == result)
            {
                Application.Run(new MembersListManagementForm());
            }
            else if (DialogResult.Cancel == result)
            {
                // TODO: これだと新しくLogin画面を開いて終わりになるので、画面はそのままでエラーメッセージを表示するようにしたい。
                f.ShowDialog();
            }
        }
    }
}
