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
    public partial class FrmListRule : Form
    {
        public bool IsOper = false;

        public string RegRuleText = "";

        public FrmListRule()
        {
            InitializeComponent();
        }

        private void btbuild_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            string urlq = tburlq.Text.Trim();
            string urlh = tburlh.Text.Trim();
            string snums = tbnums.Text.Trim();
            string snume = tbnume.Text.Trim();
            if (!Often.IsInt64(snums))
            {
                WinOften.MessShow("循环起始数字必须是整数！", 1);
                return;
            }
            if (!Often.IsInt64(snume))
            {
                WinOften.MessShow("循环终止数字必须是整数！", 1);
                return;
            }
            long nums = Convert.ToInt64(snums);
            long nume = Convert.ToInt64(snume);
            if (nume < nums)
            {
                WinOften.MessShow("循环终止数字不能小于循环起始数字！", 1);
                return;
            }
            for (long i = nums; i <= nume; i++)
            {
                if (sb.Length>0)
                {
                    sb.Append("\r\n");
                }
                sb.Append(urlq + i.ToString() + urlh);
            }
            IsOper = true;
            rTBUrlList.Text = sb.ToString();
            RegRuleText = rTBUrlList.Text.Trim();
        }

        private void btput_Click(object sender, EventArgs e)
        {
            IsOper = true;
            RegRuleText = rTBUrlList.Text.Trim();
            this.Close();
        }
    }
}
