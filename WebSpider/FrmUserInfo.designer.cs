namespace WebSpider
{
    partial class FrmUserInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserInfo));
            this.tbphone = new System.Windows.Forms.TextBox();
            this.tbrealName = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbfPwd = new System.Windows.Forms.TextBox();
            this.lqpwd = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btexit = new System.Windows.Forms.Button();
            this.btsave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbphone
            // 
            this.tbphone.Location = new System.Drawing.Point(98, 174);
            this.tbphone.MaxLength = 50;
            this.tbphone.Name = "tbphone";
            this.tbphone.Size = new System.Drawing.Size(175, 21);
            this.tbphone.TabIndex = 60;
            // 
            // tbrealName
            // 
            this.tbrealName.Location = new System.Drawing.Point(98, 133);
            this.tbrealName.MaxLength = 50;
            this.tbrealName.Name = "tbrealName";
            this.tbrealName.Size = new System.Drawing.Size(175, 21);
            this.tbrealName.TabIndex = 58;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.Location = new System.Drawing.Point(33, 178);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(57, 12);
            this.label30.TabIndex = 55;
            this.label30.Text = "联系电话";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.Location = new System.Drawing.Point(56, 137);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(31, 12);
            this.label31.TabIndex = 54;
            this.label31.Text = "姓名";
            // 
            // tbPwd
            // 
            this.tbPwd.Location = new System.Drawing.Point(98, 57);
            this.tbPwd.MaxLength = 25;
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.Size = new System.Drawing.Size(175, 21);
            this.tbPwd.TabIndex = 76;
            this.tbPwd.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(56, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 12);
            this.label6.TabIndex = 75;
            this.label6.Text = "密码";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(98, 23);
            this.tbName.MaxLength = 25;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(175, 21);
            this.tbName.TabIndex = 78;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(43, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 77;
            this.label7.Text = "用户名";
            // 
            // tbfPwd
            // 
            this.tbfPwd.Location = new System.Drawing.Point(98, 93);
            this.tbfPwd.MaxLength = 25;
            this.tbfPwd.Name = "tbfPwd";
            this.tbfPwd.Size = new System.Drawing.Size(175, 21);
            this.tbfPwd.TabIndex = 83;
            this.tbfPwd.UseSystemPasswordChar = true;
            // 
            // lqpwd
            // 
            this.lqpwd.AutoSize = true;
            this.lqpwd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lqpwd.Location = new System.Drawing.Point(33, 97);
            this.lqpwd.Name = "lqpwd";
            this.lqpwd.Size = new System.Drawing.Size(57, 12);
            this.lqpwd.TabIndex = 82;
            this.lqpwd.Text = "确认密码";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btexit);
            this.groupBox1.Controls.Add(this.btsave);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.tbfPwd);
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.lqpwd);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tbrealName);
            this.groupBox1.Controls.Add(this.tbPwd);
            this.groupBox1.Controls.Add(this.tbphone);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(16, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(315, 269);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            // 
            // btexit
            // 
            this.btexit.Location = new System.Drawing.Point(179, 220);
            this.btexit.Name = "btexit";
            this.btexit.Size = new System.Drawing.Size(75, 23);
            this.btexit.TabIndex = 85;
            this.btexit.Text = "取消";
            this.btexit.UseVisualStyleBackColor = true;
            this.btexit.Click += new System.EventHandler(this.btexit_Click);
            // 
            // btsave
            // 
            this.btsave.Location = new System.Drawing.Point(54, 220);
            this.btsave.Name = "btsave";
            this.btsave.Size = new System.Drawing.Size(75, 23);
            this.btsave.TabIndex = 84;
            this.btsave.Text = "保存";
            this.btsave.UseVisualStyleBackColor = true;
            this.btsave.Click += new System.EventHandler(this.btsave_Click);
            // 
            // FrmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 289);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(362, 328);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(362, 328);
            this.Name = "FrmUserInfo";
            this.Text = "添加/修改登录用户";
            this.Load += new System.EventHandler(this.FrmUserInfo_Load);
            this.Shown += new System.EventHandler(this.FrmUserInfo_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox tbphone;
        private System.Windows.Forms.TextBox tbrealName;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbfPwd;
        private System.Windows.Forms.Label lqpwd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btexit;
        private System.Windows.Forms.Button btsave;

    }
}