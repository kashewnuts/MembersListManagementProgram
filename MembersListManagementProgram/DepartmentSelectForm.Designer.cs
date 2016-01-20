namespace MembersListManagementProgram
{
    partial class DepartmentSelectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataSet1 = new System.Data.DataSet();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblCd_Dept = new System.Windows.Forms.Label();
            this.txtCd_Dept = new System.Windows.Forms.TextBox();
            this.txtNm_Dept = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSelectDept = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(23, 93);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowTemplate.Height = 21;
            this.dgv.Size = new System.Drawing.Size(739, 419);
            this.dgv.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(687, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblCd_Dept
            // 
            this.lblCd_Dept.AutoSize = true;
            this.lblCd_Dept.Location = new System.Drawing.Point(23, 33);
            this.lblCd_Dept.Name = "lblCd_Dept";
            this.lblCd_Dept.Size = new System.Drawing.Size(56, 12);
            this.lblCd_Dept.TabIndex = 2;
            this.lblCd_Dept.Text = "部門コード";
            // 
            // txtCd_Dept
            // 
            this.txtCd_Dept.Location = new System.Drawing.Point(85, 30);
            this.txtCd_Dept.Name = "txtCd_Dept";
            this.txtCd_Dept.Size = new System.Drawing.Size(100, 19);
            this.txtCd_Dept.TabIndex = 1;
            // 
            // txtNm_Dept
            // 
            this.txtNm_Dept.Location = new System.Drawing.Point(191, 30);
            this.txtNm_Dept.Name = "txtNm_Dept";
            this.txtNm_Dept.Size = new System.Drawing.Size(305, 19);
            this.txtNm_Dept.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(686, 527);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "戻る";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSelectDept
            // 
            this.btnSelectDept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDept.Location = new System.Drawing.Point(687, 58);
            this.btnSelectDept.Name = "btnSelectDept";
            this.btnSelectDept.Size = new System.Drawing.Size(75, 23);
            this.btnSelectDept.TabIndex = 4;
            this.btnSelectDept.Text = "選択";
            this.btnSelectDept.UseVisualStyleBackColor = true;
            this.btnSelectDept.Click += new System.EventHandler(this.btnSelectDept_Click);
            // 
            // DepartmentSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.ControlBox = false;
            this.Controls.Add(this.btnSelectDept);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtNm_Dept);
            this.Controls.Add(this.txtCd_Dept);
            this.Controls.Add(this.lblCd_Dept);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgv);
            this.KeyPreview = true;
            this.Name = "DepartmentSelectForm";
            this.Text = "部門一覧表示画面";
            this.Load += new System.EventHandler(this.DepartmentSelectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblCd_Dept;
        private System.Windows.Forms.TextBox txtCd_Dept;
        private System.Windows.Forms.TextBox txtNm_Dept;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSelectDept;
    }
}