namespace WebSpider
{
    partial class FrmListRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListRule));
            this.rTBUrlList = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btbuild = new System.Windows.Forms.Button();
            this.btput = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tburlq = new System.Windows.Forms.TextBox();
            this.tburlh = new System.Windows.Forms.TextBox();
            this.tbnums = new System.Windows.Forms.TextBox();
            this.tbnume = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rTBUrlList
            // 
            this.rTBUrlList.Location = new System.Drawing.Point(120, 171);
            this.rTBUrlList.Name = "rTBUrlList";
            this.rTBUrlList.Size = new System.Drawing.Size(666, 194);
            this.rTBUrlList.TabIndex = 102;
            this.rTBUrlList.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(57, 262);
            this.label6.MaximumSize = new System.Drawing.Size(120, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 101;
            this.label6.Text = "地址列表";
            // 
            // btbuild
            // 
            this.btbuild.Location = new System.Drawing.Point(120, 384);
            this.btbuild.Name = "btbuild";
            this.btbuild.Size = new System.Drawing.Size(98, 35);
            this.btbuild.TabIndex = 103;
            this.btbuild.Text = "生成规则";
            this.btbuild.UseVisualStyleBackColor = true;
            this.btbuild.Click += new System.EventHandler(this.btbuild_Click);
            // 
            // btput
            // 
            this.btput.Location = new System.Drawing.Point(278, 384);
            this.btput.Name = "btput";
            this.btput.Size = new System.Drawing.Size(98, 35);
            this.btput.TabIndex = 105;
            this.btput.Text = "确定";
            this.btput.UseVisualStyleBackColor = true;
            this.btput.Click += new System.EventHandler(this.btput_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(31, 34);
            this.label7.MaximumSize = new System.Drawing.Size(120, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 106;
            this.label7.Text = "循环前半部分";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(31, 69);
            this.label8.MaximumSize = new System.Drawing.Size(120, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 107;
            this.label8.Text = "循环后半部分";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(31, 103);
            this.label9.MaximumSize = new System.Drawing.Size(120, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 108;
            this.label9.Text = "循环起始数字";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(31, 138);
            this.label10.MaximumSize = new System.Drawing.Size(120, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 109;
            this.label10.Text = "循环终止数字";
            // 
            // tburlq
            // 
            this.tburlq.Location = new System.Drawing.Point(120, 30);
            this.tburlq.MaxLength = 4000;
            this.tburlq.Name = "tburlq";
            this.tburlq.Size = new System.Drawing.Size(666, 21);
            this.tburlq.TabIndex = 110;
            // 
            // tburlh
            // 
            this.tburlh.Location = new System.Drawing.Point(120, 65);
            this.tburlh.MaxLength = 4000;
            this.tburlh.Name = "tburlh";
            this.tburlh.Size = new System.Drawing.Size(666, 21);
            this.tburlh.TabIndex = 111;
            // 
            // tbnums
            // 
            this.tbnums.Location = new System.Drawing.Point(120, 99);
            this.tbnums.MaxLength = 15;
            this.tbnums.Name = "tbnums";
            this.tbnums.Size = new System.Drawing.Size(138, 21);
            this.tbnums.TabIndex = 112;
            // 
            // tbnume
            // 
            this.tbnume.Location = new System.Drawing.Point(120, 134);
            this.tbnume.MaxLength = 15;
            this.tbnume.Name = "tbnume";
            this.tbnume.Size = new System.Drawing.Size(138, 21);
            this.tbnume.TabIndex = 113;
            // 
            // FrmListRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 447);
            this.Controls.Add(this.tbnume);
            this.Controls.Add(this.tbnums);
            this.Controls.Add(this.tburlh);
            this.Controls.Add(this.tburlq);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btput);
            this.Controls.Add(this.btbuild);
            this.Controls.Add(this.rTBUrlList);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(836, 486);
            this.MinimumSize = new System.Drawing.Size(836, 486);
            this.Name = "FrmListRule";
            this.Text = "地址生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rTBUrlList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btbuild;
        private System.Windows.Forms.Button btput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tburlq;
        private System.Windows.Forms.TextBox tburlh;
        private System.Windows.Forms.TextBox tbnums;
        private System.Windows.Forms.TextBox tbnume;
    }
}