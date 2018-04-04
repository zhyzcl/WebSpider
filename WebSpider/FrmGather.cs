using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Web;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Runtime.InteropServices;
using App;
using App.Win;
using App.Web;

namespace WebSpider
{
    public partial class FrmGather : Form
    {
        /// <summary>当前任务队列</summary>
        public List<string> NowTaskIDList = new List<string>();

        /// <summary>获取字节流编码</summary>
        public ByteEncode det = new ByteEncode();

        /// <summary>当前正在操作的页面编码对象</summary>
        public Encoding PageEncoding = Encoding.Default;

        /// <summary>当前正在操作的页面编码</summary>
        public string PageCoding = "";

        /// <summary>日志输出对象</summary>
        public wapp.IlogOut lout = new wapp.FlogOut();

        /// <summary>采集项目对象</summary>
        public wapp.GatherInfo gi = new wapp.GatherInfo();

        /// <summary>网络蜘蛛id</summary>
        public string wsid = "";

        /// <summary>开始地址列表</summary>
        public List<string> UrlList = new List<string>();

        /// <summary>内容链接列表分页列表</summary>
        public List<string> CentUrlList = new List<string>();

        /// <summary>当前采集的开始地址列表</summary>
        public List<string> NowUrlList = new List<string>();

        /// <summary>开始地址采集次数总计</summary>
        public long NowUrlCount = 0;

        /// <summary>当前正在操作的开始地址列表索引位置</summary>
        public int NowUrlListIndex = 0;

        /// <summary>当前正在操作的开始地址</summary>
        public string NowUrl = "";

        /// <summary>当前采集到的内容页链接列表内容</summary>
        public string ListLinkContent = "";

        /// <summary>当前采集到的内容页链接列表</summary>
        public List<string> NowContLinkList = new List<string>();

        /// <summary>操作开始日期</summary>
        public DateTime OperStartDate = DateTime.Now;

        /// <summary>采集次数总计</summary>
        public long GatherCount = 0;

        /// <summary>内容采集成功次数总计</summary>
        public long ContSucceedCount = 0;

        /// <summary>内容采集失败次数总计</summary>
        public long ContKaputCount = 0;

        /// <summary>采集信息保存次数</summary>
        public long SaveGatherInfoCount = 0;

        /// <summary>采集信息导入成功次数</summary>
        public long SaveInfoCount = 0;

        /// <summary>当前操作开始时间</summary>
        public DateTime StartDate = DateTime.Now;

        /// <summary>当前操作结束时间</summary>
        public DateTime EndDate = DateTime.Now;

        /// <summary>Excel文件名</summary>
        public string ExcelSaveFileName = "";

        /// <summary>Excel文件保存序号</summary>
        public int ExcelSaveNum = 1000000;

        public FrmGather(params object[] param)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (param.Length > 0)
            {
                wsid = param[0].ToString().Trim();
            }
        }

        private void FrmGather_Load(object sender, EventArgs e)
        {
            lout.richText = richText;
            lout.slog = new wapp.Serverlog();
            lout.slog.ServerName = this.Text;
            StopOperSet();
        }

        private void bttest_Click(object sender, EventArgs e)
        {
            StartOperSet();
            bkWk.RunWorkerAsync();
        }

        private void btstop_Click(object sender, EventArgs e)
        {
            StopOperSet();
        }

        private void StartOper()
        {
            LoadGatherInfo();
            StartGather();
        }

        public void StartOperSet()
        {
            bttest.Enabled = false;
            btstop.Enabled = true;
        }

        public void StopOperSet()
        {
            bttest.Enabled = true;
            btstop.Enabled = false;
        }

        private void bkWk_DoWork(object sender, DoWorkEventArgs e)
        {
            StartOper();
        }

        private void bkWk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void bkWk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StopOperSet();
        }

        /// <summary>读取采集项目详细信息</summary>
        public void LoadGatherInfo()
        {
            gi.LoadWebSpiderInfo(wsid);
            if (gi.ws.ExcelSavePath.EndsWith("\\") || gi.ws.ExcelSavePath.EndsWith("/"))
            {
                ExcelSaveFileName = gi.ws.ExcelSavePath + gi.ws.SpiderName;
            }
            else
            {
                ExcelSaveFileName = gi.ws.ExcelSavePath + "\\" + gi.ws.SpiderName;
            }  
            if (gi.ws != null)
            {
                lout.OT(0, "采集信息项目读取完毕！");
                lout.OT(0, "名称：" + gi.ws.SpiderName + "");
                lout.OT(0, "Url列表：\r\n" + gi.ws.UrlList + "\r\n");
                lout.OT(0, "内容Url范围匹配规则：" + gi.ws.UrlRangeRule + "");
                lout.OT(0, "内容Url匹配规则：" + gi.ws.UrlRule + "");
                lout.OT(0, "内容Url列表分页范围匹配规则：" + gi.ws.NextRangeRule + "");
                lout.OT(0, "内容Url列表分页匹配规则：" + gi.ws.NextRule + "");
                lout.OT(0, "是否采集内容Url列表分页：" + WebOften.GetListVal(wapp.AppList.IsYesNo(), gi.ws.IsNext.ToString()) + "");
                lout.OT(0, "内容Url列表分页采集模式：" + WebOften.GetListVal(wapp.AppList.NextMode(), gi.ws.NextMode.ToString()) + "");
                lout.OT(0, "Excel文件保存路径：" + gi.ws.ExcelSavePath + "");
                lout.OT(0, "是否保存html文件：" + WebOften.GetListVal(wapp.AppList.IsYesNo(), gi.ws.IsSaveHtml.ToString()) + "");
                lout.OT(0, "html文件保存路径：" + gi.ws.HtmlSavePath + "");
                lout.OT(0, "页面编码获取模式：" + WebOften.GetListVal(wapp.AppList.CodingMode(), gi.ws.CodingMode.ToString()) + "");
                lout.OT(0, "页面编码：" + gi.ws.PageCoding + "");
                lout.OT(0, "一次最大采集数目：" + gi.ws.GatherMaxNum.ToString() + "");
                lout.OT(0, "是否允许重复内容页：" + WebOften.GetListVal(wapp.AppList.IsYesNo(), gi.ws.IsEcho.ToString()) + "");
            }
        }

        /// <summary>开始采集</summary>
        public void StartGather()
        {
            ExcelSaveNum = 1000000;
            NowContLinkList.Clear();
            NowUrlList.Clear();
            UrlList = wapp.GatherApp.GetUrlList(gi.ws.UrlList);
            if (UrlList.Count == 0)
            {
                lout.OT(0, "Url列表无有效URL地址！");
            }
            for (int i = 0; i < UrlList.Count; i++)
            {
                CentUrlList = new List<string>();
                NowUrlListIndex = i;
                string orders = (i + 1).ToString();
                string gatherurl = UrlList[NowUrlListIndex];
                NowUrl = gatherurl;
                lout.OT(1, "第[" + orders + "]次执行采集URL[" + gatherurl + "]");
                string errs = "";
                ListLinkContent = wapp.GatherApp.GetHttp(det, gatherurl, gi.ws.CodingMode, ref PageEncoding, ref PageCoding, ref errs);
                if (errs != "")
                {
                    lout.OT(1, "第[" + orders + "]次获取URL内容失败，错误信息：" + errs + "。[Url:" + gatherurl + "]");
                }
                else
                {
                    lout.OT(1, "第[" + orders + "]次获取URL内容。[" + ListLinkContent.Length.ToString() + "]");
                    if (gi.ws.IsNext == 1 && gi.ws.NextMode == 1 && ListLinkContent.Trim() != "")
                    {
                        List<string> clist = new List<string>();
                        #region 采集内容链接列表分页（列表分页为单页采集模式）
                        if (gi.ws.UrlRangeRule != "")
                        {
                            clist = wapp.GatherApp.GetRegTxtList(wapp.GatherApp.GetRegTxtString(ListLinkContent, gi.ws.UrlRangeRule, gi.ws.UrlRangeRuleIndex).ToString(), gi.ws.UrlRule, gi.ws.UrlRuleIndex);
                        }
                        else
                        {
                            clist = wapp.GatherApp.GetRegTxtList(ListLinkContent, gi.ws.UrlRule, gi.ws.UrlRuleIndex);
                        }
                        for (int ii = 0; ii < clist.Count; ii++)
                        {
                            string clink = wapp.GatherApp.GetUrls(wapp.GatherApp.FormatUrl(gatherurl, clist[ii].ToString().Trim()));
                            if (clink != "")
                            {
                                CentUrlList.Add(clink);
                            }
                        }
                        #endregion
                    }
                    if (ListLinkContent.Trim() != "")
                    {
                        GatherOper();
                    }
                }
            }
            if (gi.gdt.Rows.Count >= 0)
            {
                string savefile = GetExcelSaveFileName();
                lout.OT(1, "保存数据到Excel文件操作开始[文件名：" + savefile + "]...");
                wapp.Excel ex = new wapp.Excel();
                ex.DataTableToExcel(gi.gdt);
                ex.WriteFile(savefile);
                gi.gdt.Clear();
                lout.OT(1, "保存数据到Excel文件操作结束[文件名：" + savefile + "]");
            }
            lout.OT(1, "全部操作完毕。共采集信息[" + GatherCount.ToString() + "]次，采集内容成功[" + ContSucceedCount.ToString() + "]次，采集内容失败[" + ContKaputCount.ToString() + "]次。");
            lout.OT(1, "本次采集信息耗时：[" + DateOften.DateDiff(OperStartDate, DateTime.Now, "s").ToString() + "]秒");
        }

        /// <summary>返回Excel文件名</summary>
        /// <returns>返回Excel文件名</returns>
        public string GetExcelSaveFileName()
        {
            return ExcelSaveFileName + GetExcelNum() + ".xls";
        }

        /// <summary>返回Excel文件名序号</summary>
        /// <returns>返回Excel文件名序号</returns>
        public string GetExcelNum()
        {
            string s = DateOften.ReFDateTime("{$Year}{$Month}{$Day}{$Hour}{$Minute}{$Second}", DateTime.Now) + ExcelSaveNum.ToString();
            ExcelSaveNum ++;
            return s;
        }

        /// <summary>根据当前Url与Url内容采集数据</summary>
        public void GatherOper()
        {
            int cindex = 0;
            while (NowUrl.Trim() != "" && ListLinkContent.Trim() != "")
            {
                if (!IsListLinkEcho(NowUrl))
                {
                    string ListLinkArea = "";
                    if (gi.ws.UrlRangeRule != "")
                    {
                        ListLinkArea = wapp.GatherApp.GetRegTxtString(ListLinkContent, gi.ws.UrlRangeRule, gi.ws.UrlRangeRuleIndex);
                        lout.OT(1, "获取内容页链接列表范围匹配内容。[" + ListLinkArea.Length.ToString() + "]");
                    }
                    else
                    {
                        ListLinkArea = ListLinkContent;
                    }
                    if (ListLinkArea.Trim() != "")
                    {
                        List<string> linklist = wapp.GatherApp.GetRegTxtList(ListLinkArea, gi.ws.UrlRule, gi.ws.UrlRuleIndex);
                        lout.OT(1, "获取内容页链接匹配数组[" + linklist.Count.ToString() + "]");
                        for (int x = 0; x < linklist.Count; x++)
                        {
                            StartDate = DateTime.Now;
                            EndDate = StartDate;
                            string links = wapp.GatherApp.FormatUrl(NowUrl, linklist[x].Trim());
                            if (!IsContLinkEcho(links))
                            {
                                string actlink = links;
                                NowContLinkList.Add(actlink);
                                string errs = "";
                                string conts = wapp.GatherApp.GetHttp(det, actlink, gi.ws.CodingMode, ref PageEncoding, ref PageCoding, ref errs);
                                if (errs != "")
                                {
                                    lout.OT(1, "获取内容页起始url内容失败，错误信息：" + errs + "。[Url:" + actlink + "]");
                                }
                                else
                                {
                                    lout.OT(1, "获取内容页起始url内容。[" + actlink + "][" + conts.Length.ToString() + "]");
                                }
                                DataRow nsdr = gi.gdt.NewRow();
                                for (int si = 0; si < gi.wsilist.Count; si++)
                                {
                                    string sconts = "";
                                    if (gi.wsilist[si].ContentRangeRule != "")
                                    {
                                        sconts = wapp.GatherApp.GetRegTxtString(conts, gi.wsilist[si].ContentRangeRule, gi.wsilist[si].ContentRangeRuleIndex);
                                    }
                                    else
                                    {
                                        sconts = conts;
                                    }
                                    string sis = "";
                                    if (gi.wsilist[si].ContentType == 2)
                                    {
                                        sis = wapp.GatherApp.FormatDate(Often.OutTxt(wapp.GatherApp.GetUnCode(wapp.GatherApp.GetRegsTxt(sconts, gi.wsilist[si].ContentRule, gi.wsilist[si].ContentRuleIndex), gi.wsilist[si].ContentCodeMode)));
                                    }
                                    else
                                    {
                                        sis = wapp.GatherApp.GetUnCode(wapp.GatherApp.GetRegsTxt(sconts, gi.wsilist[si].ContentRule, gi.wsilist[si].ContentRuleIndex), gi.wsilist[si].ContentCodeMode);
                                    }
                                    if (gi.wsilist[si].ContentType == 1 && Often.IsNum(sis))
                                    {
                                        nsdr[si] = sis;
                                    }
                                    else if (gi.wsilist[si].ContentType == 2 && Often.IsDate(sis))
                                    {
                                        nsdr[si] = sis;
                                    }
                                    else
                                    {
                                        nsdr[si] = sis;
                                    }
                                    if (sis != "")
                                    {
                                        lout.OT(1, "[" + gi.wsilist[si].ListTitle + "(" + gi.wsilist[si].ListCode + ")]内容：[" + sis + "]采集成功！");
                                    }
                                }
                                gi.gdt.Rows.Add(nsdr);
                                GatherCount++;
                                if (GatherCount >= gi.ws.GatherMaxNum)
                                {
                                    return;
                                }
                                #region 保存采集内存表中的信息到数据库
                                if (gi.gdt.Rows.Count > 999)
                                {
                                    string savefile = GetExcelSaveFileName();
                                    lout.OT(1, "保存数据到Excel文件操作开始[文件名：" + savefile + "]..."); 
                                    wapp.Excel ex = new wapp.Excel();
                                    ex.DataTableToExcel(gi.gdt);
                                    ex.WriteFile(savefile);
                                    gi.gdt.Clear();
                                    lout.OT(1, "保存数据到Excel文件操作结束[文件名：" + savefile + "]");
                                }
                                #endregion
                            }
                        }
                    }
                }
                string listurl = NowUrl;
                NowUrlList.Add(NowUrl);
                NowUrl = "";
                if (gi.ws.IsNext == 1)
                {
                    if (gi.ws.NextMode == 1)
                    {
                        #region 采集内容链接列表分页（列表分页为单页采集模式）
                        if (CentUrlList.Count > 0 && cindex < CentUrlList.Count)
                        {
                            NowUrl = CentUrlList[cindex].Trim();
                            cindex++;
                        }
                        #endregion
                    }
                    else
                    {
                        #region 采集内容链接列表分页（列表分页为多页采集模式）
                        if (gi.ws.NextRangeRule != "")
                        {
                            string s = wapp.GatherApp.GetRegTxtString(ListLinkContent, gi.ws.NextRangeRule, gi.ws.NextRangeRuleIndex);
                            if (s != "")
                            {
                                s = wapp.GatherApp.GetRegsTxt(s, gi.ws.NextRule, gi.ws.NextRuleIndex);
                                if (s != "")
                                {
                                    NowUrl = wapp.GatherApp.GetUrls(wapp.GatherApp.FormatUrl(listurl, s));
                                }
                            }
                        }
                        else
                        {
                            string s = wapp.GatherApp.GetRegsTxt(ListLinkContent, gi.ws.NextRule, gi.ws.NextRuleIndex);
                            if (s != "")
                            {
                                NowUrl = wapp.GatherApp.GetUrls(wapp.GatherApp.FormatUrl(listurl, s));
                            }
                        }
                        #endregion
                    }
                    if (NowUrl != "" && !IsListLinkEcho(NowUrl))
                    {
                        string errs = "";
                        ListLinkContent = wapp.GatherApp.GetHttp(det, NowUrl, gi.ws.CodingMode, ref PageEncoding, ref PageCoding, ref errs);
                        if (errs != "")
                        {
                            ListLinkContent = "";
                            lout.OT(1, "列表页采集失败，错误信息：" + errs + "，[Url:" + NowUrl + "]");
                        }
                    }
                    else
                    {
                        ListLinkContent = "";
                    }
                }
                NowUrlCount++;
            }
        }

        /// <summary>检查指定的列表链接是否重复，如果重复返回true,否则返回false</summary>
        /// <param name="links">列表链接</param>
        /// <returns>检查指定的列表链接是否重复，如果重复返回true,否则返回false</returns>
        public bool IsListLinkEcho(string links)
        {
            if (NowUrlList.IndexOf(links) > -1)
            {
                return true;
            }
            return false;
        }

        /// <summary>检查指定的内容页链接是否重复，如果重复返回true,否则返回false</summary>
        /// <param name="links">内容页链接</param>
        /// <returns>检查指定的内容页链接是否重复，如果重复返回true,否则返回false</returns>
        public bool IsContLinkEcho(string links)
        {
            if (NowContLinkList.IndexOf(links) > -1)
            {
                return true;
            }
            if (gi.ws.IsEcho == 0)
            {
                //return true;
            }
            return false;
        }
    }
}
