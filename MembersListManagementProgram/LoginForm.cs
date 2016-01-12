using System;
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
    public partial class LoginForm : Form
    {
        // 初期化処理
        public LoginForm()
        {
            InitializeComponent();
        }

        // Login
        private void button1_Click(object sender, EventArgs e)
        {
            using (var f = new MembersListManagementForm())
            {
                f.ShowDialog(this);
            }
        }
    }
}
