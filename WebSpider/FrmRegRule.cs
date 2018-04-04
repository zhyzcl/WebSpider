using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App;
using App.Win;
using App.Web;

namespace WebSpider
{
    public partial class FrmRegRule : Form
    {
        public bool IsOper = false;

        public string RegRuleText = "";

        public FrmRegRule()
        {
            InitializeComponent();
        }

        private void btcopymark_Click(object sender, EventArgs e)
        {
            tbregrulemark.Copy();
        }

        private void btinsertmark_Click(object sender, EventArgs e)
        {
            string inserted = tbregrulemark.Text.Trim();
            rTBUrlRule.SelectedText = inserted;
        }

        private void btbuild_Click(object sender, EventArgs e)
        {
            IsOper = true;
            rTBUrlRule.Text = BuildRegRule(rTBUrlRule.Text);
            RegRuleText = rTBUrlRule.Text.Trim();
        }

        public static string BuildRegRule(string s)
        {
            List<string> lia = new List<string>();
            lia.Add(@"\\");
            lia.Add(@"\^");
            lia.Add(@"\$");
            lia.Add(@"\{");
            lia.Add(@"\[");
            lia.Add(@"\.");
            lia.Add(@"\(");
            lia.Add(@"\*");
            lia.Add(@"\+");
            lia.Add(@"\?");
            lia.Add(@"\!");
            lia.Add(@"\#");
            lia.Add(@"\|");
            List<string> lib = new List<string>();
            lib.Add(@"\\");
            lib.Add(@"^");
            lib.Add(@"$");
            lib.Add(@"{");
            lib.Add(@"[");
            lib.Add(@".");
            lib.Add(@"(");
            lib.Add(@"*");
            lib.Add(@"+");
            lib.Add(@"?");
            lib.Add(@"!");
            lib.Add(@"#");
            lib.Add(@"|");
            for (int i=0;i< lia.Count;i++)
            {
                Regex reg = new Regex(lia[i], RegexOptions.IgnoreCase);
                s = reg.Replace(s, @"\" + lib[i]);
            }
            s = new Regex(@"\s+", RegexOptions.IgnoreCase).Replace(s, @"\s+");
            return s;
        }

        private void btput_Click(object sender, EventArgs e)
        {
            IsOper = true;
            RegRuleText = rTBUrlRule.Text.Trim();
            this.Close();
        }

        private void btcopymarka_Click(object sender, EventArgs e)
        {
            tbregrulemarka.Copy();
        }

        private void btinsertmarka_Click(object sender, EventArgs e)
        {
            string inserted = tbregrulemarka.Text.Trim();
            rTBUrlRule.SelectedText = inserted;
        }
    }
}
