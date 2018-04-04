namespace WebSpider
{
    partial class FrmRegRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegRule));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbregrulemark = new System.Windows.Forms.TextBox();
            this.btcopymark = new System.Windows.Forms.Button();
            this.rTBUrlRule = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btbuild = new System.Windows.Forms.Button();
            this.btinsertmark = new System.Windows.Forms.Button();
            this.btput = new System.Windows.Forms.Button();
            this.btinsertmarka = new System.Windows.Forms.Button();
            this.btcopymarka = new System.Windows.Forms.Button();
            this.tbregrulemarka = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "第一步：查看网页源码[IE>>>查看>>>源文件]；";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(473, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "第二步：网页源码中找到一段html代码(代码中包括目标代码)，并复制到下边的文本框；";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "第三步：将代码中的目标代码替换为 ；";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(317, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "第四步：点击生成规则，文本框内就得到生成的匹配规则；";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(299, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "第五步：点击确定按钮,将生成的规则返回到文本框中。";
            // 
            // tbregrulemark
            // 
            this.tbregrulemark.Location = new System.Drawing.Point(83, 73);
            this.tbregrulemark.Name = "tbregrulemark";
            this.tbregrulemark.ReadOnly = true;
            this.tbregrulemark.Size = new System.Drawing.Size(161, 21);
            this.tbregrulemark.TabIndex = 5;
            this.tbregrulemark.Text = "[RegRuleZhyNoLabel88088]";
            // 
            // btcopymark
            // 
            this.btcopymark.Location = new System.Drawing.Point(256, 72);
            this.btcopymark.Name = "btcopymark";
            this.btcopymark.Size = new System.Drawing.Size(80, 23);
            this.btcopymark.TabIndex = 100;
            this.btcopymark.Text = "复制标志码";
            this.btcopymark.UseVisualStyleBackColor = true;
            this.btcopymark.Click += new System.EventHandler(this.btcopymark_Click);
            // 
            // rTBUrlRule
            // 
            this.rTBUrlRule.Location = new System.Drawing.Point(91, 182);
            this.rTBUrlRule.Name = "rTBUrlRule";
            this.rTBUrlRule.Size = new System.Drawing.Size(767, 315);
            this.rTBUrlRule.TabIndex = 102;
            this.rTBUrlRule.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(26, 333);
            this.label6.MaximumSize = new System.Drawing.Size(120, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 101;
            this.label6.Text = "html代码";
            // 
            // btbuild
            // 
            this.btbuild.Location = new System.Drawing.Point(91, 506);
            this.btbuild.Name = "btbuild";
            this.btbuild.Size = new System.Drawing.Size(98, 35);
            this.btbuild.TabIndex = 103;
            this.btbuild.Text = "生成规则";
            this.btbuild.UseVisualStyleBackColor = true;
            this.btbuild.Click += new System.EventHandler(this.btbuild_Click);
            // 
            // btinsertmark
            // 
            this.btinsertmark.Location = new System.Drawing.Point(346, 72);
            this.btinsertmark.Name = "btinsertmark";
            this.btinsertmark.Size = new System.Drawing.Size(80, 23);
            this.btinsertmark.TabIndex = 104;
            this.btinsertmark.Text = "插入标志码";
            this.btinsertmark.UseVisualStyleBackColor = true;
            this.btinsertmark.Click += new System.EventHandler(this.btinsertmark_Click);
            // 
            // btput
            // 
            this.btput.Location = new System.Drawing.Point(276, 506);
            this.btput.Name = "btput";
            this.btput.Size = new System.Drawing.Size(98, 35);
            this.btput.TabIndex = 105;
            this.btput.Text = "确定";
            this.btput.UseVisualStyleBackColor = true;
            this.btput.Click += new System.EventHandler(this.btput_Click);
            // 
            // btinsertmarka
            // 
            this.btinsertmarka.Location = new System.Drawing.Point(346, 98);
            this.btinsertmarka.Name = "btinsertmarka";
            this.btinsertmarka.Size = new System.Drawing.Size(80, 23);
            this.btinsertmarka.TabIndex = 108;
            this.btinsertmarka.Text = "插入标志码";
            this.btinsertmarka.UseVisualStyleBackColor = true;
            this.btinsertmarka.Click += new System.EventHandler(this.btinsertmarka_Click);
            // 
            // btcopymarka
            // 
            this.btcopymarka.Location = new System.Drawing.Point(256, 98);
            this.btcopymarka.Name = "btcopymarka";
            this.btcopymarka.Size = new System.Drawing.Size(80, 23);
            this.btcopymarka.TabIndex = 107;
            this.btcopymarka.Text = "复制标志码";
            this.btcopymarka.UseVisualStyleBackColor = true;
            this.btcopymarka.Click += new System.EventHandler(this.btcopymarka_Click);
            // 
            // tbregrulemarka
            // 
            this.tbregrulemarka.Location = new System.Drawing.Point(83, 99);
            this.tbregrulemarka.Name = "tbregrulemarka";
            this.tbregrulemarka.ReadOnly = true;
            this.tbregrulemarka.Size = new System.Drawing.Size(161, 21);
            this.tbregrulemarka.TabIndex = 106;
            this.tbregrulemarka.Text = "[RegRuleZhyLabel88088]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(443, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(197, 12);
            this.label7.TabIndex = 109;
            this.label7.Text = "此标识码不会匹配html标签，即：<>";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(443, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 12);
            this.label8.TabIndex = 110;
            this.label8.Text = "此标识码可以匹配html标签";
            // 
            // FrmRegRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 553);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btinsertmarka);
            this.Controls.Add(this.btcopymarka);
            this.Controls.Add(this.tbregrulemarka);
            this.Controls.Add(this.btput);
            this.Controls.Add(this.btinsertmark);
            this.Controls.Add(this.btbuild);
            this.Controls.Add(this.rTBUrlRule);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btcopymark);
            this.Controls.Add(this.tbregrulemark);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(904, 592);
            this.MinimumSize = new System.Drawing.Size(904, 592);
            this.Name = "FrmRegRule";
            this.Text = "规则生成器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbregrulemark;
        private System.Windows.Forms.Button btcopymark;
        private System.Windows.Forms.RichTextBox rTBUrlRule;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btbuild;
        private System.Windows.Forms.Button btinsertmark;
        private System.Windows.Forms.Button btput;
        private System.Windows.Forms.Button btinsertmarka;
        private System.Windows.Forms.Button btcopymarka;
        private System.Windows.Forms.TextBox tbregrulemarka;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}