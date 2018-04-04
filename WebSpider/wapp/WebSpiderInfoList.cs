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
    public class WebSpiderInfoList
    {
        /// <summary>网络蜘蛛列表缓存表</summary>
        private static DataTable _WebSpiderInfoTable;

        /// <summary>网络蜘蛛列表缓存表</summary>
        public static DataTable WebSpiderInfoTable
        {
            get
            {
                if (_WebSpiderInfoTable == null)
                {
                    CreateWebSpiderInfoTable();
                    LoadWebSpiderInfoTable();
                }
                return _WebSpiderInfoTable;
            }
            set { _WebSpiderInfoTable = value; }
        }

        /// <summary>创建网络蜘蛛列表</summary>
        public static void CreateWebSpiderInfoTable()
        {
            _WebSpiderInfoTable = wapp.AppList.GetConfigDataTable(wapp.AppList.WebSpiderInfoTableName);
        }

        /// <summary>读取网络蜘蛛列表</summary>
        public static void LoadWebSpiderInfoTable()
        {
            string upath = wapp.AppList.SaveConfigPath + _WebSpiderInfoTable.TableName + ".xml";
            if (File.Exists(upath))
            {
                try
                {
                    _WebSpiderInfoTable = new DataTable();
                    _WebSpiderInfoTable.ReadXml(upath);
                }
                catch
                {
                    _WebSpiderInfoTable.Clear();
                }
            }
        }

        /// <summary>保存网络蜘蛛列表</summary>
        /// <param name="wsi">网络蜘蛛明细对象</param>
        public static void SaveWebSpiderInfoTable(WebSpiderInfo wsi)
        {
            string upath = wapp.AppList.SaveConfigPath + _WebSpiderInfoTable.TableName + ".xml";
            if (wsi != null)
            {
                DateTime dqrq = DateTime.Now;
                DataRow[] dr = _WebSpiderInfoTable.Select("ListTitle='" + wsi.ListTitle + "'");
                if (dr.Length > 0)
                {
                    dr[0]["ListTitle"] = wsi.ListTitle;
                    dr[0]["ListCode"] = wsi.ListCode;
                    dr[0]["ContentRangeRule"] = wsi.ContentRangeRule;
                    dr[0]["ContentRule"] = wsi.ContentRule;
                    dr[0]["ContentRangeRuleIndex"] = wsi.ContentRangeRuleIndex;
                    dr[0]["ContentRuleIndex"] = wsi.ContentRuleIndex;
                    dr[0]["ContentFilt"] = wsi.ContentFilt;
                    dr[0]["ContentRegFilt"] = wsi.ContentRegFilt;
                    dr[0]["ContentType"] = wsi.ContentType;
                    dr[0]["OrderNum"] = wsi.OrderNum;
                    dr[0]["ContentCodeMode"] = wsi.ContentCodeMode;

                }
                else
                {
                    DataRow newRow = _WebSpiderInfoTable.NewRow();
                    newRow["ListID"] = wsi.ListID;
                    newRow["SpiderID"] = wsi.SpiderID;
                    newRow["ListTitle"] = wsi.ListTitle;
                    newRow["ListCode"] = wsi.ListCode;
                    newRow["ContentRangeRule"] = wsi.ContentRangeRule;
                    newRow["ContentRule"] = wsi.ContentRule;
                    newRow["ContentRangeRuleIndex"] = wsi.ContentRangeRuleIndex;
                    newRow["ContentRuleIndex"] = wsi.ContentRuleIndex;
                    newRow["ContentFilt"] = wsi.ContentFilt;
                    newRow["ContentRegFilt"] = wsi.ContentRegFilt;
                    newRow["ContentType"] = wsi.ContentType;
                    newRow["OrderNum"] = wsi.OrderNum;
                    newRow["ContentCodeMode"] = wsi.ContentCodeMode;
                    _WebSpiderInfoTable.Rows.Add(newRow);
                }
            }
            if (File.Exists(upath))
            {
                File.Delete(upath);
            }
            _WebSpiderInfoTable = DataOften.GetSortTable(_WebSpiderInfoTable, "OrderNum asc");
            _WebSpiderInfoTable.WriteXml(upath, XmlWriteMode.WriteSchema);
        }

        /// <summary>根据排序值重新设置排序</summary>
        public static void SetTableOrderNum(string pid)
        {
            _WebSpiderInfoTable = DataOften.GetSortTable(_WebSpiderInfoTable, "OrderNum asc");
            int sn = 1;
            for (int i = 0; i < _WebSpiderInfoTable.Rows.Count; i++)
            {
                if (pid != "")
                {
                    string spid = _WebSpiderInfoTable.Rows[i]["SpiderID"].ToString().Trim();
                    if (spid == pid)
                    {
                        _WebSpiderInfoTable.Rows[i]["OrderNum"] = sn;
                        sn++;
                    }
                }
                else
                {
                    _WebSpiderInfoTable.Rows[i]["OrderNum"] = sn;
                    sn++;
                }
            }
            string upath = wapp.AppList.SaveConfigPath + _WebSpiderInfoTable.TableName + ".xml";
            if (File.Exists(upath))
            {
                File.Delete(upath);
            }
            _WebSpiderInfoTable.WriteXml(upath, XmlWriteMode.WriteSchema);
        }
    }
}
