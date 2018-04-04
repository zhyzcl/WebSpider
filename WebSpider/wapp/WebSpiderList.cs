using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web.Security;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Runtime.InteropServices;
using App;
using App.Win;
using App.Web;
using Microsoft.Win32;

namespace wapp
{
    /// <summary>网络蜘蛛内存列表</summary>
    public class WebSpiderList
    {
        /// <summary>网络蜘蛛列表缓存表</summary>
        private static DataTable _WebSpiderTable;

        /// <summary>网络蜘蛛列表缓存表</summary>
        public static DataTable WebSpiderTable
        {
            get
            {
                if (_WebSpiderTable == null)
                {
                    CreateWebSpiderTable();
                    LoadWebSpiderTable();
                }
                return _WebSpiderTable;
            }
            set { _WebSpiderTable = value; }
        }

        /// <summary>创建网络蜘蛛列表</summary>
        public static void CreateWebSpiderTable()
        {
            _WebSpiderTable = wapp.AppList.GetConfigDataTable(wapp.AppList.WebSpiderTableName);
        }

        /// <summary>读取网络蜘蛛列表</summary>
        public static void LoadWebSpiderTable()
        {
            string upath = wapp.AppList.SaveConfigPath + _WebSpiderTable.TableName + ".xml";
            if (File.Exists(upath))
            {
                try
                {
                    _WebSpiderTable = new DataTable();
                    _WebSpiderTable.ReadXml(upath);
                }
                catch
                {
                    _WebSpiderTable.Clear();
                }
            }
        }

        /// <summary>保存网络蜘蛛列表</summary>
        /// <param name="ws">网络蜘蛛对象</param>
        public static void SaveWebSpiderTable(WebSpider ws)
        {
            string upath = wapp.AppList.SaveConfigPath + _WebSpiderTable.TableName + ".xml";          
            if (ws != null)
            {
                DateTime dqrq = DateTime.Now;
                DataRow[] dr = _WebSpiderTable.Select("SpiderName='" + ws.SpiderName + "'");
                if (dr.Length > 0)
                {
                    dr[0]["UrlList"] = ws.UrlList;
                    dr[0]["UrlRangeRule"] = ws.UrlRangeRule;
                    dr[0]["UrlRule"] = ws.UrlRule;
                    dr[0]["UrlRangeRuleIndex"] = ws.UrlRangeRuleIndex;
                    dr[0]["UrlRuleIndex"] = ws.UrlRuleIndex;
                    dr[0]["IsSaveHtml"] = ws.IsSaveHtml;
                    dr[0]["HtmlSavePath"] = ws.HtmlSavePath;
                    dr[0]["ExcelSavePath"] = ws.ExcelSavePath;
                    dr[0]["OrderNum"] = ws.OrderNum;
                    dr[0]["OperDate"] = dqrq;
                    dr[0]["NextRangeRule"] = ws.NextRangeRule;
                    dr[0]["NextRule"] = ws.NextRule;
                    dr[0]["NextRangeRuleIndex"] = ws.NextRangeRuleIndex;
                    dr[0]["NextRuleIndex"] = ws.NextRuleIndex;
                    dr[0]["CodingMode"] = ws.CodingMode;
                    dr[0]["UrlRangeRule"] = ws.UrlRangeRule;
                    dr[0]["PageCoding"] = ws.PageCoding;
                    dr[0]["GatherMaxNum"] = ws.GatherMaxNum;
                    dr[0]["IsEcho"] = ws.IsEcho;
                    dr[0]["IsNext"] = ws.IsNext;
                    dr[0]["NextMode"] = ws.NextMode;
                    dr[0]["ConNextRangeRule"] = ws.ConNextRangeRule;
                    dr[0]["ConNextRule"] = ws.ConNextRule;
                    dr[0]["ConNextRangeRuleIndex"] = ws.ConNextRangeRuleIndex;
                    dr[0]["ConNextRuleIndex"] = ws.ConNextRuleIndex;
                    dr[0]["ConIsNext"] = ws.ConIsNext;
                    dr[0]["ConNextMode"] = ws.ConNextMode;
                    dr[0]["ConIsNext"] = ws.ConIsNext;
                    dr[0]["ConNextMode"] = ws.ConNextMode;
                }
                else
                {
                    DataRow newRow = _WebSpiderTable.NewRow();
                    newRow["SpiderID"] = ws.SpiderID;
                    newRow["SpiderName"] = ws.SpiderName;
                    newRow["UrlList"] = ws.UrlList;
                    newRow["UrlRangeRule"] = ws.UrlRangeRule;
                    newRow["UrlRule"] = ws.UrlRule;
                    newRow["UrlRangeRuleIndex"] = ws.UrlRangeRuleIndex;
                    newRow["UrlRuleIndex"] = ws.UrlRuleIndex;
                    newRow["IsSaveHtml"] = ws.IsSaveHtml;
                    newRow["HtmlSavePath"] = ws.HtmlSavePath;
                    newRow["ExcelSavePath"] = ws.ExcelSavePath;
                    newRow["OrderNum"] = ws.OrderNum;
                    newRow["AddDate"] = dqrq;
                    newRow["OperDate"] = dqrq;
                    newRow["NextRangeRule"] = ws.NextRangeRule;
                    newRow["NextRule"] = ws.NextRule;
                    newRow["NextRangeRuleIndex"] = ws.NextRangeRuleIndex;
                    newRow["NextRuleIndex"] = ws.NextRuleIndex;
                    newRow["CodingMode"] = ws.CodingMode;
                    newRow["UrlRangeRule"] = ws.UrlRangeRule;
                    newRow["PageCoding"] = ws.PageCoding;
                    newRow["GatherMaxNum"] = ws.GatherMaxNum;
                    newRow["IsEcho"] = ws.IsEcho;
                    newRow["IsNext"] = ws.IsNext;
                    newRow["NextMode"] = ws.NextMode;
                    newRow["ConNextRangeRule"] = ws.ConNextRangeRule;
                    newRow["ConNextRule"] = ws.ConNextRule;
                    newRow["ConNextRangeRuleIndex"] = ws.ConNextRangeRuleIndex;
                    newRow["ConNextRuleIndex"] = ws.ConNextRuleIndex;
                    newRow["ConIsNext"] = ws.ConIsNext;
                    newRow["ConNextMode"] = ws.ConNextMode;
                    _WebSpiderTable.Rows.Add(newRow);
                }
            }
            _WebSpiderTable = DataOften.GetSortTable(_WebSpiderTable, "OrderNum asc");
            if (File.Exists(upath))
            {
                File.Delete(upath);
            }
            _WebSpiderTable.WriteXml(upath, XmlWriteMode.WriteSchema);
        }

        /// <summary>根据排序值重新设置排序</summary>
        public static void SetTableOrderNum()
        {
            _WebSpiderTable = DataOften.GetSortTable(_WebSpiderTable, "OrderNum asc");
            for (int i = 0; i < _WebSpiderTable.Rows.Count; i++)
            {
                _WebSpiderTable.Rows[i]["OrderNum"] = i + 1;
            }
            string upath = wapp.AppList.SaveConfigPath + _WebSpiderTable.TableName + ".xml";
            if (File.Exists(upath))
            {
                File.Delete(upath);
            }
            _WebSpiderTable.WriteXml(upath, XmlWriteMode.WriteSchema);
        }
    }
}
