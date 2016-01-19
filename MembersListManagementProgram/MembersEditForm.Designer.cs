namespace MembersListManagementProgram
{
    partial class MembersEditForm
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
            this.lblCd_Co = new System.Windows.Forms.Label();
            this.lblCd_Emp = new System.Windows.Forms.Label();
            this.lblNm_Emp = new System.Windows.Forms.Label();
            this.lblTxt_Passwd = new System.Windows.Forms.Label();
            this.lblCd_Dept = new System.Windows.Forms.Label();
            this.lblTxt_Zip = new System.Windows.Forms.Label();
            this.lblTxt_Addr1 = new System.Windows.Forms.Label();
            this.lblTxt_Addr2 = new System.Windows.Forms.Label();
            this.lblTxt_Addr3 = new System.Windows.Forms.Label();
            this.lblTxt_Tel = new System.Windows.Forms.Label();
            this.lblTxt_Fax = new System.Windows.Forms.Label();
            this.lblTxt_Rem = new System.Windows.Forms.Label();
            this.txtCd_Emp = new System.Windows.Forms.TextBox();
            this.txtNm_Emp = new System.Windows.Forms.TextBox();
            this.txtTxt_Passwd = new System.Windows.Forms.TextBox();
            this.txtCd_Dept = new System.Windows.Forms.TextBox();
            this.txtTxt_Zip = new System.Windows.Forms.TextBox();
            this.txtTxt_Addr1 = new System.Windows.Forms.TextBox();
            this.txtTxt_Addr2 = new System.Windows.Forms.TextBox();
            this.txtTxt_Addr3 = new System.Windows.Forms.TextBox();
            this.txtTxt_Tel = new System.Windows.Forms.TextBox();
            this.txtTxt_Fax = new System.Windows.Forms.TextBox();
            this.txtTxt_Rem = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbCdCo = new System.Windows.Forms.ComboBox();
            this.txtNm_Dept = new System.Windows.Forms.TextBox();
            this.btnDept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCd_Co
            // 
            this.lblCd_Co.AutoSize = true;
            this.lblCd_Co.Location = new System.Drawing.Point(25, 23);
            this.lblCd_Co.Name = "lblCd_Co";
            this.lblCd_Co.Size = new System.Drawing.Size(56, 12);
            this.lblCd_Co.TabIndex = 0;
            this.lblCd_Co.Text = "会社コード";
            // 
            // lblCd_Emp
            // 
            this.lblCd_Emp.AutoSize = true;
            this.lblCd_Emp.Location = new System.Drawing.Point(25, 45);
            this.lblCd_Emp.Name = "lblCd_Emp";
            this.lblCd_Emp.Size = new System.Drawing.Size(56, 12);
            this.lblCd_Emp.TabIndex = 1;
            this.lblCd_Emp.Text = "社員コード";
            // 
            // lblNm_Emp
            // 
            this.lblNm_Emp.AutoSize = true;
            this.lblNm_Emp.Location = new System.Drawing.Point(25, 67);
            this.lblNm_Emp.Name = "lblNm_Emp";
            this.lblNm_Emp.Size = new System.Drawing.Size(41, 12);
            this.lblNm_Emp.TabIndex = 2;
            this.lblNm_Emp.Text = "社員名";
            // 
            // lblTxt_Passwd
            // 
            this.lblTxt_Passwd.AutoSize = true;
            this.lblTxt_Passwd.Location = new System.Drawing.Point(25, 89);
            this.lblTxt_Passwd.Name = "lblTxt_Passwd";
            this.lblTxt_Passwd.Size = new System.Drawing.Size(52, 12);
            this.lblTxt_Passwd.TabIndex = 3;
            this.lblTxt_Passwd.Text = "パスワード";
            // 
            // lblCd_Dept
            // 
            this.lblCd_Dept.AutoSize = true;
            this.lblCd_Dept.Location = new System.Drawing.Point(25, 111);
            this.lblCd_Dept.Name = "lblCd_Dept";
            this.lblCd_Dept.Size = new System.Drawing.Size(56, 12);
            this.lblCd_Dept.TabIndex = 4;
            this.lblCd_Dept.Text = "部門コード";
            // 
            // lblTxt_Zip
            // 
            this.lblTxt_Zip.AutoSize = true;
            this.lblTxt_Zip.Location = new System.Drawing.Point(25, 133);
            this.lblTxt_Zip.Name = "lblTxt_Zip";
            this.lblTxt_Zip.Size = new System.Drawing.Size(53, 12);
            this.lblTxt_Zip.TabIndex = 5;
            this.lblTxt_Zip.Text = "郵便番号";
            // 
            // lblTxt_Addr1
            // 
            this.lblTxt_Addr1.AutoSize = true;
            this.lblTxt_Addr1.Location = new System.Drawing.Point(25, 155);
            this.lblTxt_Addr1.Name = "lblTxt_Addr1";
            this.lblTxt_Addr1.Size = new System.Drawing.Size(35, 12);
            this.lblTxt_Addr1.TabIndex = 6;
            this.lblTxt_Addr1.Text = "住所1";
            // 
            // lblTxt_Addr2
            // 
            this.lblTxt_Addr2.AutoSize = true;
            this.lblTxt_Addr2.Location = new System.Drawing.Point(25, 177);
            this.lblTxt_Addr2.Name = "lblTxt_Addr2";
            this.lblTxt_Addr2.Size = new System.Drawing.Size(35, 12);
            this.lblTxt_Addr2.TabIndex = 7;
            this.lblTxt_Addr2.Text = "住所2";
            // 
            // lblTxt_Addr3
            // 
            this.lblTxt_Addr3.AutoSize = true;
            this.lblTxt_Addr3.Location = new System.Drawing.Point(25, 199);
            this.lblTxt_Addr3.Name = "lblTxt_Addr3";
            this.lblTxt_Addr3.Size = new System.Drawing.Size(35, 12);
            this.lblTxt_Addr3.TabIndex = 8;
            this.lblTxt_Addr3.Text = "住所3";
            // 
            // lblTxt_Tel
            // 
            this.lblTxt_Tel.AutoSize = true;
            this.lblTxt_Tel.Location = new System.Drawing.Point(25, 221);
            this.lblTxt_Tel.Name = "lblTxt_Tel";
            this.lblTxt_Tel.Size = new System.Drawing.Size(25, 12);
            this.lblTxt_Tel.TabIndex = 9;
            this.lblTxt_Tel.Text = "TEL";
            // 
            // lblTxt_Fax
            // 
            this.lblTxt_Fax.AutoSize = true;
            this.lblTxt_Fax.Location = new System.Drawing.Point(25, 243);
            this.lblTxt_Fax.Name = "lblTxt_Fax";
            this.lblTxt_Fax.Size = new System.Drawing.Size(27, 12);
            this.lblTxt_Fax.TabIndex = 10;
            this.lblTxt_Fax.Text = "FAX";
            // 
            // lblTxt_Rem
            // 
            this.lblTxt_Rem.AutoSize = true;
            this.lblTxt_Rem.Location = new System.Drawing.Point(25, 265);
            this.lblTxt_Rem.Name = "lblTxt_Rem";
            this.lblTxt_Rem.Size = new System.Drawing.Size(29, 12);
            this.lblTxt_Rem.TabIndex = 11;
            this.lblTxt_Rem.Text = "備考";
            // 
            // txtCd_Emp
            // 
            this.txtCd_Emp.Location = new System.Drawing.Point(92, 42);
            this.txtCd_Emp.MaxLength = 30;
            this.txtCd_Emp.Name = "txtCd_Emp";
            this.txtCd_Emp.Size = new System.Drawing.Size(196, 19);
            this.txtCd_Emp.TabIndex = 13;
            // 
            // txtNm_Emp
            // 
            this.txtNm_Emp.Location = new System.Drawing.Point(92, 64);
            this.txtNm_Emp.MaxLength = 100;
            this.txtNm_Emp.Name = "txtNm_Emp";
            this.txtNm_Emp.Size = new System.Drawing.Size(443, 19);
            this.txtNm_Emp.TabIndex = 14;
            // 
            // txtTxt_Passwd
            // 
            this.txtTxt_Passwd.Location = new System.Drawing.Point(92, 86);
            this.txtTxt_Passwd.MaxLength = 30;
            this.txtTxt_Passwd.Name = "txtTxt_Passwd";
            this.txtTxt_Passwd.PasswordChar = '*';
            this.txtTxt_Passwd.Size = new System.Drawing.Size(196, 19);
            this.txtTxt_Passwd.TabIndex = 15;
            // 
            // txtCd_Dept
            // 
            this.txtCd_Dept.Location = new System.Drawing.Point(92, 108);
            this.txtCd_Dept.MaxLength = 30;
            this.txtCd_Dept.Name = "txtCd_Dept";
            this.txtCd_Dept.Size = new System.Drawing.Size(60, 19);
            this.txtCd_Dept.TabIndex = 16;
            // 
            // txtTxt_Zip
            // 
            this.txtTxt_Zip.Location = new System.Drawing.Point(92, 130);
            this.txtTxt_Zip.MaxLength = 30;
            this.txtTxt_Zip.Name = "txtTxt_Zip";
            this.txtTxt_Zip.Size = new System.Drawing.Size(196, 19);
            this.txtTxt_Zip.TabIndex = 17;
            // 
            // txtTxt_Addr1
            // 
            this.txtTxt_Addr1.Location = new System.Drawing.Point(92, 152);
            this.txtTxt_Addr1.MaxLength = 100;
            this.txtTxt_Addr1.Name = "txtTxt_Addr1";
            this.txtTxt_Addr1.Size = new System.Drawing.Size(443, 19);
            this.txtTxt_Addr1.TabIndex = 18;
            // 
            // txtTxt_Addr2
            // 
            this.txtTxt_Addr2.Location = new System.Drawing.Point(92, 174);
            this.txtTxt_Addr2.MaxLength = 100;
            this.txtTxt_Addr2.Name = "txtTxt_Addr2";
            this.txtTxt_Addr2.Size = new System.Drawing.Size(443, 19);
            this.txtTxt_Addr2.TabIndex = 19;
            // 
            // txtTxt_Addr3
            // 
            this.txtTxt_Addr3.Location = new System.Drawing.Point(92, 196);
            this.txtTxt_Addr3.MaxLength = 100;
            this.txtTxt_Addr3.Name = "txtTxt_Addr3";
            this.txtTxt_Addr3.Size = new System.Drawing.Size(443, 19);
            this.txtTxt_Addr3.TabIndex = 20;
            // 
            // txtTxt_Tel
            // 
            this.txtTxt_Tel.Location = new System.Drawing.Point(92, 218);
            this.txtTxt_Tel.MaxLength = 30;
            this.txtTxt_Tel.Name = "txtTxt_Tel";
            this.txtTxt_Tel.Size = new System.Drawing.Size(196, 19);
            this.txtTxt_Tel.TabIndex = 21;
            // 
            // txtTxt_Fax
            // 
            this.txtTxt_Fax.Location = new System.Drawing.Point(92, 240);
            this.txtTxt_Fax.MaxLength = 30;
            this.txtTxt_Fax.Name = "txtTxt_Fax";
            this.txtTxt_Fax.Size = new System.Drawing.Size(196, 19);
            this.txtTxt_Fax.TabIndex = 22;
            // 
            // txtTxt_Rem
            // 
            this.txtTxt_Rem.Location = new System.Drawing.Point(92, 262);
            this.txtTxt_Rem.MaxLength = 500;
            this.txtTxt_Rem.Multiline = true;
            this.txtTxt_Rem.Name = "txtTxt_Rem";
            this.txtTxt_Rem.Size = new System.Drawing.Size(480, 114);
            this.txtTxt_Rem.TabIndex = 23;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(489, 407);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegister.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRegister.Location = new System.Drawing.Point(408, 407);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 31;
            this.btnRegister.Text = "登録";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDelete.Location = new System.Drawing.Point(27, 407);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 30;
            this.btnDelete.Text = "削除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbCdCo
            // 
            this.cmbCdCo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCdCo.FormattingEnabled = true;
            this.cmbCdCo.Location = new System.Drawing.Point(92, 20);
            this.cmbCdCo.Name = "cmbCdCo";
            this.cmbCdCo.Size = new System.Drawing.Size(196, 20);
            this.cmbCdCo.TabIndex = 33;
            // 
            // txtNm_Dept
            // 
            this.txtNm_Dept.Location = new System.Drawing.Point(158, 108);
            this.txtNm_Dept.Name = "txtNm_Dept";
            this.txtNm_Dept.Size = new System.Drawing.Size(377, 19);
            this.txtNm_Dept.TabIndex = 34;
            // 
            // btnDept
            // 
            this.btnDept.Location = new System.Drawing.Point(539, 106);
            this.btnDept.Name = "btnDept";
            this.btnDept.Size = new System.Drawing.Size(33, 23);
            this.btnDept.TabIndex = 35;
            this.btnDept.Text = "...";
            this.btnDept.UseVisualStyleBackColor = true;
            this.btnDept.Click += new System.EventHandler(this.btnDept_Click);
            // 
            // MembersEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 442);
            this.ControlBox = false;
            this.Controls.Add(this.btnDept);
            this.Controls.Add(this.txtNm_Dept);
            this.Controls.Add(this.cmbCdCo);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtTxt_Rem);
            this.Controls.Add(this.txtTxt_Fax);
            this.Controls.Add(this.txtTxt_Tel);
            this.Controls.Add(this.txtTxt_Addr3);
            this.Controls.Add(this.txtTxt_Addr2);
            this.Controls.Add(this.txtTxt_Addr1);
            this.Controls.Add(this.txtTxt_Zip);
            this.Controls.Add(this.txtCd_Dept);
            this.Controls.Add(this.txtTxt_Passwd);
            this.Controls.Add(this.txtNm_Emp);
            this.Controls.Add(this.txtCd_Emp);
            this.Controls.Add(this.lblTxt_Rem);
            this.Controls.Add(this.lblTxt_Fax);
            this.Controls.Add(this.lblTxt_Tel);
            this.Controls.Add(this.lblTxt_Addr3);
            this.Controls.Add(this.lblTxt_Addr2);
            this.Controls.Add(this.lblTxt_Addr1);
            this.Controls.Add(this.lblTxt_Zip);
            this.Controls.Add(this.lblCd_Dept);
            this.Controls.Add(this.lblTxt_Passwd);
            this.Controls.Add(this.lblNm_Emp);
            this.Controls.Add(this.lblCd_Emp);
            this.Controls.Add(this.lblCd_Co);
            this.Name = "MembersEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "社員新規作成画面";
            this.Load += new System.EventHandler(this.MembersEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCd_Co;
        private System.Windows.Forms.Label lblCd_Emp;
        private System.Windows.Forms.Label lblNm_Emp;
        private System.Windows.Forms.Label lblTxt_Passwd;
        private System.Windows.Forms.Label lblCd_Dept;
        private System.Windows.Forms.Label lblTxt_Zip;
        private System.Windows.Forms.Label lblTxt_Addr1;
        private System.Windows.Forms.Label lblTxt_Addr2;
        private System.Windows.Forms.Label lblTxt_Addr3;
        private System.Windows.Forms.Label lblTxt_Tel;
        private System.Windows.Forms.Label lblTxt_Fax;
        private System.Windows.Forms.Label lblTxt_Rem;
        private System.Windows.Forms.TextBox txtCd_Emp;
        private System.Windows.Forms.TextBox txtNm_Emp;
        private System.Windows.Forms.TextBox txtTxt_Passwd;
        private System.Windows.Forms.TextBox txtCd_Dept;
        private System.Windows.Forms.TextBox txtTxt_Zip;
        private System.Windows.Forms.TextBox txtTxt_Addr1;
        private System.Windows.Forms.TextBox txtTxt_Addr2;
        private System.Windows.Forms.TextBox txtTxt_Addr3;
        private System.Windows.Forms.TextBox txtTxt_Tel;
        private System.Windows.Forms.TextBox txtTxt_Fax;
        private System.Windows.Forms.TextBox txtTxt_Rem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cmbCdCo;
        private System.Windows.Forms.TextBox txtNm_Dept;
        private System.Windows.Forms.Button btnDept;
    }
}