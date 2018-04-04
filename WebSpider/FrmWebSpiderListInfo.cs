using System;
using System.IO;
using System.Collections.Generic;
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
    public partial class FrmWebSpiderListInfo : Form
    {
        /// <summary>是否操作成功</summary>
        public bool IsOper = false;

        /// <summary>编辑时的id</summary>
        public string ids = "";

        public string pids = "";

        /// <summary>操作模式 0:添加,1:编辑</summary>
        public byte OpMode = 0;

        public DataTable infodt = new DataTable();

        public List<RadioButton> rbctype = new List<RadioButton>();

        public List<RadioButton> rbcfilta = new List<RadioButton>();
        public List<RadioButton> rbcfiltb = new List<RadioButton>();
        public List<RadioButton> rbcfiltc = new List<RadioButton>();
        public List<RadioButton> rbcfiltd = new List<RadioButton>();

        public List<int> cflist = new List<int>();

        public FrmWebSpiderListInfo(params object[] param)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (param.Length > 2)
            {
                ids = param[0].ToString().Trim();
                pids = param[1].ToString().Trim();
                OpMode = Convert.ToByte(param[2]);
            }
        }

        private void FrmWebSpiderListInfo_Load(object sender, EventArgs e)
        {
            tbOrderNum.Text = (wapp.WebSpiderInfoList.WebSpiderInfoTable.Rows.Count + 1).ToString();

            cBContentCodeMode.Items.Clear();
            cBContentCodeMode.Items.Add(new App.ValTxt("0", "不解码"));
            cBContentCodeMode.Items.Add(new App.ValTxt("1", "Html解码"));
            cBContentCodeMode.Items.Add(new App.ValTxt("2", "Url解码"));
            cBContentCodeMode.Items.Add(new App.ValTxt("3", "Escape解码"));
            cBContentCodeMode.Items.Add(new App.ValTxt("4", "EURI解码"));
            cBContentCodeMode.Items.Add(new App.ValTxt("11", "Html编码"));
            cBContentCodeMode.Items.Add(new App.ValTxt("12", "Url编码"));
            cBContentCodeMode.Items.Add(new App.ValTxt("13", "Escape编码"));
            cBContentCodeMode.Items.Add(new App.ValTxt("14", "EURI编码"));
            cBContentCodeMode.SelectedIndex = 0;

            rbctype.Add(rBContentTypea);
            rbctype.Add(rBContentTypeb);
            rbctype.Add(rBContentTypec);

            rbcfilta.Add(rBContentFilta1);
            rbcfilta.Add(rBContentFilta2);

            rbcfiltb.Add(rBContentFiltb1);
            rbcfiltb.Add(rBContentFiltb2);
            rbcfiltb.Add(rBContentFiltb3);

            rbcfiltc.Add(rBContentFiltc1);
            rbcfiltc.Add(rBContentFiltc2);
            rbcfiltc.Add(rBContentFiltc3);

            rbcfiltd.Add(rBContentFiltd1);
            rbcfiltd.Add(rBContentFiltd2);
            rbcfiltd.Add(rBContentFiltd3);

            LoadPInfo();
            if (OpMode == 1)
            {
                LoadInfo();
            }
        }

        private void LoadPInfo()
        {
            DataRow[] idrs = wapp.WebSpiderList.WebSpiderTable.Select("SpiderID='" + pids + "'");
            if (idrs.Length <= 0)
            {
                WinOften.MessShow("该信息不存在或已被删除！", 1);
                this.Close();
                return;
            }
        }

        private void LoadInfo()
        {
            DataRow[] idrs = wapp.WebSpiderInfoList.WebSpiderInfoTable.Select("ListID='" + ids + "'");
            if (idrs.Length > 0)
            {
                infodt = DataOften.GetTable(idrs);
            }
            else
            {
                WinOften.MessShow("该信息不存在或已被删除！", 1);
                this.Close();
                return;
            }
            tbListTitle.Text = DataOften.GetStr(infodt, "ListTitle");
            tbListCode.Text = DataOften.GetStr(infodt, "ListCode");
            tbOrderNum.Text = DataOften.GetStr(infodt, "OrderNum");            
            rTBContentRule.Text = DataOften.GetStr(infodt, "ContentRule");
            rTBContentRangeRule.Text = DataOften.GetStr(infodt, "ContentRangeRule");
            tbContentRuleIndex.Text = DataOften.GetStr(infodt, "ContentRuleIndex");
            tbContentRangeRuleIndex.Text = DataOften.GetStr(infodt, "ContentRangeRuleIndex");
            rTBContentRegFilt.Text = DataOften.GetStr(infodt, "ContentRegFilt");
            wapp.AppPub.SetRadioListChecked(DataOften.GetStr(infodt, "ContentType"), rbctype);
            wapp.AppPub.SelectComboBoxItems(cBContentCodeMode, DataOften.GetStr(infodt, "ContentCodeMode"));
            string cf = DataOften.GetStr(infodt, "ContentFilt");
            cflist = wapp.AppPub.GetIntList(cf, 4);
            wapp.AppPub.SetRadioListChecked(cflist[0].ToString(), rbcfilta);
            wapp.AppPub.SetRadioListChecked(cflist[1].ToString(), rbcfiltb);
            wapp.AppPub.SetRadioListChecked(cflist[2].ToString(), rbcfiltc);
            wapp.AppPub.SetRadioListChecked(cflist[3].ToString(), rbcfiltd);
        }

        public bool IsRunOper()
        {
            string ltitle = tbListTitle.Text.Trim();
            if (ltitle == "")
            {
                WinOften.MessShow("标题不能为空！", 1);
                return false;
            }
            string lcode = tbListCode.Text.Trim();
            if (lcode == "")
            {
                WinOften.MessShow("代码不能为空！", 1);
                return false;
            }
            string onum = tbOrderNum.Text.Trim();
            if (!Often.IsInt32(onum))
            {
                WinOften.MessShow("排序必须是数字！", 1);
                return false;
            }
            string crule = rTBContentRule.Text.Trim();
            if (crule == "")
            {
                WinOften.MessShow("内容匹配规则不能为空！", 1);
                return false;
            }
            return true;
        }

        private void btsave_Click(object sender, EventArgs e)
        {
            if (!IsRunOper())
            {
                return;
            }
            string ltitle = tbListTitle.Text.Trim();
            if (OpMode == 0)
            {
                DataRow[] drs = wapp.WebSpiderInfoList.WebSpiderInfoTable.Select("ListTitle='" + ltitle + "'");
                if (drs.Length > 0)
                {
                    WinOften.MessShow("标题已存在！", 1);
                    return;
                }
            }
            string guid = System.Guid.NewGuid().ToString("N").ToUpper();
            if (ids != "")
            {
                guid = ids;
            }
            wapp.WebSpiderInfo wsi = new wapp.WebSpiderInfo();
            wsi.ListID = guid;
            wsi.SpiderID = pids;
            wsi.ListTitle = tbListTitle.Text.Trim();
            wsi.ListCode = tbListCode.Text.Trim();
            wsi.OrderNum = Convert.ToInt32(tbOrderNum.Text);
            wsi.ContentRule = rTBContentRule.Text.Trim();
            wsi.ContentRangeRule = rTBContentRangeRule.Text.Trim();
            wsi.ContentRuleIndex = wapp.AppPub.GetInt(tbContentRuleIndex.Text);
            wsi.ContentRangeRuleIndex = wapp.AppPub.GetInt(tbContentRangeRuleIndex.Text);
            wsi.ContentRegFilt = rTBContentRegFilt.Text.Trim();
            wsi.ContentType = wapp.AppPub.GetRadioListCheckedIndex(rbctype);
            wsi.ContentCodeMode = wapp.AppPub.GetInt(((ValTxt)cBContentCodeMode.SelectedItem).Value);
            string cfs = wapp.AppPub.GetRadioListCheckedIndex(rbcfilta).ToString() + "," + wapp.AppPub.GetRadioListCheckedIndex(rbcfiltb).ToString() + "," + wapp.AppPub.GetRadioListCheckedIndex(rbcfiltc).ToString() + "," + wapp.AppPub.GetRadioListCheckedIndex(rbcfiltd).ToString();
            wsi.ContentFilt = cfs;
            wapp.WebSpiderInfoList.SaveWebSpiderInfoTable(wsi);
            WinOften.MessShow("保存成功！", 0);
            IsOper = true;
            this.Close();
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmUserInfo_Shown(object sender, EventArgs e)
        {
            if (wapp.SysConfig.atUser.ManageLv < 60)
            {
                WinOften.MessShow("操作权限不足！", 1);
                this.Close();
            }
        }

        private void btUrlRule_Click(object sender, EventArgs e)
        {
            using (FrmRegRule form = new FrmRegRule())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if (form.IsOper)
                {
                    rTBContentRule.Text = form.RegRuleText;
                }
            }
        }

        private void btUrlRangeRule_Click(object sender, EventArgs e)
        {
            using (FrmRegRule form = new FrmRegRule())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if (form.IsOper)
                {
                    rTBContentRangeRule.Text = form.RegRuleText;
                }
            }
        }
    }
}
