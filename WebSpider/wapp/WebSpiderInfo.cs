using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    /// <summary>网络蜘蛛明细采集对象</summary>
    public class WebSpiderInfo
    {
        /// <summary>蜘蛛明细ID</summary>
        public string ListID = "";

        /// <summary>蜘蛛ID</summary>
        public string SpiderID = "";

        /// <summary>列表标题</summary>
        public string ListTitle = "";

        /// <summary>列表代码</summary>
        public string ListCode = "";

        /// <summary>内容范围匹配规则 (多个匹配规则用回车键分隔)</summary>
        public string ContentRangeRule = "";

        /// <summary>内容范围匹配索引位置(从0开始)</summary>
        public int ContentRangeRuleIndex = 0;

        /// <summary>内容匹配规则 (多个匹配规则用回车键分隔)</summary>
        public string ContentRule = "";

        /// <summary>内容匹配索引位置(从0开始)</summary>
        public int ContentRuleIndex = 0;

        /// <summary>内容过滤器</summary>
        public string ContentFilt = "";

        /// <summary>正则表达式过滤列表</summary>
        public string ContentRegFilt = "";

        /// <summary>内容类型</summary>
        public int ContentType = 0;

        /// <summary>排序</summary>
        public int OrderNum = 0;

        /// <summary>内容解码模式,=0：不解码,=1：Html解码,=2：Url解码,=3：Escape解码,=4：EURI解码,=11：Html编码,=12：Url编码,=13：Escape编码,=14：EURI编码</summary>
        public int ContentCodeMode = 0;
    }
}
