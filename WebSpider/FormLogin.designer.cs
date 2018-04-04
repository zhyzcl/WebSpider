namespace WebSpider
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.cBName = new System.Windows.Forms.ComboBox();
            this.tPwd = new System.Windows.Forms.TextBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.bLogin = new System.Windows.Forms.Button();
            this.pBox = new System.Windows.Forms.PictureBox();
            this.cBsave = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cBName
            // 
            this.cBName.FormattingEnabled = true;
            this.cBName.Location = new System.Drawing.Point(134, 101);
            this.cBName.Name = "cBName";
            this.cBName.Size = new System.Drawing.Size(161, 20);
            this.cBName.TabIndex = 14;
            this.cBName.Leave += new System.EventHandler(this.cBName_Leave);
            // 
            // tPwd
            // 
            this.tPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tPwd.Location = new System.Drawing.Point(134, 155);
            this.tPwd.MaxLength = 25;
            this.tPwd.Name = "tPwd";
            this.tPwd.Size = new System.Drawing.Size(161, 21);
            this.tPwd.TabIndex = 9;
            this.tPwd.UseSystemPasswordChar = true;
            this.tPwd.Leave += new System.EventHandler(this.tPwd_Leave);
            // 
            // bCancel
            // 
            this.bCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bCancel.Location = new System.Drawing.Point(243, 206);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(86, 28);
            this.bCancel.TabIndex = 11;
            this.bCancel.Text = "取 消";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // bLogin
            // 
            this.bLogin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bLogin.Location = new System.Drawing.Point(47, 206);
            this.bLogin.Name = "bLogin";
            this.bLogin.Size = new System.Drawing.Size(86, 28);
            this.bLogin.TabIndex = 10;
            this.bLogin.Text = "确 定";
            this.bLogin.UseVisualStyleBackColor = true;
            this.bLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pBox
            // 
            this.pBox.Image = ((System.Drawing.Image)(resources.GetObject("pBox.Image")));
            this.pBox.Location = new System.Drawing.Point(3, 2);
            this.pBox.Name = "pBox";
            this.pBox.Size = new System.Drawing.Size(376, 255);
            this.pBox.TabIndex = 12;
            this.pBox.TabStop = false;
            // 
            // cBsave
            // 
            this.cBsave.AutoSize = true;
            this.cBsave.Location = new System.Drawing.Point(152, 214);
            this.cBsave.Name = "cBsave";
            this.cBsave.Size = new System.Drawing.Size(72, 16);
            this.cBsave.TabIndex = 15;
            this.cBsave.Text = "记住密码";
            this.cBsave.UseVisualStyleBackColor = true;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 256);
            this.Controls.Add(this.cBsave);
            this.Controls.Add(this.cBName);
            this.Controls.Add(this.tPwd);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bLogin);
            this.Controls.Add(this.pBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(391, 295);
            this.MinimumSize = new System.Drawing.Size(391, 295);
            this.Name = "FormLogin";
            this.Text = "网络蜘蛛﹥登录通道";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLogin_FormClosed);
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.Shown += new System.EventHandler(this.FrmLogin_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cBName;
        private System.Windows.Forms.TextBox tPwd;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bLogin;
        private System.Windows.Forms.PictureBox pBox;
        private System.Windows.Forms.CheckBox cBsave;
    }
}