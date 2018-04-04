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

namespace wapp
{
    public class GatherInfo
    {
        #region 采集信息项目属性

        /// <summary>网络蜘蛛ID</summary>
        public string SpiderID = "";

        /// <summary>网络蜘蛛对象</summary>
        public WebSpider ws = null;

        /// <summary>网络蜘蛛内容对象列表</summary>
        public List<WebSpiderInfo> wsilist = new List<WebSpiderInfo>();

        /// <summary>采集信息内存表</summary>
        public DataTable gdt = new DataTable();

        /// <summary>是否已读取网络蜘蛛信息</summary>
        public bool IsLoad = false;

        #endregion

        /// <summary>读取网络蜘蛛设置</summary>
        public void LoadWebSpiderInfo()
        {
            LoadWebSpiderInfo(SpiderID);
        }

        /// <summary>读取网络蜘蛛设置</summary>
        /// <param name="wsid">网络蜘蛛ID</param>
        public void LoadWebSpiderInfo(string wsid)
        {
            IsLoad = false;
            SpiderID = wsid;
            DataRow[] sdrs = wapp.WebSpiderList.WebSpiderTable.Select("SpiderID='" + wsid + "'");
            if (sdrs.Length <= 0)
            {
                return;
            }
            ws = new WebSpider();
            ws.SpiderID = DataOften.GetStr(sdrs, "SpiderID");
            ws.SpiderName = DataOften.GetStr(sdrs, "SpiderName");
            ws.UrlList = DataOften.GetStr(sdrs, "UrlList");
            ws.UrlRangeRule = DataOften.GetStr(sdrs, "UrlRangeRule").Replace("\\[RegRuleZhyNoLabel88088]", "([\\S\\s][^<>]*?)").Replace("\\[RegRuleZhyLabel88088]", "([\\S\\s]*?)");
            ws.UrlRule = DataOften.GetStr(sdrs, "UrlRule").Replace("\\[RegRuleZhyNoLabel88088]", "([\\S\\s][^<>]*?)").Replace("\\[RegRuleZhyLabel88088]", "([\\S\\s]*?)");
            ws.NextRangeRule = DataOften.GetStr(sdrs, "NextRangeRule").Replace("\\[RegRuleZhyNoLabel88088]", "([\\S\\s][^<>]*?)").Replace("\\[RegRuleZhyLabel88088]", "([\\S\\s]*?)");
            ws.NextRule = DataOften.GetStr(sdrs, "NextRule").Replace("\\[RegRuleZhyNoLabel88088]", "([\\S\\s][^<>]*?)").Replace("\\[RegRuleZhyLabel88088]", "([\\S\\s]*?)");
            ws.UrlRangeRuleIndex = AppPub.GetInt(DataOften.GetStr(sdrs, "UrlRangeRuleIndex", "0"));
            ws.UrlRuleIndex = AppPub.GetInt(DataOften.GetStr(sdrs, "UrlRuleIndex", "0"));
            ws.NextRangeRuleIndex = AppPub.GetInt(DataOften.GetStr(sdrs, "NextRangeRuleIndex", "0"));
            ws.NextRuleIndex = AppPub.GetInt(DataOften.GetStr(sdrs, "NextRuleIndex", "0"));
            ws.IsSaveHtml = AppPub.GetInt(DataOften.GetStr(sdrs, "IsSaveHtml", "0"));
            ws.HtmlSavePath = DataOften.GetStr(sdrs, "HtmlSavePath");
            ws.ExcelSavePath = DataOften.GetStr(sdrs, "ExcelSavePath");
            ws.OrderNum = AppPub.GetInt(DataOften.GetStr(sdrs, "OrderNum", "0"));
            ws.GatherCount = AppPub.GetInt(DataOften.GetStr(sdrs, "GatherCount", "0"));
            ws.CodingMode = AppPub.GetInt(DataOften.GetStr(sdrs, "CodingMode", "0"));
            ws.PageCoding = DataOften.GetStr(sdrs, "PageCoding");
            ws.GatherMaxNum = AppPub.GetInt(DataOften.GetStr(sdrs, "GatherMaxNum", "0"));
            ws.IsEcho = AppPub.GetInt(DataOften.GetStr(sdrs, "IsEcho", "0"));
            ws.IsNext = AppPub.GetInt(DataOften.GetStr(sdrs, "IsNext", "0"));
            ws.NextMode = AppPub.GetInt(DataOften.GetStr(sdrs, "NextMode", "0"));
            ws.ConNextRangeRule = DataOften.GetStr(sdrs, "ConNextRangeRule").Replace("\\[RegRuleZhyNoLabel88088]", "([\\S\\s][^<>]*?)").Replace("\\[RegRuleZhyLabel88088]", "([\\S\\s]*?)");
            ws.ConNextRule = DataOften.GetStr(sdrs, "ConNextRule").Replace("\\[RegRuleZhyNoLabel88088]", "([\\S\\s][^<>]*?)").Replace("\\[RegRuleZhyLabel88088]", "([\\S\\s]*?)");
            ws.ConNextRangeRuleIndex = AppPub.GetInt(DataOften.GetStr(sdrs, "ConNextRangeRuleIndex", "0"));
            ws.ConNextRuleIndex = AppPub.GetInt(DataOften.GetStr(sdrs, "ConNextRuleIndex", "0"));
            ws.ConIsNext = AppPub.GetInt(DataOften.GetStr(sdrs, "ConIsNext", "0"));
            ws.ConNextMode = AppPub.GetInt(DataOften.GetStr(sdrs, "ConNextMode", "0"));
            DataRow[] sidrs = wapp.WebSpiderInfoList.WebSpiderInfoTable.Select("SpiderID='" + wsid + "'");
            if (sidrs.Length <= 0)
            {
                return;
            }
            Table table = new Table("GTable");
            bool iscol = false;
            for (int i = 0; i < sidrs.Length; i++)
            {
                WebSpiderInfo wsi = new wapp.WebSpiderInfo();
                wsi.ListID = DataOften.GetStr(sidrs, "ListID", i);
                wsi.SpiderID = DataOften.GetStr(sidrs, "SpiderID", i);
                wsi.ListTitle = DataOften.GetStr(sidrs, "ListTitle", i);
                wsi.ListCode = DataOften.GetStr(sidrs, "ListCode", i);
                wsi.ContentRangeRule = DataOften.GetStr(sidrs, "ContentRangeRule", i).Replace("\\[RegRuleZhyNoLabel88088]", "([\\S\\s][^<>]*?)").Replace("\\[RegRuleZhyLabel88088]", "([\\S\\s]*?)");
                wsi.ContentRule = DataOften.GetStr(sidrs, "ContentRule", i).Replace("\\[RegRuleZhyNoLabel88088]", "([\\S\\s][^<>]*?)").Replace("\\[RegRuleZhyLabel88088]", "([\\S\\s]*?)");
                wsi.ContentRangeRuleIndex = AppPub.GetInt(DataOften.GetStr(sidrs, "ContentRangeRuleIndex", i, "0"));
                wsi.ContentRuleIndex = AppPub.GetInt(DataOften.GetStr(sidrs, "ContentRuleIndex", i, "0"));
                wsi.ContentFilt = DataOften.GetStr(sidrs, "ContentFilt", i); 
                wsi.ContentRegFilt = DataOften.GetStr(sidrs, "ContentRegFilt", i);
                wsi.ContentType = AppPub.GetInt(DataOften.GetStr(sidrs, "ContentType", i, "0"));
                wsi.OrderNum = AppPub.GetInt(DataOften.GetStr(sidrs, "OrderNum", i, "0"));
                wsi.ContentCodeMode = AppPub.GetInt(DataOften.GetStr(sidrs, "ContentCodeMode", i, "0"));
                if (wsi.ContentType == 1)
                {
                    table.Add(wsi.ListTitle, "double", 0);
                }
                else if (wsi.ContentType == 2)
                {
                    table.Add(wsi.ListTitle, "datetime", 0);
                }
                else
                {
                    table.Add(wsi.ListTitle, "string", 0);
                }
                wsilist.Add(wsi);
                iscol = true;
            }
            gdt = DataTables.GetDataTable(table);
            if (iscol)
            {
                IsLoad = true;
            }
        }

    }
}
