namespace WebSpider
{
    partial class FrmWebSpiderList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWebSpiderList));
            this.contentlist = new System.Windows.Forms.ListView();
            this.carcha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carche = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.carchf = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.linfo = new System.Windows.Forms.Label();
            this.btadd = new System.Windows.Forms.Button();
            this.btdel = new System.Windows.Forms.Button();
            this.btsort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contentlist
            // 
            this.contentlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.carcha,
            this.carchc,
            this.carchd,
            this.carche,
            this.carchf});
            this.contentlist.FullRowSelect = true;
            this.contentlist.GridLines = true;
            this.contentlist.Location = new System.Drawing.Point(13, 47);
            this.contentlist.MultiSelect = false;
            this.contentlist.Name = "contentlist";
            this.contentlist.Size = new System.Drawing.Size(631, 391);
            this.contentlist.TabIndex = 13;
            this.contentlist.UseCompatibleStateImageBehavior = false;
            this.contentlist.View = System.Windows.Forms.View.Details;
            this.contentlist.DoubleClick += new System.EventHandler(this.contentlist_DoubleClick);
            // 
            // carcha
            // 
            this.carcha.Text = "序号";
            this.carcha.Width = 69;
            // 
            // carchc
            // 
            this.carchc.Text = "标题";
            this.carchc.Width = 236;
            // 
            // carchd
            // 
            this.carchd.Text = "代码";
            this.carchd.Width = 234;
            // 
            // carche
            // 
            this.carche.Text = "内容类型";
            this.carche.Width = 85;
            // 
            // carchf
            // 
            this.carchf.Text = "ids";
            this.carchf.Width = 0;
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
            this.btadd.Location = new System.Drawing.Point(293, 12);
            this.btadd.Name = "btadd";
            this.btadd.Size = new System.Drawing.Size(90, 23);
            this.btadd.TabIndex = 14;
            this.btadd.Text = "新增采集内容";
            this.btadd.UseVisualStyleBackColor = true;
            this.btadd.Click += new System.EventHandler(this.btadd_Click);
            // 
            // btdel
            // 
            this.btdel.Location = new System.Drawing.Point(422, 12);
            this.btdel.Name = "btdel";
            this.btdel.Size = new System.Drawing.Size(90, 23);
            this.btdel.TabIndex = 15;
            this.btdel.Text = "删除采集内容";
            this.btdel.UseVisualStyleBackColor = true;
            this.btdel.Click += new System.EventHandler(this.btdel_Click);
            // 
            // btsort
            // 
            this.btsort.Location = new System.Drawing.Point(551, 12);
            this.btsort.Name = "btsort";
            this.btsort.Size = new System.Drawing.Size(93, 23);
            this.btsort.TabIndex = 23;
            this.btsort.Text = "重新排序";
            this.btsort.UseVisualStyleBackColor = true;
            this.btsort.Click += new System.EventHandler(this.btsort_Click);
            // 
            // FrmWebSpiderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 451);
            this.Controls.Add(this.btsort);
            this.Controls.Add(this.btdel);
            this.Controls.Add(this.btadd);
            this.Controls.Add(this.contentlist);
            this.Controls.Add(this.linfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(673, 490);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(673, 490);
            this.Name = "FrmWebSpiderList";
            this.Text = "采集内容管理";
            this.Load += new System.EventHandler(this.FrmLeave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label linfo;
        private System.Windows.Forms.ListView contentlist;
        private System.Windows.Forms.ColumnHeader carcha;
        private System.Windows.Forms.ColumnHeader carchc;
        private System.Windows.Forms.ColumnHeader carchd;
        private System.Windows.Forms.ColumnHeader carche;
        private System.Windows.Forms.ColumnHeader carchf;
        private System.Windows.Forms.Button btadd;
        private System.Windows.Forms.Button btdel;
        private System.Windows.Forms.Button btsort;
    }
}