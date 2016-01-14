using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MembersListManagementProgram
{
    public partial class MasterInitForm : Form
    {
        /// <summary>
        /// プロパティ
        /// </summary>
        public string m_strInitId { get; set; }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="strInitId"></param>
        public MasterInitForm(string strInitId)
        {
            InitializeComponent();
            this.m_strInitId = strInitId;
            this.Text = getFormTitle();
            // ボタン表示切り替え
            switchButtonView(false);
        }

        /// <summary>
        /// Load Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterInitForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// タイトル取得
        /// </summary>
        /// <returns></returns>
        private string getFormTitle()
        {
            return this.m_strInitId.Equals(CommonConstants.BUMON) ? "部門マスタ管理画面" : "社員マスタ管理画面";
        }

        /// <summary>
        /// ボタン表示・非表示切替
        /// </summary>
        private void switchButtonView(bool flg)
        {
            this.btnEdit.Enabled = flg;
            this.btnView.Enabled = flg;
            this.btnUpdate.Enabled = flg;
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataView dv;
            OleDbDataAdapter da;
            DataSet ds = new DataSet();
            ds.Clear();
            OleDbConnection conn = new OleDbConnection();
            // 接続文字列を設定して接続する
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;

            // DataSetに取得する
            string strSql = null;
            string strTable = this.m_strInitId.Equals(CommonConstants.BUMON) ? "M_DEPT" : "M_EMP";
            // TODO: 全項目ではなく、指定したい項目のみ表示
            //if (this.InitId.Equals(CommonConstants.BUMON))
            //{
            //    sql = "SELECT CD_CO, CD_DEPT, NM_DEPT, TXT_REM FROM M_DEPT";
            //}
            //else
            //{
            //    sql = "SELECT CD_CO, CD_EMP, NM_EMP, CD_DEPT, TXT_ZIP, TXT_ADDR1, TXT_ADDR2, TXT_ADDR3, TXT_TEL, TXT_FAX, TXT_REM FROM M_EMP";
            //}
            strSql = String.Format("SELECT * FROM {0} WHERE FLG_ACTIVE='Y'", strTable);
            da = new OleDbDataAdapter(strSql, conn);
            da.Fill(ds, strTable);

            // 表示するレコードをDataViewに取得し、DataGridViewに関連付ける
            dv = new DataView(ds.Tables[strTable], "", "", DataViewRowState.CurrentRows);
            this.dgv.DataSource = ds.Tables[strTable];

            // ボタン活性化
            if (this.dgv.RowCount != 0) switchButtonView(true);
        }

        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.CREATE_MODE, sender, e);
        }

        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.UPDATE_MODE, sender, e, this.dgv.CurrentRow.Cells[0].Value.ToString(), this.dgv.CurrentRow.Cells[1].Value.ToString());
        }

        /// <summary>
        /// 参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            showDialog(CommonConstants.VIEW_MODE, sender, e, this.dgv.CurrentRow.Cells[0].Value.ToString(), this.dgv.CurrentRow.Cells[1].Value.ToString());
        }

        /// <summary>
        /// Daiologを開く
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="args"></param>
        private void showDialog(string mode, object sender, EventArgs e, params string[] args)
        {
            if (this.m_strInitId.Equals(CommonConstants.BUMON))
            {
                // 部門管理画面処理
                DepartmentMasterEditForm f = new DepartmentMasterEditForm(mode, args);
                f.MdiParent = this.MdiParent;
                f.Show();
                if (f.DialogResult == DialogResult.OK && this.dgv.RowCount != 0) btnSearch_Click(sender, e);
            }
            else
            {
                // 社員管理画面処理
                MembersMasterEditForm f = new MembersMasterEditForm(mode, args);
                f.MdiParent = this.MdiParent;
                f.Show();
                if (f.DialogResult == DialogResult.OK && this.dgv.RowCount != 0) btnSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataTable tbl;
            tbl = (DataTable)this.dgv.DataSource;
            // 編集された行をコミットする
            foreach (DataRow row in tbl.Rows)
            {
                if (row.RowState != DataRowState.Unchanged)
                {
                    row.AcceptChanges();
                }
            }
        }
    }
}
