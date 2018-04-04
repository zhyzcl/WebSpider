namespace WebSpider
{
    partial class FrmGather
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGather));
            this.btstop = new System.Windows.Forms.Button();
            this.richText = new System.Windows.Forms.RichTextBox();
            this.bttest = new System.Windows.Forms.Button();
            this.bkWk = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btstop
            // 
            this.btstop.Location = new System.Drawing.Point(541, 27);
            this.btstop.Name = "btstop";
            this.btstop.Size = new System.Drawing.Size(152, 38);
            this.btstop.TabIndex = 129;
            this.btstop.Text = "停止采集";
            this.btstop.UseVisualStyleBackColor = true;
            this.btstop.Click += new System.EventHandler(this.btstop_Click);
            // 
            // richText
            // 
            this.richText.Location = new System.Drawing.Point(13, 99);
            this.richText.Name = "richText";
            this.richText.Size = new System.Drawing.Size(883, 412);
            this.richText.TabIndex = 128;
            this.richText.Text = "";
            // 
            // bttest
            // 
            this.bttest.Location = new System.Drawing.Point(189, 27);
            this.bttest.Name = "bttest";
            this.bttest.Size = new System.Drawing.Size(152, 38);
            this.bttest.TabIndex = 127;
            this.bttest.Text = "开始采集";
            this.bttest.UseVisualStyleBackColor = true;
            this.bttest.Click += new System.EventHandler(this.bttest_Click);
            // 
            // bkWk
            // 
            this.bkWk.WorkerReportsProgress = true;
            this.bkWk.WorkerSupportsCancellation = true;
            this.bkWk.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkWk_DoWork);
            this.bkWk.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bkWk_ProgressChanged);
            this.bkWk.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkWk_RunWorkerCompleted);
            // 
            // FrmGather
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 525);
            this.Controls.Add(this.btstop);
            this.Controls.Add(this.richText);
            this.Controls.Add(this.bttest);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(925, 564);
            this.MinimumSize = new System.Drawing.Size(925, 564);
            this.Name = "FrmGather";
            this.Text = "采集数据";
            this.Load += new System.EventHandler(this.FrmGather_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btstop;
        private System.Windows.Forms.RichTextBox richText;
        private System.Windows.Forms.Button bttest;
        private System.ComponentModel.BackgroundWorker bkWk;
    }
}