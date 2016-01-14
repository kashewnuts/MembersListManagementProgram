namespace MembersListManagementProgram
{
    partial class LoginForm
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
            this.lblCd_Emp = new System.Windows.Forms.Label();
            this.lblTxt_Passwd = new System.Windows.Forms.Label();
            this.lblCd_Co = new System.Windows.Forms.Label();
            this.txtCd_Co = new System.Windows.Forms.TextBox();
            this.txtCd_Emp = new System.Windows.Forms.TextBox();
            this.txtTxt_Passwd = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCd_Emp
            // 
            this.lblCd_Emp.AutoSize = true;
            this.lblCd_Emp.Location = new System.Drawing.Point(29, 65);
            this.lblCd_Emp.Name = "lblCd_Emp";
            this.lblCd_Emp.Size = new System.Drawing.Size(56, 12);
            this.lblCd_Emp.TabIndex = 0;
            this.lblCd_Emp.Text = "社員コード";
            // 
            // lblTxt_Passwd
            // 
            this.lblTxt_Passwd.AutoSize = true;
            this.lblTxt_Passwd.Location = new System.Drawing.Point(29, 93);
            this.lblTxt_Passwd.Name = "lblTxt_Passwd";
            this.lblTxt_Passwd.Size = new System.Drawing.Size(52, 12);
            this.lblTxt_Passwd.TabIndex = 1;
            this.lblTxt_Passwd.Text = "パスワード";
            // 
            // lblCd_Co
            // 
            this.lblCd_Co.AutoSize = true;
            this.lblCd_Co.Location = new System.Drawing.Point(29, 37);
            this.lblCd_Co.Name = "lblCd_Co";
            this.lblCd_Co.Size = new System.Drawing.Size(56, 12);
            this.lblCd_Co.TabIndex = 2;
            this.lblCd_Co.Text = "会社コード";
            // 
            // txtCd_Co
            // 
            this.txtCd_Co.Location = new System.Drawing.Point(91, 34);
            this.txtCd_Co.Name = "txtCd_Co";
            this.txtCd_Co.Size = new System.Drawing.Size(193, 19);
            this.txtCd_Co.TabIndex = 3;
            // 
            // txtCd_Emp
            // 
            this.txtCd_Emp.Location = new System.Drawing.Point(91, 62);
            this.txtCd_Emp.Name = "txtCd_Emp";
            this.txtCd_Emp.Size = new System.Drawing.Size(193, 19);
            this.txtCd_Emp.TabIndex = 4;
            // 
            // txtTxt_Passwd
            // 
            this.txtTxt_Passwd.Location = new System.Drawing.Point(91, 90);
            this.txtTxt_Passwd.Name = "txtTxt_Passwd";
            this.txtTxt_Passwd.PasswordChar = '*';
            this.txtTxt_Passwd.Size = new System.Drawing.Size(193, 19);
            this.txtTxt_Passwd.TabIndex = 5;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Location = new System.Drawing.Point(133, 153);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "ログイン";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLlogin_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 212);
            this.ControlBox = false;
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtTxt_Passwd);
            this.Controls.Add(this.txtCd_Emp);
            this.Controls.Add(this.txtCd_Co);
            this.Controls.Add(this.lblCd_Co);
            this.Controls.Add(this.lblTxt_Passwd);
            this.Controls.Add(this.lblCd_Emp);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCd_Emp;
        private System.Windows.Forms.Label lblTxt_Passwd;
        private System.Windows.Forms.Label lblCd_Co;
        private System.Windows.Forms.TextBox txtTxt_Passwd;
        private System.Windows.Forms.Button btnLogin;
        internal System.Windows.Forms.TextBox txtCd_Co;
        internal System.Windows.Forms.TextBox txtCd_Emp;
    }
}