namespace WebSpider
{
    partial class FrmUserManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserManage));
            this.userlist = new System.Windows.Forms.ListView();
            this.carcha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carche = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.linfo = new System.Windows.Forms.Label();
            this.btadd = new System.Windows.Forms.Button();
            this.btdel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userlist
            // 
            this.userlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.carcha,
            this.carchc,
            this.carchd,
            this.carche,
            this.carchf,
            this.carchg});
            this.userlist.FullRowSelect = true;
            this.userlist.GridLines = true;
            this.userlist.Location = new System.Drawing.Point(12, 15);
            this.userlist.MultiSelect = false;
            this.userlist.Name = "userlist";
            this.userlist.Size = new System.Drawing.Size(646, 314);
            this.userlist.TabIndex = 13;
            this.userlist.UseCompatibleStateImageBehavior = false;
            this.userlist.View = System.Windows.Forms.View.Details;
            this.userlist.DoubleClick += new System.EventHandler(this.userlist_DoubleClick);
            // 
            // carcha
            // 
            this.carcha.Text = "用户名";
            this.carcha.Width = 100;
            // 
            // carchc
            // 
            this.carchc.Text = "姓名";
            this.carchc.Width = 100;
            // 
            // carchd
            // 
            this.carchd.Text = "权限级别";
            this.carchd.Width = 90;
            // 
            // carche
            // 
            this.carche.Text = "最后登录日期";
            this.carche.Width = 120;
            // 
            // carchf
            // 
            this.carchf.Text = "登录次数";
            this.carchf.Width = 70;
            // 
            // carchg
            // 
            this.carchg.Text = "联系电话";
            this.carchg.Width = 150;
            // 
            // linfo
            // 
            this.linfo.AutoSize = true;
            this.linfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linfo.ForeColor = System.Drawing.Color.Blue;
            this.linfo.Location = new System.Drawing.Point(52, 476);
            this.linfo.Name = "linfo";
            this.linfo.Size = new System.Drawing.Size(0, 16);
            this.linfo.TabIndex = 12;
            // 
            // btadd
            // 
            this.btadd.Location = new System.Drawing.Point(452, 344);
            this.btadd.Name = "btadd";
            this.btadd.Size = new System.Drawing.Size(75, 23);
            this.btadd.TabIndex = 14;
            this.btadd.Text = "添加用户";
            this.btadd.UseVisualStyleBackColor = true;
            this.btadd.Click += new System.EventHandler(this.btadd_Click);
            // 
            // btdel
            // 
            this.btdel.Location = new System.Drawing.Point(568, 344);
            this.btdel.Name = "btdel";
            this.btdel.Size = new System.Drawing.Size(75, 23);
            this.btdel.TabIndex = 15;
            this.btdel.Text = "删除用户";
            this.btdel.UseVisualStyleBackColor = true;
            this.btdel.Click += new System.EventHandler(this.btdel_Click);
            // 
            // FrmUserManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 379);
            this.Controls.Add(this.btdel);
            this.Controls.Add(this.btadd);
            this.Controls.Add(this.userlist);
            this.Controls.Add(this.linfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(686, 418);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(686, 418);
            this.Name = "FrmUserManage";
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.FrmLeave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label linfo;
        private System.Windows.Forms.ListView userlist;
        private System.Windows.Forms.ColumnHeader carcha;
        private System.Windows.Forms.ColumnHeader carchc;
        private System.Windows.Forms.ColumnHeader carchd;
        private System.Windows.Forms.ColumnHeader carche;
        private System.Windows.Forms.ColumnHeader carchf;
        private System.Windows.Forms.Button btadd;
        private System.Windows.Forms.Button btdel;
        private System.Windows.Forms.ColumnHeader carchg;
    }
}