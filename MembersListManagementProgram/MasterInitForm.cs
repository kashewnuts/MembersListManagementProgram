using System;
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
            string[] args = makeParams();
            showDialog(CommonConstants.UPDATE_MODE, sender, e, args[0], args[1]);
        }

        /// <summary>
        /// 参照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            string[] args = makeParams();
            showDialog(CommonConstants.VIEW_MODE, sender, e, args[0], args[1]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string[] makeParams()
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
            string strSql = null;
            string cd_co, cd_dept, cd_emp;
            MainMDI f = (MainMDI)this.MdiParent;
            DataTable tbl = (DataTable)this.dgv.DataSource;
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
                            this.dgv.CurrentRow.Cells[0].Value.ToString(), 
                            this.dgv.CurrentRow.Cells[1].Value.ToString(), 
                            this.dgv.CurrentRow.Cells[2].Value.ToString(), 
                            this.dgv.CurrentRow.Cells[3].Value.ToString(),
                            f.txtUserName.Text, cd_co, cd_dept);
                    }
                    else
                    {
                        cd_emp = (row["CD_EMP", DataRowVersion.Original] != null) ? row["CD_EMP", DataRowVersion.Original].ToString() : null;
                        strSql = "UPDATE M_EMP SET CD_CO='{0}', CD_EMP='{1}', NM_EMP='{2}', TXT_PASSWD='{3}', CD_DEPT='{4}', TXT_ZIP='{5}', TXT_ADDR1='{6}', TXT_ADDR2='{7}', TXT_ADDR3='{8}', TXT_TEL='{9}', TXT_FAX='{10}', TXT_REM='{11}', CD_UPDATE='{12}', DTM_UPDATE=SYSDATE, FLG_ACTIVE='Y' WHERE CD_CO='{13}' AND CD_EMP='{14}'";
                        strSql = String.Format(
                            strSql,
                            this.dgv.CurrentRow.Cells[0].Value.ToString(),
                            this.dgv.CurrentRow.Cells[1].Value.ToString(),
                            this.dgv.CurrentRow.Cells[2].Value.ToString(),
                            this.dgv.CurrentRow.Cells[3].Value.ToString(),
                            this.dgv.CurrentRow.Cells[4].Value.ToString(),
                            this.dgv.CurrentRow.Cells[5].Value.ToString(),
                            this.dgv.CurrentRow.Cells[6].Value.ToString(),
                            this.dgv.CurrentRow.Cells[7].Value.ToString(),
                            this.dgv.CurrentRow.Cells[8].Value.ToString(),
                            this.dgv.CurrentRow.Cells[9].Value.ToString(),
                            this.dgv.CurrentRow.Cells[10].Value.ToString(),
                            this.dgv.CurrentRow.Cells[11].Value.ToString(),
                            this.dgv.CurrentRow.Cells[12].Value.ToString(),
                            f.txtUserName.Text, cd_co, cd_emp);
                    }
                    excuteSql(strSql);
                }
            }
        }

        /// <summary>
        /// SQL実行
        /// </summary>
        /// <param name="strSql"></param>
        private void excuteSql(string strSql)
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd;
            // 接続文字列を設定して接続する
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MembersListManagementProgram.Properties.Settings.ConnectionString"].ConnectionString;
            try
            {
                conn.Open();
                cmd = new OleDbCommand(strSql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("正常に処理を完了しました。", "通知");
            }
            catch (DbException ex)
            {
                if (ex.ErrorCode == -2147217873)    // ORA-00001: 一意性違反
                {
                    MessageBox.Show("既に登録されています。別のデータを登録してください。", "通知");
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。入力項目を見なおしてください。", "通知");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "通知");
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
    }
}
