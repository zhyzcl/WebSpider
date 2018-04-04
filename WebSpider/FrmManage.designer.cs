namespace WebSpider
{
    partial class FrmManage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManage));
            this.tssltotime = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuEixt = new System.Windows.Forms.ToolStripMenuItem();
            this.statusSa = new System.Windows.Forms.StatusStrip();
            this.toolStatusLab2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStatusLab6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssluserinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssldatetime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timea = new System.Windows.Forms.Timer(this.components);
            this.mStrip = new System.Windows.Forms.MenuStrip();
            this.MenuUserManage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.btdel = new System.Windows.Forms.Button();
            this.btadd = new System.Windows.Forms.Button();
            this.wslist = new System.Windows.Forms.ListView();
            this.carcha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carche = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchh = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchi = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchj = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btsort = new System.Windows.Forms.Button();
            this.statusSa.SuspendLayout();
            this.mStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tssltotime
            // 
            this.tssltotime.Name = "tssltotime";
            this.tssltotime.Size = new System.Drawing.Size(64, 17);
            this.tssltotime.Text = "tssltotime";
            // 
            // MenuEixt
            // 
            this.MenuEixt.Name = "MenuEixt";
            this.MenuEixt.Size = new System.Drawing.Size(44, 21);
            this.MenuEixt.Text = "退出";
            this.MenuEixt.ToolTipText = "退出系统";
            this.MenuEixt.Click += new System.EventHandler(this.MenuEixt_Click);
            // 
            // statusSa
            // 
            this.statusSa.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatusLab2,
            this.toolStatusLab6,
            this.tssluserinfo,
            this.tssldatetime,
            this.tssltotime});
            this.statusSa.Location = new System.Drawing.Point(0, 524);
            this.statusSa.Name = "statusSa";
            this.statusSa.Size = new System.Drawing.Size(984, 22);
            this.statusSa.TabIndex = 18;
            // 
            // toolStatusLab2
            // 
            this.toolStatusLab2.Name = "toolStatusLab2";
            this.toolStatusLab2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStatusLab6
            // 
            this.toolStatusLab6.Name = "toolStatusLab6";
            this.toolStatusLab6.Size = new System.Drawing.Size(0, 17);
            // 
            // tssluserinfo
            // 
            this.tssluserinfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tssluserinfo.Name = "tssluserinfo";
            this.tssluserinfo.Size = new System.Drawing.Size(74, 17);
            this.tssluserinfo.Text = "tssluserinfo";
            // 
            // tssldatetime
            // 
            this.tssldatetime.Name = "tssldatetime";
            this.tssldatetime.Size = new System.Drawing.Size(68, 17);
            this.tssldatetime.Text = "现在时间：";
            // 
            // timea
            // 
            this.timea.Enabled = true;
            this.timea.Interval = 200;
            this.timea.Tick += new System.EventHandler(this.timea_Tick);
            // 
            // mStrip
            // 
            this.mStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuUserManage,
            this.MenuLogin,
            this.MenuEixt});
            this.mStrip.Location = new System.Drawing.Point(0, 0);
            this.mStrip.Name = "mStrip";
            this.mStrip.Size = new System.Drawing.Size(984, 25);
            this.mStrip.TabIndex = 17;
            this.mStrip.Text = "menuStrip1";
            // 
            // MenuUserManage
            // 
            this.MenuUserManage.Name = "MenuUserManage";
            this.MenuUserManage.Size = new System.Drawing.Size(68, 21);
            this.MenuUserManage.Text = "用户管理";
            this.MenuUserManage.Click += new System.EventHandler(this.MenuUserManage_Click);
            // 
            // MenuLogin
            // 
            this.MenuLogin.Name = "MenuLogin";
            this.MenuLogin.Size = new System.Drawing.Size(44, 21);
            this.MenuLogin.Text = "注销";
            this.MenuLogin.Click += new System.EventHandler(this.MenuLogin_Click);
            // 
            // btdel
            // 
            this.btdel.Location = new System.Drawing.Point(743, 33);
            this.btdel.Name = "btdel";
            this.btdel.Size = new System.Drawing.Size(93, 23);
            this.btdel.TabIndex = 21;
            this.btdel.Text = "删除";
            this.btdel.UseVisualStyleBackColor = true;
            this.btdel.Click += new System.EventHandler(this.btdel_Click);
            // 
            // btadd
            // 
            this.btadd.Location = new System.Drawing.Point(611, 33);
            this.btadd.Name = "btadd";
            this.btadd.Size = new System.Drawing.Size(93, 23);
            this.btadd.TabIndex = 20;
            this.btadd.Text = "新增网络蜘蛛";
            this.btadd.UseVisualStyleBackColor = true;
            this.btadd.Click += new System.EventHandler(this.btadd_Click);
            // 
            // wslist
            // 
            this.wslist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.carcha,
            this.carchc,
            this.carchd,
            this.carche,
            this.carchf,
            this.carchg,
            this.carchh,
            this.carchi,
            this.carchj});
            this.wslist.FullRowSelect = true;
            this.wslist.GridLines = true;
            this.wslist.Location = new System.Drawing.Point(14, 62);
            this.wslist.MultiSelect = false;
            this.wslist.Name = "wslist";
            this.wslist.Size = new System.Drawing.Size(954, 450);
            this.wslist.TabIndex = 19;
            this.wslist.UseCompatibleStateImageBehavior = false;
            this.wslist.View = System.Windows.Forms.View.Details;
            this.wslist.DoubleClick += new System.EventHandler(this.wslist_DoubleClick);
            // 
            // carcha
            // 
            this.carcha.Text = "序号";
            this.carcha.Width = 63;
            // 
            // carchc
            // 
            this.carchc.Text = "网络蜘蛛名称";
            this.carchc.Width = 193;
            // 
            // carchd
            // 
            this.carchd.Text = "Url列表";
            this.carchd.Width = 147;
            // 
            // carche
            // 
            this.carche.Text = "Excel文件保存位置";
            this.carche.Width = 125;
            // 
            // carchf
            // 
            this.carchf.Text = "是否保存html";
            this.carchf.Width = 91;
            // 
            // carchg
            // 
            this.carchg.Text = "html文件保存位置";
            this.carchg.Width = 122;
            // 
            // carchh
            // 
            this.carchh.Text = "最近采集日期";
            this.carchh.Width = 106;
            // 
            // carchi
            // 
            this.carchi.Text = "采集总次数";
            this.carchi.Width = 99;
            // 
            // carchj
            // 
            this.carchj.Text = "ids";
            this.carchj.Width = 0;
            // 
            // btsort
            // 
            this.btsort.Location = new System.Drawing.Point(875, 33);
            this.btsort.Name = "btsort";
            this.btsort.Size = new System.Drawing.Size(93, 23);
            this.btsort.TabIndex = 22;
            this.btsort.Text = "重新排序";
            this.btsort.UseVisualStyleBackColor = true;
            this.btsort.Click += new System.EventHandler(this.btsort_Click);
            // 
            // FrmManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 546);
            this.Controls.Add(this.btsort);
            this.Controls.Add(this.btdel);
            this.Controls.Add(this.btadd);
            this.Controls.Add(this.wslist);
            this.Controls.Add(this.statusSa);
            this.Controls.Add(this.mStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 585);
            this.MinimumSize = new System.Drawing.Size(1000, 585);
            this.Name = "FrmManage";
            this.Text = "网络蜘蛛";
            this.Activated += new System.EventHandler(this.PayManage_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_FormClosed);
            this.Load += new System.EventHandler(this.PayManage_Load);
            this.statusSa.ResumeLayout(false);
            this.statusSa.PerformLayout();
            this.mStrip.ResumeLayout(false);
            this.mStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel tssltotime;
        private System.Windows.Forms.ToolStripMenuItem MenuEixt;
        private System.Windows.Forms.StatusStrip statusSa;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLab2;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLab6;
        private System.Windows.Forms.ToolStripStatusLabel tssluserinfo;
        private System.Windows.Forms.ToolStripStatusLabel tssldatetime;
        private System.Windows.Forms.Timer timea;
        private System.Windows.Forms.MenuStrip mStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuLogin;
        private System.Windows.Forms.ToolStripMenuItem MenuUserManage;
        private System.Windows.Forms.Button btdel;
        private System.Windows.Forms.Button btadd;
        private System.Windows.Forms.ListView wslist;
        private System.Windows.Forms.ColumnHeader carcha;
        private System.Windows.Forms.ColumnHeader carchc;
        private System.Windows.Forms.ColumnHeader carchd;
        private System.Windows.Forms.ColumnHeader carche;
        private System.Windows.Forms.ColumnHeader carchf;
        private System.Windows.Forms.ColumnHeader carchg;
        private System.Windows.Forms.ColumnHeader carchh;
        private System.Windows.Forms.ColumnHeader carchi;
        private System.Windows.Forms.ColumnHeader carchj;
        private System.Windows.Forms.Button btsort;
    }
}