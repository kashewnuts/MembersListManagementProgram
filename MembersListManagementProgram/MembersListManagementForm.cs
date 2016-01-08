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
        public int InitId { get; set; }
        public MembersListManagementForm()
        {
            InitializeComponent();
        }

        private void MembersListManagementForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var f = new MasterInitForm("1"))
            {
                f.ShowDialog(this);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var f = new MasterInitForm("2"))
            {
                f.ShowDialog(this);
            }
        }

    }
}
