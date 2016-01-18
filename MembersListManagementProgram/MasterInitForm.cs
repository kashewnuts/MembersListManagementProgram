using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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
            this.Text = GetFormTitle();
            // ボタン表示切り替え
            SwitchButtonView(false);
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
        private string GetFormTitle()
        {
            return this.m_strInitId.Equals(CommonConstants.BUMON) ? "部門マスタ管理画面" : "社員マスタ管理画面";
        }

        /// <summary>
        /// ボタン表示・非表示切替
        /// </summary>
        private void SwitchButtonView(bool flg)
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
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            OleDbIf db = new OleDbIf();
            try
            {
                db.Connect();
                // 表示するレコードを取得し、DataGridViewに関連付ける
                string strTable = this.m_strInitId.Equals(CommonConstants.BUMON) ? "M_DEPT" : "M_EMP";
                string strSql = String.Format("SELECT * FROM {0} WHERE FLG_ACTIVE='Y'", strTable);
                this.dgv.DataSource = db.ExecuteSql(strSql);
                SetDgvHeaderText(dgv);
                // ボタン活性化
                if (this.dgv.RowCount != 0) SwitchButtonView(true);
            }
            finally
            {
                db.Disconnect();
            }
        }

        /// <summary>
        /// DataGridViewのHeaderText変更
        /// </summary>
        /// <param name="dgv"></param>
        private void SetDgvHeaderText(DataGridView dgv)
        {
            int i = 0;
            this.dgv.Columns[i].ReadOnly = true;
            this.dgv.Columns[i++].HeaderText = "会社コード";
            if (this.m_strInitId.Equals(CommonConstants.BUMON))
            {
                this.dgv.Columns[i].ReadOnly = true;
                this.dgv.Columns[i++].HeaderText = "部門コード";
                this.dgv.Columns[i++].HeaderText = "部門名";
            }
            else
            {
                this.dgv.Columns[i].ReadOnly = true;
                this.dgv.Columns[i++].HeaderText = "社員コード";
                this.dgv.Columns[i++].HeaderText = "社員名";
                this.dgv.Columns[i++].HeaderText = "パスワード";
                this.dgv.Columns[i++].HeaderText = "部門コード";
                this.dgv.Columns[i++].HeaderText = "郵便番号";
                this.dgv.Columns[i++].HeaderText = "住所1";
                this.dgv.Columns[i++].HeaderText = "住所2";
                this.dgv.Columns[i++].HeaderText = "住所3";
                this.dgv.Columns[i++].HeaderText = "TEL";
                this.dgv.Columns[i++].HeaderText = "FAX";
            }
            this.dgv.Columns[i++].HeaderText = "備考";
            this.dgv.Columns[i].ReadOnly = true;
            this.dgv.Columns[i++].HeaderText = "作成者";
            this.dgv.Columns[i].ReadOnly = true;
            this.dgv.Columns[i++].HeaderText = "作成日";
            this.dgv.Columns[i].ReadOnly = true;
            this.dgv.Columns[i++].HeaderText = "最終更新者";
            this.dgv.Columns[i].ReadOnly = true;
            this.dgv.Columns[i++].HeaderText = "最終更新日";
            this.dgv.Columns[i].ReadOnly = true;
            this.dgv.Columns[i++].HeaderText = "有効フラグ";
        }

        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            ShowDialog(CommonConstants.CREATE_MODE, sender, e);
        }

        /// <summary>
        /// 編集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            string[] args = MakeParams();
            ShowDialog(CommonConstants.UPDATE_MODE, sender, e, args[0], args[1]);
        }

        /// <summary>
        /// 参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnView_Click(object sender, EventArgs e)
        {
            string[] args = MakeParams();
            ShowDialog(CommonConstants.VIEW_MODE, sender, e, args[0], args[1]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string[] MakeParams()
        {
            BindingManagerBase bm = dgv.BindingContext[dgv.DataSource, dgv.DataMember];
            DataRowView drv = (DataRowView)bm.Current;
            DataRow row = drv.Row;

            string[] args = new string[2];
            args[0] = row["CD_CO", DataRowVersion.Original].ToString();
            if (this.m_strInitId.Equals(CommonConstants.BUMON))
            {
                args[1] = (row["CD_DEPT", DataRowVersion.Original] != null) ? row["CD_DEPT", DataRowVersion.Original].ToString() : null;
            }
            else
            {
                args[1] = (row["CD_EMP", DataRowVersion.Original] != null) ? row["CD_EMP", DataRowVersion.Original].ToString() : null;
            }
            return args;
        }

        /// <summary>
        /// Daiologを開く
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="args"></param>
        private void ShowDialog(string mode, object sender, EventArgs e, params string[] args)
        {
            if (this.m_strInitId.Equals(CommonConstants.BUMON))
            {
                // 部門管理画面処理
                DepartmentMasterEditForm f = new DepartmentMasterEditForm(mode, args);
                f.MdiParent = this.MdiParent;
                f.Show();
                if (f.DialogResult == DialogResult.OK && this.dgv.RowCount != 0) BtnSearch_Click(sender, e);
            }
            else
            {
                // 社員管理画面処理
                MembersMasterEditForm f = new MembersMasterEditForm(mode, args);
                f.MdiParent = this.MdiParent;
                f.Show();
                if (f.DialogResult == DialogResult.OK && this.dgv.RowCount != 0) BtnSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            OleDbIf db = new OleDbIf();
            try
            {
                string strSql = null;
                string cd_co, cd_dept, cd_emp;
                MainMDI f = (MainMDI)this.MdiParent;
                DataTable tbl = (DataTable)this.dgv.DataSource;
                List<string> lst = new List<string>();

                // 編集された行をコミットする
                foreach (DataRow row in tbl.Rows)
                {
                    if (row.RowState != DataRowState.Unchanged)
                    {
                        cd_co = row["CD_CO", DataRowVersion.Original].ToString();
                        if (this.m_strInitId.Equals(CommonConstants.BUMON))
                        {
                            cd_dept = (row["CD_DEPT", DataRowVersion.Original] != null) ? row["CD_DEPT", DataRowVersion.Original].ToString() : null;
                            strSql = "UPDATE M_DEPT SET CD_CO='{0}', CD_DEPT='{1}', NM_DEPT='{2}', TXT_REM='{3}', CD_UPDATE='{4}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{5}' AND CD_DEPT='{6}'";
                            strSql = String.Format(
                                strSql,
                                row["CD_CO", DataRowVersion.Current].ToString(),
                                row["CD_DEPT", DataRowVersion.Current].ToString(),
                                row["NM_DEPT", DataRowVersion.Current].ToString(),
                                row["TXT_REM", DataRowVersion.Current].ToString(),
                                f.txtUserName.Text, cd_co, cd_dept);
                        }
                        else
                        {
                            cd_emp = (row["CD_EMP", DataRowVersion.Original] != null) ? row["CD_EMP", DataRowVersion.Original].ToString() : null;
                            strSql = "UPDATE M_EMP SET CD_CO='{0}', CD_EMP='{1}', NM_EMP='{2}', TXT_PASSWD='{3}', CD_DEPT='{4}', TXT_ZIP='{5}', TXT_ADDR1='{6}', TXT_ADDR2='{7}', TXT_ADDR3='{8}', TXT_TEL='{9}', TXT_FAX='{10}', TXT_REM='{11}', CD_UPDATE='{12}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{13}' AND CD_EMP='{14}'";
                            strSql = String.Format(
                                strSql,
                                row["CD_CO", DataRowVersion.Current].ToString(),
                                row["CD_EMP", DataRowVersion.Current].ToString(),
                                row["NM_EMP", DataRowVersion.Current].ToString(),
                                row["TXT_PASSWD", DataRowVersion.Current].ToString(),
                                row["CD_DEPT", DataRowVersion.Current].ToString(),
                                row["TXT_ZIP", DataRowVersion.Current].ToString(),
                                row["TXT_ADDR1", DataRowVersion.Current].ToString(),
                                row["TXT_ADDR2", DataRowVersion.Current].ToString(),
                                row["TXT_ADDR3", DataRowVersion.Current].ToString(),
                                row["TXT_TEL", DataRowVersion.Current].ToString(),
                                row["TXT_FAX", DataRowVersion.Current].ToString(),
                                row["TXT_REM", DataRowVersion.Current].ToString(),
                                f.txtUserName.Text, cd_co, cd_emp);
                        }
                        lst.Add(strSql);
                    }
                }
                // SQL実行
                db.Connect();
                foreach (string s in lst)
                {
                    db.ExecuteSql(s);
                }
                BtnSearch_Click(sender, e);
            }
            finally
            {
                db.Disconnect();
            }
        }
    }
}