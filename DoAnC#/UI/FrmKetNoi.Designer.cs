namespace DoAnC_.UI
{
    partial class FrmKetNoi
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
            this.lblTDKNCSDL = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cobStatus = new System.Windows.Forms.ComboBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblServerNameKNDL = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblUserNameKNDL = new System.Windows.Forms.Label();
            this.lblPassKNDL = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.btnKetNoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTDKNCSDL
            // 
            this.lblTDKNCSDL.BackColor = System.Drawing.Color.Transparent;
            this.lblTDKNCSDL.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTDKNCSDL.Location = new System.Drawing.Point(197, 49);
            this.lblTDKNCSDL.Name = "lblTDKNCSDL";
            this.lblTDKNCSDL.Size = new System.Drawing.Size(233, 33);
            this.lblTDKNCSDL.TabIndex = 0;
            this.lblTDKNCSDL.Text = "Kết Nối Cơ Sở Dữ Liệu";
            // 
            // cobStatus
            // 
            this.cobStatus.FormattingEnabled = true;
            this.cobStatus.Items.AddRange(new object[] {
            "Windows Authentication",
            "SQL Server Authentication"});
            this.cobStatus.Location = new System.Drawing.Point(248, 169);
            this.cobStatus.Name = "cobStatus";
            this.cobStatus.Size = new System.Drawing.Size(229, 24);
            this.cobStatus.TabIndex = 3;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(248, 126);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(229, 22);
            this.txtServerName.TabIndex = 4;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(248, 217);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(229, 22);
            this.txtUserName.TabIndex = 4;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(248, 260);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(229, 22);
            this.txtPass.TabIndex = 4;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(248, 304);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(229, 22);
            this.txtDatabase.TabIndex = 4;
            // 
            // lblServerNameKNDL
            // 
            this.lblServerNameKNDL.AutoSize = true;
            this.lblServerNameKNDL.Location = new System.Drawing.Point(158, 129);
            this.lblServerNameKNDL.Name = "lblServerNameKNDL";
            this.lblServerNameKNDL.Size = new System.Drawing.Size(84, 16);
            this.lblServerNameKNDL.TabIndex = 5;
            this.lblServerNameKNDL.Text = "ServerName";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(158, 172);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status";
            // 
            // lblUserNameKNDL
            // 
            this.lblUserNameKNDL.AutoSize = true;
            this.lblUserNameKNDL.Location = new System.Drawing.Point(158, 220);
            this.lblUserNameKNDL.Name = "lblUserNameKNDL";
            this.lblUserNameKNDL.Size = new System.Drawing.Size(73, 16);
            this.lblUserNameKNDL.TabIndex = 5;
            this.lblUserNameKNDL.Text = "UserName";
            // 
            // lblPassKNDL
            // 
            this.lblPassKNDL.AutoSize = true;
            this.lblPassKNDL.Location = new System.Drawing.Point(158, 263);
            this.lblPassKNDL.Name = "lblPassKNDL";
            this.lblPassKNDL.Size = new System.Drawing.Size(38, 16);
            this.lblPassKNDL.TabIndex = 5;
            this.lblPassKNDL.Text = "Pass";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(158, 307);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(67, 16);
            this.lblDatabase.TabIndex = 5;
            this.lblDatabase.Text = "Database";
            // 
            // btnKetNoi
            // 
            this.btnKetNoi.Location = new System.Drawing.Point(355, 352);
            this.btnKetNoi.Name = "btnKetNoi";
            this.btnKetNoi.Size = new System.Drawing.Size(75, 23);
            this.btnKetNoi.TabIndex = 6;
            this.btnKetNoi.Text = "Kết Nối";
            this.btnKetNoi.UseVisualStyleBackColor = true;
            this.btnKetNoi.Click += new System.EventHandler(this.btnKetNoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(214, 352);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // FrmKetNoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 412);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnKetNoi);
            this.Controls.Add(this.lblDatabase);
            this.Controls.Add(this.lblPassKNDL);
            this.Controls.Add(this.lblUserNameKNDL);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblServerNameKNDL);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.cobStatus);
            this.Controls.Add(this.lblTDKNCSDL);
            this.Name = "FrmKetNoi";
            this.Text = "FrmKetNoi";
            this.Load += new System.EventHandler(this.FrmKetNoi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTDKNCSDL;
        private System.Windows.Forms.ComboBox cobStatus;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblServerNameKNDL;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblUserNameKNDL;
        private System.Windows.Forms.Label lblPassKNDL;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Button btnKetNoi;
        private System.Windows.Forms.Button btnLuu;
    }
}