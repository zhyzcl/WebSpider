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
    public partial class FrmWebSpiderInfo : Form
    {
        /// <summary>是否操作成功</summary>
        public bool IsOper = false;

        /// <summary>编辑时的id</summary>
        public string ids = "";

        /// <summary>操作模式 0:添加,1:编辑</summary>
        public byte OpMode = 0;

        public DataTable infodt = new DataTable();

        public List<RadioButton> rbishtml = new List<RadioButton>();
        public List<RadioButton> rbcmode = new List<RadioButton>();
        public List<RadioButton> rbisecho = new List<RadioButton>();
        public List<RadioButton> rbisnext = new List<RadioButton>();
        public List<RadioButton> rbcisnext = new List<RadioButton>();
        public List<RadioButton> rbnmode = new List<RadioButton>();
        public List<RadioButton> rbcnmode = new List<RadioButton>();

        public FrmWebSpiderInfo(params object[] param)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (param.Length > 1)
            {
                ids = param[0].ToString().Trim();
                OpMode = Convert.ToByte(param[1]);
            }
        }

        private void FrmUserInfo_Load(object sender, EventArgs e)
        {
            btset.Visible = false;

            tbOrderNum.Text = (wapp.WebSpiderList.WebSpiderTable.Rows.Count + 1).ToString();

            cBPageCoding.Items.Add("utf-8");
            cBPageCoding.Items.Add("big5");
            cBPageCoding.Items.Add("gb2312");
            cBPageCoding.Items.Add("utf-16");
            cBPageCoding.Items.Add("EUC-JP");
            cBPageCoding.Items.Add("euc-jp");
            cBPageCoding.Items.Add("utf-7");

            rbishtml.Add(rBIsSaveHtmlb);
            rbishtml.Add(rBIsSaveHtmla);

            rbcmode.Add(rBCodingModea);
            rbcmode.Add(rBCodingModeb);

            rbisecho.Add(rBIsEchob);
            rbisecho.Add(rBIsEchoa);

            rbisnext.Add(rBIsNextb);
            rbisnext.Add(rBIsNexta);

            rbcisnext.Add(rBConIsNextb);
            rbcisnext.Add(rBConIsNexta);

            rbnmode.Add(rBNextModea);
            rbnmode.Add(rBNextModeb);

            rbcnmode.Add(rBConNextModea);
            rbcnmode.Add(rBConNextModeb);

            if (OpMode == 1)
            {
                LoadInfo();
            }
        }

        private void LoadInfo()
        {
            DataRow[] idrs = wapp.WebSpiderList.WebSpiderTable.Select("SpiderID='" + ids + "'");
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
            btset.Visible = true;
            tbSpiderName.Text = DataOften.GetStr(infodt, "SpiderName");
            tbOrderNum.Text = DataOften.GetStr(infodt, "OrderNum");
            rTBUrlList.Text = DataOften.GetStr(infodt, "UrlList");
            rTBUrlRule.Text = DataOften.GetStr(infodt, "UrlRule");
            rTBUrlRangeRule.Text = DataOften.GetStr(infodt, "UrlRangeRule");
            tbUrlRuleIndex.Text = DataOften.GetStr(infodt, "UrlRuleIndex");
            tbUrlRangeRuleIndex.Text = DataOften.GetStr(infodt, "UrlRangeRuleIndex");
            tbExcelSavePath.Text = DataOften.GetStr(infodt, "ExcelSavePath");
            tbHtmlSavePath.Text = DataOften.GetStr(infodt, "HtmlSavePath");
            tbGatherMaxNum.Text = DataOften.GetStr(infodt, "GatherMaxNum");
            cBPageCoding.Text = DataOften.GetStr(infodt, "PageCoding");
            rTBNextRangeRule.Text = DataOften.GetStr(infodt, "NextRangeRule");
            rTBNextRule.Text = DataOften.GetStr(infodt, "NextRule");
            rTBConNextRangeRule.Text = DataOften.GetStr(infodt, "ConNextRangeRule");
            rTBConNextRule.Text = DataOften.GetStr(infodt, "ConNextRule");
            tbNextRangeRuleIndex.Text = DataOften.GetStr(infodt, "NextRangeRuleIndex");
            tbNextRuleIndex.Text = DataOften.GetStr(infodt, "NextRuleIndex");
            tbConNextRangeRuleIndex.Text = DataOften.GetStr(infodt, "ConNextRangeRuleIndex");
            tbConNextRuleIndex.Text = DataOften.GetStr(infodt, "ConNextRuleIndex");
            wapp.AppPub.SetRadioListChecked(DataOften.GetStr(infodt, "IsSaveHtml"), rbishtml);
            wapp.AppPub.SetRadioListChecked(DataOften.GetStr(infodt, "CodingMode"), rbcmode);
            wapp.AppPub.SetRadioListChecked(DataOften.GetStr(infodt, "IsEcho"), rbisecho);
            wapp.AppPub.SetRadioListChecked(DataOften.GetStr(infodt, "IsNext"), rbisnext);
            wapp.AppPub.SetRadioListChecked(DataOften.GetStr(infodt, "ConIsNext"), rbcisnext);
            wapp.AppPub.SetRadioListChecked(DataOften.GetStr(infodt, "NextMode"), rbnmode);
            wapp.AppPub.SetRadioListChecked(DataOften.GetStr(infodt, "ConNextMode"), rbcnmode);
        }

        public bool IsRunOper()
        {
            string sname = tbSpiderName.Text.Trim();
            if (sname == "")
            {
                WinOften.MessShow("名称不能为空！", 1);
                return false;
            }
            string onum = tbOrderNum.Text.Trim();
            if (!Often.IsInt32(onum))
            {
                WinOften.MessShow("排序必须是数字！", 1);
                return false;
            }
            string ulist = rTBUrlList.Text.Trim();
            if (ulist == "")
            {
                WinOften.MessShow("Url列表不能为空！", 1);
                return false;
            }
            string urule = rTBUrlRule.Text.Trim();
            if (urule == "")
            {
                WinOften.MessShow("内容Url匹配规则不能为空！", 1);
                return false;
            }
            if (rBIsSaveHtmla.Checked)
            {
                string hsp = tbHtmlSavePath.Text.Trim();
                if (hsp == "")
                {
                    WinOften.MessShow("html文件保存路径不能为空！", 1);
                    return false;
                }
                if (!Directory.Exists(hsp))
                {
                    WinOften.MessShow("html文件保存路径不存在！", 1);
                    return false;
                }
            }
            string esp = tbExcelSavePath.Text.Trim();
            if (esp == "")
            {
                WinOften.MessShow("Excel文件保存路径不能为空！", 1);
                return false;
            }
            if (!Directory.Exists(esp))
            {
                WinOften.MessShow("Excel文件保存路径不存在！", 1);
                return false;
            }
            string gmn = tbGatherMaxNum.Text.Trim();
            if (!Often.IsInt32(gmn))
            {
                WinOften.MessShow("一次最大采集数量必须是数字！", 1);
                return false;
            }
            int gmni = Convert.ToInt32(gmn);
            if (gmni < 0)
            {
                WinOften.MessShow("一次最大采集数量不能小于0！", 1);
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
            string sname = tbSpiderName.Text.Trim();
            if (OpMode == 0)
            {
                DataRow[] drs = wapp.WebSpiderList.WebSpiderTable.Select("SpiderName='" + sname + "'");
                if (drs.Length > 0)
                {
                    WinOften.MessShow("名称已存在！", 1);
                    return;
                }
            }
            string guid = System.Guid.NewGuid().ToString("N").ToUpper();
            if (ids != "")
            {
                guid = ids;
            }
            wapp.WebSpider ws = new wapp.WebSpider();
            ws.SpiderID = guid;
            ws.SpiderName = tbSpiderName.Text.Trim();
            ws.OrderNum = Convert.ToInt32(tbOrderNum.Text);
            ws.UrlList = rTBUrlList.Text.Trim();
            ws.UrlRule = rTBUrlRule.Text.Trim();
            ws.UrlRangeRule = rTBUrlRangeRule.Text.Trim();
            ws.UrlRuleIndex = wapp.AppPub.GetInt(tbUrlRuleIndex.Text);
            ws.UrlRangeRuleIndex = wapp.AppPub.GetInt(tbUrlRangeRuleIndex.Text);
            ws.ExcelSavePath = tbExcelSavePath.Text.Trim();
            ws.HtmlSavePath = tbHtmlSavePath.Text.Trim();
            ws.IsSaveHtml = wapp.AppPub.GetRadioListCheckedIndex(rbishtml);
            ws.CodingMode = wapp.AppPub.GetRadioListCheckedIndex(rbcmode);
            ws.IsEcho = wapp.AppPub.GetRadioListCheckedIndex(rbisecho);
            ws.IsNext = wapp.AppPub.GetRadioListCheckedIndex(rbisnext);
            ws.ConIsNext = wapp.AppPub.GetRadioListCheckedIndex(rbcisnext);
            ws.NextMode = wapp.AppPub.GetRadioListCheckedIndex(rbnmode);
            ws.ConNextMode = wapp.AppPub.GetRadioListCheckedIndex(rbcnmode);
            ws.GatherMaxNum = Convert.ToInt32(tbGatherMaxNum.Text);
            ws.PageCoding = cBPageCoding.Text;
            ws.NextRangeRule = rTBNextRangeRule.Text;
            ws.NextRule = rTBNextRule.Text;
            ws.ConNextRangeRule = rTBConNextRangeRule.Text;
            ws.ConNextRule = rTBConNextRule.Text;
            ws.NextRangeRuleIndex = wapp.AppPub.GetInt(tbNextRangeRuleIndex.Text);
            ws.NextRuleIndex = wapp.AppPub.GetInt(tbNextRuleIndex.Text);
            ws.ConNextRangeRuleIndex = wapp.AppPub.GetInt(tbConNextRangeRuleIndex.Text);
            ws.ConNextRuleIndex = wapp.AppPub.GetInt(tbConNextRuleIndex.Text);
            wapp.WebSpiderList.SaveWebSpiderTable(ws);
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

        private void btUrlList_Click(object sender, EventArgs e)
        {
            using (FrmListRule form = new FrmListRule())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if (form.IsOper)
                {
                    rTBUrlList.Text = form.RegRuleText;
                }
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
                    rTBUrlRule.Text = form.RegRuleText;
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
                    rTBUrlRangeRule.Text = form.RegRuleText;
                }
            }
        }

        private void btExcelSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = System.Environment.CurrentDirectory;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbExcelSavePath.Text = fbd.SelectedPath;
            }
        }

        private void btHtmlSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = System.Environment.CurrentDirectory;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbHtmlSavePath.Text = fbd.SelectedPath;
            }
        }

        private void btset_Click(object sender, EventArgs e)
        {
            using (FrmWebSpiderList form = new FrmWebSpiderList(ids))
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
            }
        }

        private void btgather_Click(object sender, EventArgs e)
        {
            using (FrmGather form = new FrmGather(ids))
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
            }
        }

        private void btNextRangeRule_Click(object sender, EventArgs e)
        {
            using (FrmRegRule form = new FrmRegRule())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if (form.IsOper)
                {
                    rTBNextRangeRule.Text = form.RegRuleText;
                }
            }
        }

        private void btNextRule_Click(object sender, EventArgs e)
        {
            using (FrmRegRule form = new FrmRegRule())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if (form.IsOper)
                {
                    rTBNextRule.Text = form.RegRuleText;
                }
            }
        }

        private void btConNextRangeRule_Click(object sender, EventArgs e)
        {
            using (FrmRegRule form = new FrmRegRule())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if (form.IsOper)
                {
                    rTBConNextRangeRule.Text = form.RegRuleText;
                }
            }
        }

        private void btConNextRule_Click(object sender, EventArgs e)
        {
            using (FrmRegRule form = new FrmRegRule())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if (form.IsOper)
                {
                    rTBConNextRule.Text = form.RegRuleText;
                }
            }
        }
    }
}
