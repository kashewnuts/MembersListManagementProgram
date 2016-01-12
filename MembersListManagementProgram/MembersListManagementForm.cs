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
    public partial class MembersListManagementForm : Form
    {
        // プロパティ
        public int InitId { get; set; }

        // 初期化処理
        public MembersListManagementForm()
        {
            InitializeComponent();
        }

        // Load Event Handler
        private void MembersListManagementForm_Load(object sender, EventArgs e)
        {

        }

        // 部門マスタ管理画面へ遷移
        private void button1_Click(object sender, EventArgs e)
        {
            using (var f = new MasterInitForm(CommonConstants.BUMON))
            {
                f.ShowDialog(this);
            }
        }

        // 社員マスタ管理画面へ遷移
        private void button2_Click(object sender, EventArgs e)
        {
            using (var f = new MasterInitForm(CommonConstants.SYAIN))
            {
                f.ShowDialog(this);
            }
        }

    }
}
