namespace MembersListManagementProgram
{
	partial class DepartmentEditForm
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
			this.lblCd_Dept = new System.Windows.Forms.Label();
			this.lblTxt_Rem = new System.Windows.Forms.Label();
			this.btnCansel = new System.Windows.Forms.Button();
			this.btnRegister = new System.Windows.Forms.Button();
			this.txtNm_Dept = new System.Windows.Forms.TextBox();
			this.txtTxt_Rem = new System.Windows.Forms.TextBox();
			this.lblCd_Co = new System.Windows.Forms.Label();
			this.btnDelete = new System.Windows.Forms.Button();
			this.cmbCdCo = new System.Windows.Forms.ComboBox();
			this.txtCd_Dept = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lblCd_Dept
			// 
			this.lblCd_Dept.AutoSize = true;
			this.lblCd_Dept.Location = new System.Drawing.Point(9, 62);
			this.lblCd_Dept.Name = "lblCd_Dept";
			this.lblCd_Dept.Size = new System.Drawing.Size(56, 12);
			this.lblCd_Dept.TabIndex = 1;
			this.lblCd_Dept.Text = "部門コード";
			// 
			// lblTxt_Rem
			// 
			this.lblTxt_Rem.AutoSize = true;
			this.lblTxt_Rem.Location = new System.Drawing.Point(9, 87);
			this.lblTxt_Rem.Name = "lblTxt_Rem";
			this.lblTxt_Rem.Size = new System.Drawing.Size(29, 12);
			this.lblTxt_Rem.TabIndex = 3;
			this.lblTxt_Rem.Text = "備考";
			// 
			// btnCansel
			// 
			this.btnCansel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCansel.Location = new System.Drawing.Point(477, 407);
			this.btnCansel.Name = "btnCansel";
			this.btnCansel.Size = new System.Drawing.Size(75, 23);
			this.btnCansel.TabIndex = 17;
			this.btnCansel.Text = "キャンセル";
			this.btnCansel.UseVisualStyleBackColor = true;
			this.btnCansel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnRegister
			// 
			this.btnRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRegister.Location = new System.Drawing.Point(397, 407);
			this.btnRegister.Name = "btnRegister";
			this.btnRegister.Size = new System.Drawing.Size(75, 23);
			this.btnRegister.TabIndex = 16;
			this.btnRegister.Text = "登録";
			this.btnRegister.UseVisualStyleBackColor = true;
			this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
			// 
			// txtNm_Dept
			// 
			this.txtNm_Dept.Location = new System.Drawing.Point(154, 59);
			this.txtNm_Dept.MaxLength = 100;
			this.txtNm_Dept.Name = "txtNm_Dept";
			this.txtNm_Dept.Size = new System.Drawing.Size(408, 19);
			this.txtNm_Dept.TabIndex = 12;
			// 
			// txtTxt_Rem
			// 
			this.txtTxt_Rem.Location = new System.Drawing.Point(82, 84);
			this.txtTxt_Rem.MaxLength = 500;
			this.txtTxt_Rem.Multiline = true;
			this.txtTxt_Rem.Name = "txtTxt_Rem";
			this.txtTxt_Rem.Size = new System.Drawing.Size(480, 200);
			this.txtTxt_Rem.TabIndex = 14;
			// 
			// lblCd_Co
			// 
			this.lblCd_Co.AutoSize = true;
			this.lblCd_Co.Location = new System.Drawing.Point(9, 33);
			this.lblCd_Co.Name = "lblCd_Co";
			this.lblCd_Co.Size = new System.Drawing.Size(56, 12);
			this.lblCd_Co.TabIndex = 14;
			this.lblCd_Co.Text = "会社コード";
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.Location = new System.Drawing.Point(13, 406);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 15;
			this.btnDelete.Text = "削除";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// cmbCdCo
			// 
			this.cmbCdCo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCdCo.FormattingEnabled = true;
			this.cmbCdCo.Location = new System.Drawing.Point(82, 30);
			this.cmbCdCo.Name = "cmbCdCo";
			this.cmbCdCo.Size = new System.Drawing.Size(480, 20);
			this.cmbCdCo.TabIndex = 10;
			// 
			// txtCd_Dept
			// 
			this.txtCd_Dept.Location = new System.Drawing.Point(82, 59);
			this.txtCd_Dept.MaxLength = 30;
			this.txtCd_Dept.Name = "txtCd_Dept";
			this.txtCd_Dept.Size = new System.Drawing.Size(66, 19);
			this.txtCd_Dept.TabIndex = 11;
			// 
			// DepartmentEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 442);
			this.ControlBox = false;
			this.Controls.Add(this.cmbCdCo);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.lblCd_Co);
			this.Controls.Add(this.txtTxt_Rem);
			this.Controls.Add(this.txtNm_Dept);
			this.Controls.Add(this.txtCd_Dept);
			this.Controls.Add(this.btnRegister);
			this.Controls.Add(this.btnCansel);
			this.Controls.Add(this.lblTxt_Rem);
			this.Controls.Add(this.lblCd_Dept);
			this.KeyPreview = true;
			this.Name = "DepartmentEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "部門マスタ新規作成画面";
			this.Load += new System.EventHandler(this.DepartmentEditForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblCd_Dept;
		private System.Windows.Forms.Label lblTxt_Rem;
		private System.Windows.Forms.Button btnCansel;
		private System.Windows.Forms.Button btnRegister;
		private System.Windows.Forms.TextBox txtNm_Dept;
		private System.Windows.Forms.TextBox txtTxt_Rem;
		private System.Windows.Forms.Label lblCd_Co;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.ComboBox cmbCdCo;
		private System.Windows.Forms.TextBox txtCd_Dept;
	}
}