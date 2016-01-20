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
    public partial class DepartmentSelectForm : Form
    {
        /// <summary>
        /// プロパティ
        /// </summary>
        private string m_strCd_Co { get; set; }
        private DepartmentEditForm dParentForm { get; set; }
        private MembersEditForm mParentForm { get; set; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public DepartmentSelectForm(string strCd_Co, object parentForm)
        {
            InitializeComponent();
            this.m_strCd_Co = strCd_Co;
            if (parentForm is DepartmentEditForm)
            {
                this.dParentForm = (DepartmentEditForm)parentForm;
            }
            else
            {
                this.mParentForm = (MembersEditForm)parentForm;
            }
        }

        /// <summary>
        /// Load Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentSelectForm_Load(object sender, EventArgs e)
        {
            // 画面サイズ指定
            this.WindowState = FormWindowState.Maximized;
            // 部門コード変更時部門名取得
            txtCd_Dept.LostFocus += txtCd_Dept_LostFocus;
            // 画面を閉じる際に部門コード、部門名の値設定
            this.FormClosed += DepartmentListForm_FormClosed;
            // KeyEvent処理
            this.KeyPress += DepartmentSelectForm_KeyPress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtCd_Dept_LostFocus(object sender, EventArgs e)
        {
            if ("".Equals(txtCd_Dept.Text))
            {
                this.txtNm_Dept.Text = null;
            }
            else
            {
                // TODO: 毎回SQLを投げるとネットワーク的によろしくないので、どこかに値を保持しておきたい。
                using (OleDbIf db = new OleDbIf())
                {
                    db.Connect();
                    string strSql = "SELECT NM_DEPT FROM M_DEPT WHERE CD_CO='{0}' AND CD_DEPT='{1}'";
                    DataTable tbl = db.ExecuteSql(String.Format(strSql, this.m_strCd_Co, txtCd_Dept.Text));
                    this.txtNm_Dept.Text = (tbl.Rows.Count > 0) ? tbl.Rows[0]["NM_DEPT"].ToString() : null;
                }
            }
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (OleDbIf db = new OleDbIf())
            {
                // DB処理
                db.Connect();
                string strSql = "SELECT CD_CO, CD_DEPT, NM_DEPT, TXT_REM FROM M_DEPT WHERE CD_CO='{0}'";
                if (!"".Equals(this.txtCd_Dept.Text)) strSql += String.Format("AND CD_DEPT='{0}'", this.txtCd_Dept.Text);
                dgv.DataSource = db.ExecuteSql(String.Format(strSql, this.m_strCd_Co));
                // DataGridViewのHeaderText変更
                SetDgvHeaderText(dgv);
            }
        }

        /// <summary>
        /// DataGridViewのHeaderText変更
        /// </summary>
        /// <param name="dgv"></param>
        private void SetDgvHeaderText(DataGridView dgv)
        {
            int i = 0;
            this.dgv.Columns[i++].HeaderText = "会社コード";
            this.dgv.Columns[i++].HeaderText = "部門コード";
            this.dgv.Columns[i++].HeaderText = "部門名";
            this.dgv.Columns[i++].HeaderText = "備考";
        }

        /// <summary>
        ///  選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectDept_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 画面Close時、部門コード・名称設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DepartmentListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 選択している行の値を設定
            if (dgv.SelectedCells.Count > 0)
            {
                if (dParentForm != null)
                {
                    // 部門コード設定
                    dParentForm.m_strCd_Dept = dgv.CurrentRow.Cells[0].Value.ToString();
                    // 部門名設定
                    dParentForm.m_strNm_Dept = dgv.CurrentRow.Cells[1].Value.ToString();
                }
                if (mParentForm != null)
                {
                    // 部門コード設定
                    mParentForm.m_strCd_Dept = dgv.CurrentRow.Cells[0].Value.ToString();
                    // 部門名設定
                    mParentForm.m_strNm_Dept = dgv.CurrentRow.Cells[1].Value.ToString();
                }
            }
        }

        /// <summary>
        /// 戻る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// KeyEvent処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentSelectForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
