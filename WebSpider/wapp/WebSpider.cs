using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    /// <summary>网络蜘蛛对象</summary>
    public class WebSpider
    {
        /// <summary>蜘蛛ID</summary>
        public string SpiderID = "";

        /// <summary>名称</summary>
        public string SpiderName = "";

        /// <summary>Url列表(多个Url用回车键分隔)</summary>
        public string UrlList = "";

        /// <summary>内容Url范围匹配规则(多个匹配规则用回车键分隔)</summary>
        public string UrlRangeRule = "";

        /// <summary>内容Url范围匹配索引位置(从0开始)</summary>
        public int UrlRangeRuleIndex = 0;

        /// <summary>内容Url匹配规则(多个匹配规则用回车键分隔)</summary>
        public string UrlRule = "";

        /// <summary>内容Url匹配索引位置(从0开始)</summary>
        public int UrlRuleIndex = 0;

        /// <summary>内容Url列表分页范围匹配规则 (多个匹配规则用回车键分隔)</summary>
        public string NextRangeRule = "";

        /// <summary>内容Url列表分页范围匹配索引位置(从0开始)</summary>
        public int NextRangeRuleIndex = 0;

        /// <summary>内容Url列表分页匹配规则(多个匹配规则用回车键分隔)</summary>
        public string NextRule = "";

        /// <summary>内容Url列表分页匹配索引位置(从0开始)</summary>
        public int NextRuleIndex = 0;

        /// <summary>是否保存html文件</summary>
        public int IsSaveHtml = 0;

        /// <summary>html文件保存路径</summary>
        public string HtmlSavePath = "";

        /// <summary>Excel文件保存路径</summary>
        public string ExcelSavePath = "";

        /// <summary>排序</summary>
        public int OrderNum = 0;

        /// <summary>新增日期</summary>
        public DateTime AddDate = DateTime.Now;

        /// <summary>操作日期</summary>
        public DateTime OperDate = DateTime.Now;

        /// <summary>最近采集日期</summary>
        public DateTime GatherDate = DateTime.Now;

        /// <summary>采集数据总数</summary>
        public int GatherCount = 0;

        /// <summary>页面编码获取模式,=0：自动,=1：每页自动,=2：自定义</summary>
        public int CodingMode = 0;

        /// <summary>页面编码</summary>
        public string PageCoding = "";

        /// <summary>一次最大采集数目</summary>
        public int GatherMaxNum = 0;

        /// <summary>是否允许重复内容页,=0：否,=1：是</summary>
        public int IsEcho = 0;

        /// <summary>是否采集内容页链接列表分页,=0：否,=1：是</summary>
        public int IsNext = 0;

        /// <summary>内容页链接列表分页采集模式,=0：多页匹配,=1：单页匹配</summary>
        public int NextMode = 0;

        /// <summary>内容页分页范围匹配规则 (多个匹配规则用回车键分隔)</summary>
        public string ConNextRangeRule = "";

        /// <summary>内容页分页范围匹配索引位置(从0开始)</summary>
        public int ConNextRangeRuleIndex = 0;

        /// <summary>内容页分页匹配规则(多个匹配规则用回车键分隔)</summary>
        public string ConNextRule = "";

        /// <summary>内容页分页匹配索引位置(从0开始)</summary>
        public int ConNextRuleIndex = 0;

        /// <summary>是否采集内容页分页,=0：否,=1：是</summary>
        public int ConIsNext = 0;

        /// <summary>内容页分页采集模式,=0：多页匹配,=1：单页匹配</summary>
        public int ConNextMode = 0;
    }
}
