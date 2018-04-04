using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Web;
using System.Windows.Forms;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    public class GatherApp
    {
        /// <summary>根据由换行符分隔的字符串返回Url列表数组</summary>
        /// <param name="s">由换行符分隔的字符串</param>
        /// <returns>根据由换行符分隔的字符串返回</returns>
        public static List<string> GetUrlList(string s)
        {
            List<string> lists = new List<string>();
            if (s != "")
            {
                string[] sArray = s.Split('\n');
                for (int i = 0; i < sArray.Length; i++)
                {
                    string ss = GetUrls(sArray[i].Trim());
                    if (ss != "")
                    {
                        lists.Add(ss);
                    }
                }
            }
            return lists;
        }

        /// <summary>如果指定的Url正确则返回Url字符串，否则返回空字符串</summary>
        /// <param name="url">指定的Url</param>
        /// <returns>如果指定的Url正确则返回Url字符串，否则返回空字符串</returns>
        public static string GetUrls(string url)
        {
            if (Often.IsUrl(url))
            {
                if (url.IndexOf("/", 8) == -1)
                {
                    url += "/";
                }
                return url;
            }
            return "";
        }

        /// <summary>根据指定的字符串和由换行符分隔的正则表达式数组字符串返回匹配字符串列表</summary>
        /// <param name="s">指定的字符串</param>
        /// <param name="reglist">由换行符分隔的正则表达式数组字符串</param>
        /// <param name="index">获取指定匹配索引位置</param>
        /// <returns>根据指定的字符串和由换行符分隔的正则表达式数组字符串返回匹配字符串列表</returns>
        public static List<string> GetRegTxtList(string s, string reglist, int index)
        {
            List<string> lists = new List<string>();
            if (s != "" || reglist != "")
            {
                try
                {
                    string[] tArray = reglist.Split('\n');
                    for (int i = 0; i < tArray.Length; i++)
                    {
                        MatchCollection mcoll = GetRegColl(s, tArray[i].Trim());
                        if (mcoll.Count != 0)
                        {
                            for (int ii = 0; ii < mcoll.Count; ii++)
                            {
                                int vindex = 1 + index;
                                if (mcoll[ii].Groups.Count > vindex)
                                {
                                    lists.Add(mcoll[ii].Groups[vindex].Value.Trim());
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return lists;
        }

        /// <summary>根据指定的字符串和由换行符分隔的正则表达式数组字符串返回所有匹配字符串叠加后的字符串</summary>
        /// <param name="s">指定的字符串</param>
        /// <param name="reglist">由换行符分隔的正则表达式数组字符串</param>
        /// <param name="index">获取匹配的索引位置内容</param>
        /// <returns>根据指定的字符串和由换行符分隔的正则表达式数组字符串返回所有匹配字符串叠加后的字符串</returns>
        public static string GetRegTxtString(string s, string reglist, int index)
        {
            StringBuilder sb = new StringBuilder();
            if (s != "" || reglist != "")
            {
                try
                {
                    string[] tArray = reglist.Split('\n');
                    for (int i = 0; i < tArray.Length; i++)
                    {
                        MatchCollection mcoll = GetRegColl(s, tArray[i].Trim());
                        if (mcoll.Count != 0)
                        {
                            for (int ii = 0; ii < mcoll.Count; ii++)
                            {
                                int vindex = 1 + index;
                                if (mcoll[ii].Groups.Count > vindex)
                                {
                                    string vs = mcoll[ii].Groups[vindex].Value;
                                    sb.Append(vs);
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            return sb.ToString();
        }

        /// <summary>返回正则表达式匹配集合</summary>
        /// <param name="s">指定的字符串</param>
        /// <param name="pattern">由正则表达式</param>
        /// <returns>返回正则表达式匹配集合</returns>
        public static MatchCollection GetRegColl(string s, string pattern)
        {
            Regex regs = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mcoll = regs.Matches(s);
            return mcoll;
        }

        /// <summary>根据指定的字符串和由换行符分隔的正则表达式数组过滤字符串返回过滤后的字符串</summary>
        /// <param name="s">指定的字符串</param>
        /// <param name="filters">由换行符分隔的正则表达式数组过滤字符串</param>
        /// <returns>根据指定的字符串和由换行符分隔的正则表达式数组过滤字符串返回过滤后的字符串</returns>
        public static string Filters(string s, string filters)
        {
            if (s != "" || filters.Trim() != "")
            {
                try
                {
                    string[] tArray = filters.Split('\n');
                    for (int i = 0; i < tArray.Length; i++)
                    {
                        s = Regex.Replace(s, tArray[i], "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    }

                }
                catch
                {
                }
            }
            return s;
        }

        /// <summary>返回字节数组最接近编码</summary>
        /// <param name="sr">字节数组</param>
        /// <returns>返回字节数组最接近编码</returns>
        public static string GetBytesEncoding(byte[] sr)
        {
            ByteEncode be = new ByteEncode();
            return be.GetByCode(sr);
        }

        /// <summary>返回匹配html内文件链接地址正则表达式</summary>
        /// <param name="exts">可用文件后缀集合，多个后缀使用|分割</param>
        /// <returns>返回匹配html内文件链接地址正则表达式</returns>
        public static string GetSrcReg(string exts)
        {
            return "<[\\S\\s][^>]*(\\ssrc=|\\svalue=|\\shref=|\\sbackground=)('|\")?([^>\"'\\s]*\\.(" + exts + "))('|\")?[\\S\\s][^>]*>";
        }

        /// <summary>读取图片文件的尺寸</summary>
        /// <param name="fpath">图片相对地址</param>
        /// <param name="width">返回图片的宽度</param>
        /// <param name="height">返回图片的高度</param>
        public static void LoadImageSize(string fpath, ref int width, ref int height)
        {
            try
            {
                System.Drawing.Image im = System.Drawing.Image.FromFile(fpath);
                width = im.Width;
                height = im.Height;
                im.Dispose();
            }
            catch
            {
            }
        }

        /// <summary>根据基础Url地址返回相对Url地址的绝对Url地址</summary>
        /// <param name="baseUrl">基础Url地址</param>
        /// <param name="theUrl">相对Url地址</param>
        /// <returns>根据基础Url地址返回相对Url地址的绝对Url地址</returns>
        public static string FormatUrl(string baseUrl, string theUrl)
        {
            if (baseUrl.Trim() == "")
            {
                return theUrl;
            }
            if (theUrl.StartsWith("?"))
            {
                int czint = baseUrl.IndexOf("?");
                if (czint == -1)
                {
                    theUrl = baseUrl + theUrl;
                }
                else
                {
                    theUrl = baseUrl.Substring(0, czint) + theUrl;
                }
                return theUrl;
            }
            int numx = 0;
            string textx = baseUrl.Substring(0, baseUrl.IndexOf("/", 8));
            baseUrl = baseUrl.Substring(0, baseUrl.LastIndexOf("/"));
            if (theUrl.StartsWith("./"))
            {
                theUrl = theUrl.Remove(0, 1);
                theUrl = baseUrl + theUrl;
            }
            else if (theUrl.StartsWith("/"))
            {
                theUrl = textx + theUrl;
            }
            else if (theUrl.StartsWith("../"))
            {
                while (theUrl.StartsWith("../"))
                {
                    numx += 1;
                    theUrl = theUrl.Remove(0, 3);
                }
                for (int i = 0; i < numx; i++)
                {
                    baseUrl = baseUrl.Substring(0, baseUrl.LastIndexOf("/", baseUrl.Length - 2));
                }
                theUrl = baseUrl + "/" + theUrl;
            }
            if ((!theUrl.StartsWith("http://")) && (!theUrl.StartsWith("https://")))
            {
                theUrl = baseUrl + "/" + theUrl;
            }
            return theUrl;
        }

        /// <summary>将采集到的日期转换成正常日期格式</summary>
        /// <param name="s">采集到的日期</param>
        /// <returns>将采集到的日期转换成正常日期格式</returns>
        public static string FormatDate(string s)
        {
            if (Often.IsDate(s))
            {
                return s;
            }
            try
            {
                string dates = s.Trim().Replace("年", "-");
                dates = dates.Replace("月", "-");
                dates = dates.Replace("日", " ");
                dates = dates.Replace("时", ":");
                dates = dates.Replace("分", ":");
                dates = dates.Replace("秒", "");
                dates = dates.Replace("&nbsp;", " ");
                dates = dates.Replace("　", " ");
                dates = dates.Replace("    ", " ");
                dates = dates.Replace("   ", " ");
                dates = dates.Replace("  ", " ");

                string ns = "";
                string ys = "01";
                string rs = "01";
                string ss = "00";
                string fs = "00";
                string ms = "00";
                bool iscontinue = false;
                int place = dates.IndexOf("-");
                if (place > -1)
                {
                    iscontinue = false;
                    ns = dates.Substring(0, place);
                    if (!IsDateNum(ns, "y"))
                    {
                        ns = "";
                    }
                    else
                    {
                        iscontinue = true;
                    }
                    dates = dates.Remove(0, place + 1).Trim();
                }
                place = dates.IndexOf("-");
                if (place > -1 && iscontinue)
                {
                    iscontinue = false;
                    ys = dates.Substring(0, place);
                    if (!IsDateNum(ys, "m"))
                    {
                        ys = "01";
                    }
                    else
                    {
                        iscontinue = true;
                    }
                    dates = dates.Remove(0, place + 1).Trim();
                }
                else if (iscontinue)
                {
                    ys = dates;
                    if (!IsDateNum(ys, "m"))
                    {
                        ys = "01";
                    }
                }
                place = dates.IndexOf(" ");
                if (place > -1 && iscontinue)
                {
                    iscontinue = false;
                    rs = dates.Substring(0, place);
                    if (!IsDateNum(rs, "d"))
                    {
                        rs = "01";
                    }
                    else
                    {
                        iscontinue = true;
                    }
                    dates = dates.Remove(0, place + 1).Trim();
                }
                else if (iscontinue)
                {
                    rs = dates;
                    if (!IsDateNum(rs, "d"))
                    {
                        rs = "01";
                    }
                }
                place = dates.IndexOf(":");
                if (place > -1 && iscontinue)
                {
                    iscontinue = false;
                    ss = dates.Substring(0, place);
                    if (!IsDateNum(ss, "h"))
                    {
                        ss = "00";
                    }
                    else
                    {
                        iscontinue = true;
                    }
                    dates = dates.Remove(0, place + 1).Trim();
                }
                else if (iscontinue)
                {
                    ss = dates;
                    if (!IsDateNum(ss, "h"))
                    {
                        ss = "00";
                    }
                }
                place = dates.IndexOf(":");
                if (place > -1 && iscontinue)
                {
                    iscontinue = false;
                    fs = dates.Substring(0, place);
                    if (!IsDateNum(fs, "mi"))
                    {
                        fs = "00";
                    }
                    else
                    {
                        iscontinue = true;
                    }
                    dates = dates.Remove(0, place + 1).Trim();
                }
                else if (iscontinue)
                {
                    fs = dates;
                    if (!IsDateNum(fs, "mi"))
                    {
                        fs = "00";
                    }
                }
                place = dates.IndexOf(":");
                if (place > -1 && iscontinue)
                {
                    iscontinue = false;
                    ms = dates.Substring(0, place);
                    if (!IsDateNum(ms, "s"))
                    {
                        ms = "00";
                    }
                    else
                    {
                        iscontinue = true;
                    }
                    dates = dates.Remove(0, place + 1).Trim();
                }
                else if (iscontinue)
                {
                    ms = dates;
                    if (!IsDateNum(ms, "s"))
                    {
                        ms = "00";
                    }
                }
                if (ns == "")
                {
                    return s;
                }
                return ns + "-" + ys + "-" + rs + " " + ss + ":" + fs + ":" + ms;
            }
            catch
            {
                return s;
            }
        }

        /// <summary>判断指定的字符串是否是日期格式，如果是则返回true 否则返回false</summary>
        /// <param name="s">指定的字符串</param>
        /// <param name="mode">日期模式：y：年，m：月，d：日，h：时，mi：分，s：秒</param>
        /// <returns>判断指定的字符串是否是日期格式，如果是则返回true 否则返回false</returns>
        public static bool IsDateNum(string s, string mode)
        {
            mode = mode.ToLower().Trim();
            if (!Often.IsInt32(s))
            {
                return false;
            }
            int dnum = Convert.ToInt32(s);
            if (mode == "y" && dnum >= 1800 && dnum <= 9999)
            {
                return true;
            }
            else if (mode == "m" && dnum >= 01 && dnum <= 12)
            {
                return true;
            }
            else if (mode == "d" && dnum >= 01 && dnum <= 31)
            {
                return true;
            }
            else if (mode == "h" && dnum >= 0 && dnum <= 24)
            {
                return true;
            }
            else if ((mode == "mi" || mode == "s") && dnum >= 0 && dnum <= 60)
            {
                return true;
            }
            return false;
        }

        /// <summary>读取指定字节数组编码</summary>
        /// <param name="det">获取字节流编码</param>
        /// <param name="sr">指定字节数组</param>
        /// <param name="pcoding">返回文本编码文本</param>
        public static Encoding GetEncoding(ByteEncode det, byte[] sr, ref string pcoding)
        {
            pcoding = det.GetByCode(sr);
            if (pcoding != "")
            {
                return Encoding.GetEncoding(pcoding);
            }
            else
            {
                return Encoding.Default;
            }
        }

        /// <summary>根据指定的url下载并返回文本数据</summary>
        /// <param name="det">获取字节流编码</param>
        /// <param name="surl">指定的url</param>
        /// <param name="cmode">页面编码获取模式,=0：自动,=1：每页自动,=2：自定义</param>
        /// <param name="pencoding">返回文本编码</param>
        /// <param name="pcoding">返回文本编码文本</param>
        /// <param name="errs">下载失败则返回错误信息</param>
        /// <returns>根据指定的url下载并返回文本数据</returns>
        public static string GetHttp(ByteEncode det, string surl, int cmode, ref Encoding pencoding, ref string pcoding, ref string errs)
        {
            try
            {
                string s = "";
                WebClient wclient = new WebClient();
                wclient.Credentials = CredentialCache.DefaultCredentials;
                byte[] bytes = wclient.DownloadData(Often.UrlEn(surl));
                if (cmode == 0)
                {
                    s = GetEncoding(det, bytes, ref pcoding).GetString(bytes);
                }
                else if (cmode == 1)
                {
                    s = GetEncoding(det, bytes, ref pcoding).GetString(bytes);
                }
                else if (cmode == 2)
                {
                    s = Encoding.GetEncoding(pcoding).GetString(bytes);
                }
                wclient.Dispose();
                return s;
            }
            catch (Exception ex)
            {
                errs = ex.Message;
                return "";
            }
        }

        /// <summary>根据指定的解码模式解码指定的字符串</summary>
        /// <param name="s">指定的字符串</param>
        /// <param name="cMode">解码模式,=0：不解码,=1：Html解码,=2：Url解码,=3：Escape解码,=4：EURI解码,=11：Html编码,=12：Url编码,=13：Escape编码,=14：EURI编码</param>
        /// <returns>根据指定的解码模式解码指定的字符串</returns>
        public static string GetUnCode(string s, int cMode)
        {
            if (cMode == 1)
            {
                return Often.HtmlDecode(s);
            }
            else if (cMode == 2)
            {
                return Often.UrlDes(s);
            }
            else if (cMode == 3)
            {
                return Often.Unescape(s);
            }
            else if (cMode == 4)
            {
                return Often.DecodeURI(s);
            }
            else if (cMode == 11)
            {
                return Often.HtmlEncode(s);
            }
            else if (cMode == 12)
            {
                return Often.UrlEn(s);
            }
            else if (cMode == 13)
            {
                return Often.Escape(s);
            }
            else if (cMode == 14)
            {
                return Often.EncodeURI(s);
            }
            return s;
        }

        /// <summary>返回正则表达式匹配指定的内容结果第一个文本</summary>
        /// <param name="s">指定的内容</param>
        /// <param name="matchs">匹配正则表达式</param>
        /// <returns>回正则表达式匹配指定的内容结果第一个文本</returns>
        public static string GetRegsTxt(string s, string matchs, int index)
        {
            List<string> clist = wapp.GatherApp.GetRegTxtList(s, matchs, index);
            for (int i = 0; i < clist.Count; i++)
            {
                string str = clist[i].Trim();
                if (str != "")
                {
                    return str;
                }
            }
            return "";
        }
    }
}
