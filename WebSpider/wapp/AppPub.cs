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

namespace wapp
{
    public class AppPub
    {
        /// <summary>写操作日志文件</summary>
        /// <param name="savePath">日志保存路径</param>
        /// <param name="errsb">日志信息</param>
        /// <param name="errs">信息</param>
        public static void SaveServerErrLog(string savePath, ref StringBuilder errsb, string errs)
        {
            if (errsb.Length > 100000)
            {
                errsb = new StringBuilder();
            }
            errsb.Append(errs);
            try
            {
                FileStream fs = File.Create(savePath);
                byte[] bContent = Encoding.GetEncoding("utf-8").GetBytes(errsb.ToString());
                fs.Write(bContent, 0, bContent.Length);
                fs.Close();
            }
            catch
            {
            }
        }

        /// <summary>返回当前程序运行路径</summary>
        /// <returns>返回当前程序运行路径</returns>
        public static string GetAssemblyPath()
        {
            string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8); // 8是 file:// 的长度   
            string[] arrSection = _CodeBase.Split(new char[] { '/' });
            string _FolderPath = "";
            for (int i = 0; i < arrSection.Length - 1; i++)
            {
                _FolderPath += arrSection[i] + "\\";
            }
            return _FolderPath;
        }

        /// <summary>文本窗显示委托方法</summary>
        /// <param name="myControl">文本对象</param>
        /// <param name="myCaption">需要插入的文本</param>
        /// <param name="isqc">是否清空文本对象true 是 false 否</param>
        /// <param name="blxx">基础文本</param>
        public static void RTB(RichTextBox myControl, string myCaption, bool isqc, string blxx)
        {
            if (isqc)
            {
                myControl.Text = "";
                myControl.AppendText(blxx);
            }
            myControl.AppendText(myCaption);
            myControl.ScrollToCaret();
        }

        /// <summary>文本显示委托方法</summary>
        /// <param name="myLabel">文本对象</param>
        /// <param name="myCaption">需要显示的文本</param>
        public static void LB(Label myLabel, string myCaption)
        {
            myLabel.Text=myCaption;
        }

        /// <summary>返回object数组</summary>
        /// <param name="objs">object数组</param>
        public static object[] GetArray(params object[] objs)
        {
            return objs;
        }

        /// <summary>根据列表值返回列表名称</summary>
        /// <param name="cdt">列表</param>
        /// <param name="val">列表值</param>
        /// <returns>根据列表值返回列表名称</returns>
        public static string GetListValue(DataTable cdt, string val)
        {
            if (cdt != null && Often.IsInt32(val))
            {
                if (cdt.Rows.Count > 0)
                {
                    DataRow[] dr = cdt.Select("id=" + val);
                    if (dr.Length > 0)
                    {
                        return dr[0]["name"].ToString().Trim();
                    }
                }
            }
            return "";
        }

        /// <summary>自动调整图片控件大小</summary>
        /// <param name="pic">图片控件</param>
        /// <param name="pan">图片空间容器</param>
        public static void AutoPictureBoxSize(PictureBox pic, Panel pan)
        {
            if (pic.Image != null)
            {
                try
                {
                    System.Drawing.Image imfile = pic.Image;
                    int imsw = pan.Width - 1;
                    int imsh = pan.Height - 1;
                    int plx = pan.Width / 2;
                    int ply = pan.Height / 2;
                    Often.AutoSizeNarrow(ref imsw, ref imsh, imfile.Size.Width, imfile.Size.Height);
                    pic.Width = imsw;
                    pic.Height = imsh;
                    pic.Top = ply - (pic.Height / 2);
                    pic.Left = plx - (pic.Width / 2);
                }
                catch
                {
                }
            }
        }

        /// <summary>根据条件设置ListViewItem内的值</summary>
        /// <param name="itema">需要设置的ListViewItem</param>
        /// <param name="val">值</param>
        /// <param name="defval">默认值</param>
        /// <param name="isval">条件</param>
        public static void SetListViewItem(ListViewItem itema, string val, string defval, bool isval)
        {
            if (isval)
            {
                itema.SubItems.Add(val);
            }
            else
            {
                itema.SubItems.Add(defval);
            }
        }

        /// <summary>设置导入文件内存表结构</summary>
        /// <param name="dt">内存表</param>
        public static void SetDataFilesName(DataTable dt)
        {
            dt.TableName = "DataFilesName";
            dt.Columns.Add("AddDate", Type.GetType("System.DateTime"));
            dt.Columns.Add("ExportMode", Type.GetType("System.Int32"));
            dt.Columns.Add("StatDate", Type.GetType("System.DateTime"));
            dt.Columns.Add("EndDate", Type.GetType("System.DateTime"));
        }

        /// <summary>设置DataGridView列</summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="index">列索引</param>
        /// <param name="title">列名</param>
        /// <param name="width">宽度</param>
        /// <param name="readOnly">是否只读</param>
        public static void SetDataGridView(DataGridView dgv, int index, string title, int width, bool readOnly)
        {
            SetDataGridView(dgv, index, title, width, readOnly, true, DataGridViewColumnSortMode.Automatic);
        }

        /// <summary>设置DataGridView列</summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="index">列索引</param>
        /// <param name="title">列名</param>
        /// <param name="width">宽度</param>
        /// <param name="readOnly">是否只读</param>
        /// <param name="visible">是否显示</param>
        public static void SetDataGridView(DataGridView dgv, int index, string title, int width, bool readOnly, bool visible)
        {
            SetDataGridView(dgv, index, title, width, readOnly, visible, DataGridViewColumnSortMode.Automatic);
        }

        /// <summary>设置DataGridView列</summary>
        /// <param name="dgv">DataGridView</param>
        /// <param name="index">列索引</param>
        /// <param name="title">列名</param>
        /// <param name="width">宽度</param>
        /// <param name="readOnly">是否只读</param>
        /// <param name="visible">是否显示</param>
        /// <param name="dgvcs">列排序模式</param>
        public static void SetDataGridView(DataGridView dgv, int index, string title, int width, bool readOnly, bool visible, DataGridViewColumnSortMode dgvcs)
        {
            dgv.Columns[index].HeaderText = title;
            dgv.Columns[index].Width = width;
            dgv.Columns[index].ReadOnly = readOnly;
            dgv.Columns[index].Visible = visible;
            dgv.Columns[index].SortMode = dgvcs;
        }

        /// <summary>关闭子窗口,如果不传递需要关闭的窗口名称则关闭所有窗口，关闭成功返回true，否则返回false</summary>
        /// <param name="f">父窗口</param>
        /// <param name="formName">需要关闭的窗口名称集合</param>
        public static bool CloseForms(Form f, params string[] formNames)
        {
            List<string> li = new List<string>(formNames);
            bool isall = false;
            Form[] AddFrm = f.MdiChildren;
            for (int i = 0; i < AddFrm.Length; i++)
            {
                if (AddFrm[i] != null)
                {
                    bool isclose = false;
                    string formName = AddFrm[i].Name.Trim();
                    if (li.Count > 0)
                    {
                        if (li.IndexOf(formName) > -1)
                        {
                            isclose = true;
                        }
                    }
                    else
                    {
                        isclose = true;
                    }
                    if (isclose)
                    {
                        isclose = false;
                        if (!WinOften.IsFormAction(formName))
                        {
                            isclose = true;
                        }
                        else
                        {
                            if (MessageBox.Show("[" + AddFrm[i].Text + "]操作正在执行中，确认退出？", "系统警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                            {
                                isclose = true;
                            }
                        }
                        if (isclose)
                        {
                            AddFrm[i].Close();
                            AddFrm[i].Dispose();
                        }
                    }
                }
            }
            return isall;
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="lists">列表字符串</param>
        /// <param name="selval">选中值</param>
        public static void SetComboBoxItems(ComboBox cb, string lists, string selval)
        {
            List<string> notVals = new List<string>();
            DataTable rdt = WebOften.StrToDataTable(lists);
            SetComboBoxItems(cb, rdt, 0, 1, 0, selval, notVals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="lists">列表字符串</param>
        /// <param name="valnum">值索引</param>
        /// <param name="txtnum">文本索引</param>
        /// <param name="mode">组合框列模式：0 值加文本模式 1值模式</param>
        /// <param name="selval">选中值</param>
        public static void SetComboBoxItems(ComboBox cb, string lists, int valnum, int txtnum, int mode, string selval)
        {
            List<string> notVals = new List<string>();
            DataTable rdt = WebOften.StrToDataTable(lists);
            SetComboBoxItems(cb, rdt, valnum, txtnum, mode, selval, notVals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="lists">列表字符串</param>
        /// <param name="valnum">值索引</param>
        /// <param name="txtnum">文本索引</param>
        /// <param name="mode">组合框列模式：0 值加文本模式 1值模式</param>
        /// <param name="selval">选中值</param>
        /// <param name="notVals">不出现在列表值的集合</param>
        public static void SetComboBoxItems(ComboBox cb, string lists, int valnum, int txtnum, int mode, string selval, List<string> notVals)
        {
            DataTable rdt = WebOften.StrToDataTable(lists);
            SetComboBoxItems(cb, rdt, valnum, txtnum, mode, selval, notVals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="rdt">列表</param>
        /// <param name="valnum">值索引</param>
        /// <param name="txtnum">文本索引</param>
        /// <param name="mode">组合框列模式：0 值加文本模式 1值模式</param>
        /// <param name="selval">选中值</param>
        public static void SetComboBoxItems(ComboBox cb, DataTable rdt, int valnum, int txtnum, int mode, string selval)
        {
            List<string> notVals = new List<string>();
            SetComboBoxItems(cb, rdt, valnum, txtnum, mode, selval, notVals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="cb">组合框</param>
        /// <param name="rdt">列表</param>
        /// <param name="valnum">值索引</param>
        /// <param name="txtnum">文本索引</param>
        /// <param name="mode">组合框列模式：0 值加文本模式 1值模式</param>
        /// <param name="selval">选中值</param>
        /// <param name="notVals">不出现在列表值的集合</param>
        public static void SetComboBoxItems(ComboBox cb, DataTable rdt, int valnum, int txtnum, int mode, string selval, List<string> notVals)
        {
            cb.Items.Clear();
            int index = 0;
            for (int i = 0; i < rdt.Rows.Count; i++)
            {
                string val = rdt.Rows[i][valnum].ToString().Trim();
                string txt = rdt.Rows[i][txtnum].ToString().Trim();
                if (mode == 0)
                {
                    if (notVals.Count == 0 || notVals.IndexOf(val) > -1)
                    {
                        cb.Items.Add(new ValTxt(val, txt));
                        if (selval.Trim() != "" && selval == val)
                        {
                            cb.SelectedIndex = index;
                        }
                        index++;
                    }
                }
                else
                {
                    if (notVals.Count == 0 || notVals.IndexOf(val) > -1)
                    {
                        cb.Items.Add(val);
                        if (selval.Trim() != "" && selval == val)
                        {
                            cb.SelectedIndex = index;
                        }
                        index++;
                    }
                }
            }
        }


        /// <summary>根据选中值选中组合框列表项</summary>
        /// <param name="cb">组合框</param>
        /// <param name="selval">选中值</param>
        public static void SelectComboBoxItems(ComboBox cb, string selval)
        {
            for (int i = 0; i < cb.Items.Count; i++)
            {
                ValTxt vt = (ValTxt)cb.Items[i];
                string val = vt.Value;
                if (selval== val)
                {
                    cb.SelectedIndex = i;
                }
            }
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="selval">选中值</param>
        /// <param name="cb">组合框</param>
        /// <param name="vals">列表值集合</param>
        public static void SetComboBoxItems(string selval, ComboBox cb, params string[] vals)
        {
            List<string> notVals = new List<string>();
            SetComboBoxItems(selval, cb, notVals, vals);
        }

        /// <summary>设置组合框列表内容</summary>
        /// <param name="selval">选中值</param>
        /// <param name="cb">组合框</param>
        /// <param name="notVals">不出现在列表值的集合</param>
        /// <param name="vals">列表值集合</param>
        public static void SetComboBoxItems(string selval, ComboBox cb, List<string> notVals, params string[] vals)
        {
            cb.Items.Clear();
            int index = 0;
            for (int i = 0; i < vals.Length; i++)
            {
                string val = vals[i].Trim();
                if (notVals.Count == 0 || notVals.IndexOf(val) > -1)
                {
                    cb.Items.Add(val);
                    if (selval.Trim() != "" && selval == val)
                    {
                        cb.SelectedIndex = index;
                    }
                    index++;
                }
            }
        }

        /// <summary>设置时分秒组合框列表内容</summary>
        /// <param name="cbs">时组合框</param>
        /// <param name="cbf">分组合框</param>
        /// <param name="cbm">秒组合框</param>
        /// <param name="tst">秒分秒字符串</param>
        public static void SetTimeComboBox(ComboBox cbs, ComboBox cbf, ComboBox cbm, string tst)
        {
            bool istst = false;
            if (tst != "")
            {
                string[] tarr = tst.Split(':');
                if (tarr.Length == 3)
                {
                    string s = tarr[0].Trim();
                    string f = tarr[1].Trim();
                    string m = tarr[2].Trim();
                    if (Often.IsInt32(s) && Often.IsInt32(f) && Often.IsInt32(m))
                    {
                        wapp.AppPub.SetComboBoxItems(cbs, wapp.AppList.Hour(), s);
                        wapp.AppPub.SetComboBoxItems(cbf, wapp.AppList.Minute(), f);
                        wapp.AppPub.SetComboBoxItems(cbm, wapp.AppList.Minute(), m);
                        istst = true;
                    }
                }
            }
            if (!istst)
            {
                wapp.AppPub.SetComboBoxItems(cbs, wapp.AppList.Hour(), "0");
                wapp.AppPub.SetComboBoxItems(cbf, wapp.AppList.Minute(), "0");
                wapp.AppPub.SetComboBoxItems(cbm, wapp.AppList.Minute(), "0");
            }
        }

        /// <summary>返回时分秒组合框时间字符串</summary>
        /// <param name="cbs">时组合框</param>
        /// <param name="cbf">分组合框</param>
        /// <param name="cbm">秒组合框</param>
        /// <returns>返回时分秒组合框时间字符串</returns>
        public static string GetTimeComboBox(ComboBox cbs, ComboBox cbf, ComboBox cbm)
        {
            string s = ((ValTxt)cbs.SelectedItem).Value;
            string f = ((ValTxt)cbf.SelectedItem).Value;
            string m = ((ValTxt)cbm.SelectedItem).Value;
            return s + ":" + f + ":" + m;
        }

        /// <summary>根据时间返回日期</summary>
        /// <param name="dTime">指定日期</param>
        /// <param name="hour">小时数</param>
        /// <returns>根据时间返回日期</returns>
        public static DateTime GetRandomTime(DateTime dTime, int hour)
        {
            Random random = new Random(Often.Seed);
            int minute = random.Next(20, 59);
            int second = random.Next(0, 59);
            return Convert.ToDateTime(DateOften.ReDateTime("{$Year}-{$Month}-{$Day} " + hour.ToString() + ":" + minute.ToString() + ":" + second.ToString(), dTime));
        }

        /// <summary>设置单选列表选中</summary>
        /// <param name="index">选中索引</param>
        /// <param name="rblist">单选列表</param>
        public static void SetRadioListChecked(string indexs, List<RadioButton> rblist)
        {
            int index = 0;
            if (Often.IsInt32(indexs))
            {
                index = Convert.ToInt32(indexs);
            }
            SetRadioListChecked(index, rblist);
        }

        /// <summary>设置单选列表选中</summary>
        /// <param name="index">选中索引</param>
        /// <param name="rblist">单选列表</param>
        public static void SetRadioListChecked(int index, List<RadioButton> rblist)
        {
            if (rblist.Count > index)
            {
                rblist[index].Checked = true;
            }
        }

        /// <summary>返回单选列表选中索引</summary>
        /// <param name="rblist">单选列表</param>
        /// <returns>返回单选列表选中索引</returns>
        public static int GetRadioListCheckedIndex(List<RadioButton> rblist)
        {
            for (int i = 0; i < rblist.Count; i++)
            {
                if (rblist[i].Checked)
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>设置复选框列表选中</summary>
        /// <param name="index">选中索引</param>
        /// <param name="cblist">复选框列表</param>
        public static void SetCheckBoxListChecked(string indexs, List<CheckBox> cblist)
        {
            int index = 0;
            if (Often.IsInt32(indexs))
            {
                index = Convert.ToInt32(indexs);
            }
            SetCheckBoxListChecked(index, cblist);
        }

        /// <summary>设置复选框列表选中</summary>
        /// <param name="index">选中索引</param>
        /// <param name="cblist">复选框列表</param>
        public static void SetCheckBoxListChecked(int index, List<CheckBox> cblist)
        {
            if (cblist.Count > index)
            {
                cblist[index].Checked = true;
            }
        }

        /// <summary>返回复选框列表选中索引</summary>
        /// <param name="cblist">复选框列表</param>
        /// <returns>返回复选框列表选中索引</returns>
        public static int GetCheckBoxListCheckedIndex(List<CheckBox> cblist)
        {
            for (int i = 0; i < cblist.Count; i++)
            {
                if (cblist[i].Checked)
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>如果输入的字符串为空则返回默认字符串值</summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="defstr">默认字符串值</param>
        /// <returns>如果输入的字符串为空则返回默认字符串值</returns>
        public static string GetStr(string str, string defstr)
        {
            if (str.Trim() == "")
            {
                return defstr;
            }
            return str;
        }

        /// <summary>如果输入的字符串不是长整数则返回默认字符串值</summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="defstr">默认字符串值</param>
        /// <returns>如果输入的字符串不是长整数则返回默认字符串值</returns>
        public static string GetInt64(string str, string defstr)
        {
            if (!Often.IsInt64(str))
            {
                return defstr;
            }
            return str;
        }

        /// <summary>如果输入的字符串不是整数则返回默认字符串值</summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="defstr">默认字符串值</param>
        /// <returns>如果输入的字符串不是整数则返回默认字符串值</returns>
        public static string GetInt32(string str, string defstr)
        {
            if (!Often.IsInt32(str))
            {
                return defstr;
            }
            return str;
        }

        /// <summary>如果输入的字符串不是数字则返回默认字符串值</summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="defstr">默认字符串值</param>
        /// <returns>如果输入的字符串不是数字则返回默认字符串值</returns>
        public static string GetNum(string str, string defstr)
        {
            if (!Often.IsNum(str))
            {
                return defstr;
            }
            return str;
        }

        /// <summary>在字符串右侧增加文本，如果文本值不为空，添加指定分隔字符串</summary>
        /// <param name="sb">字符串</param>
        /// <param name="s">增加的文本</param>
        /// <param name="compart">分隔字符串</param>
        public static void StrAdd(ref StringBuilder sb, string s, string compart)
        {
            if (sb.Length > 0)
            {
                sb.Append(compart);
            }
            sb.Append(s);
        }

        /// <summary>保留指定位小数，并舍去保留位之后的所有小数</summary>
        /// <param name="num">数字</param>
        /// <param name="digit">保留的小数位数</param>
        /// <returns>保留指定位小数，并舍去保留位之后的所有小数</returns>
        public static double GetHoldDigit(double num, int digit)
        {
            num = Math.Round(num, 4);
            if (num > 0)
            {
                if (digit >= 0)
                {
                    int xindex = num.ToString().IndexOf(".");
                    if (xindex > -1)
                    {
                        if (digit == 0)
                        {
                            return Convert.ToDouble(num.ToString().Split('.')[0]);
                        }
                        else
                        {
                            string[] sarr = num.ToString().Split('.');
                            string s1 = sarr[0].Trim();
                            string s2 = sarr[1].Trim();
                            if (s2.Length > digit)
                            {
                                return Convert.ToDouble(s1 + "." + s2.Remove(digit));
                            }
                        }
                    }
                }
                return num;
            }
            return 0;
        }

        /// <summary>返回数字的保留2位小数格式</summary>
        /// <param name="dnum">数字</param>
        /// <returns>返回数字的保留2位小数格式</returns>
        public static double GetNumber(double dnum)
        {
            return GetHoldDigit(dnum, 2);
        }

        /// <summary>返回数字的保留2位小数格式</summary>
        /// <param name="dnum">数字</param>
        /// <returns>返回数字的保留2位小数格式</returns>
        public static double GetNumber(string dnum)
        {
            if (!Often.IsNum(dnum))
            {
                dnum = "0";
            }
            return GetHoldDigit(Convert.ToDouble(dnum), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataTable dt, string cname)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataTable dt, string cname, int row)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataRow[] dr, string cname)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataRow[] dr, string cname, int row)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataTable dt, string cname, string defval)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataTable dt, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataRow[] dr, string cname, string defval)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetNumber(DataRow[] dr, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return GetHoldDigit(Convert.ToDouble(val), 2);
        }

        /// <summary>返回数字的保留2位小数格式</summary>
        /// <param name="dnum">数字</param>
        /// <returns>返回数字的保留2位小数格式</returns>
        public static double GetDouble(string dnum)
        {
            if (!Often.IsNum(dnum))
            {
                dnum = "0";
            }
            return Convert.ToDouble(dnum);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataTable dt, string cname)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataTable dt, string cname, int row)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataRow[] dr, string cname)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataRow[] dr, string cname, int row)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsNum(val))
            {
                val = "0";
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数字的保留2位小数格式</summary>
        /// <param name="dnum">数字</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数字的保留2位小数格式</returns>
        public static double GetDouble(string dnum, string defval)
        {
            if (!Often.IsNum(dnum))
            {
                dnum = defval;
            }
            return Convert.ToDouble(dnum);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataTable dt, string cname, string defval)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataTable dt, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataRow[] dr, string cname, string defval)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static double GetDouble(DataRow[] dr, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsNum(val))
            {
                val = defval;
            }
            return Convert.ToDouble(val);
        }

        /// <summary>返回数字的整数格式，如果不是数字则返回0</summary>
        /// <param name="dnum">数字</param>
        /// <returns>返回数字的整数格式，如果不是数字则返回0</returns>
        public static int GetInt(string dnum)
        {
            if (Often.IsInt32(dnum))
            {
                return Convert.ToInt32(dnum);
            }
            return 0;
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataTable dt, string cname)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsInt32(val))
            {
                val = "0";
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataTable dt, string cname, int row)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsInt32(val))
            {
                val = "0";
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataRow[] dr, string cname)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsInt32(val))
            {
                val = "0";
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dr">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataRow[] dr, string cname, int row)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsInt32(val))
            {
                val = "0";
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数字的整数格式</summary>
        /// <param name="dnum">数字</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数字的整数格式</returns>
        public static int GetInt(string dnum, string defval)
        {
            if (!Often.IsInt32(dnum))
            {
                dnum = defval;
            }
            return Convert.ToInt32(dnum);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataTable dt, string cname, string defval)
        {
            string val = DataOften.GetStr(dt, cname);
            if (!Often.IsInt32(val))
            {
                val = defval;
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据表</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataTable dt, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dt, cname, row);
            if (!Often.IsInt32(val))
            {
                val = defval;
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataRow[] dr, string cname, string defval)
        {
            string val = DataOften.GetStr(dr, cname);
            if (!Often.IsInt32(val))
            {
                val = defval;
            }
            return Convert.ToInt32(val);
        }

        /// <summary>返回数据表字段的数字格式</summary>
        /// <param name="dt">数据行集合</param>
        /// <param name="cname">列名</param>
        /// <param name="row">行索引</param>
        /// <param name="defval">默认值</param>
        /// <returns>返回数据表字段的数字格式</returns>
        public static int GetInt(DataRow[] dr, string cname, int row, string defval)
        {
            string val = DataOften.GetStr(dr, cname, row);
            if (!Often.IsInt32(val))
            {
                val = defval;
            }
            return Convert.ToInt32(val);
        }

        /// <summary>根据字符串集合返回list数组</summary>
        /// <param name="s">以逗号分隔的字符串集合</param>
        /// <param name="yss">指定元素数量</param>
        /// <returns>根据字符串集合返回list数组</returns>
        public static List<int> GetIntList(string s, int yss)
        {
            List<int> li = new List<int>();
            string[] arr = s.Split(',');
            for (int i = 0; i < yss; i++)
            {
                if (i < arr.Length)
                {
                    li.Add(wapp.AppPub.GetInt(arr[i]));
                }
                else
                {
                    li.Add(0);
                }
            }
            return li;
        }

    }
}
