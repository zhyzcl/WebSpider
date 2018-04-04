using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;
using App;
using App.Win;
using App.Web;
using System.Windows.Forms;

namespace wapp
{
    /// <summary>窗口日志输出对象</summary>
    public class FlogOut : IlogOut
    {

        /// <summary>监视器当前行数</summary>
        private long _rtxdqhs = 0;

        /// <summary>监视器当前行数</summary>
        public long rtxdqhs
        {
            get
            {
                return _rtxdqhs;
            }

            set
            {
                _rtxdqhs = value;
            }
        }

        /// <summary>监视器最大行数</summary>
        private long _rtxhs = 1000000;

        /// <summary>监视器最大行数</summary>
        public long rtxhs
        {
            get
            {
                return _rtxhs;
            }

            set
            {
                _rtxhs = value;
            }
        }

        /// <summary>保留监视信息</summary>
        private StringBuilder _savehs = new StringBuilder();

        /// <summary>保留监视信息</summary>
        public StringBuilder savehs
        {
            get
            {
                return _savehs;
            }

            set
            {
                _savehs = value;
            }
        }

        /// <summary>监视器文本显示框</summary>
        private System.Windows.Forms.RichTextBox _richText = null;

        /// <summary>监视器文本显示框</summary>
        public RichTextBox richText
        {
            get
            {
                return _richText;
            }

            set
            {
                _richText = value;
            }
        }

        /// <summary>日志记录接口</summary>
        private wapp.IServerlog _slog = null;

        /// <summary>日志记录接口</summary>
        public wapp.IServerlog slog
        {
            get
            {
                return _slog;
            }

            set
            {
                _slog = value;
            }
        }

        /// <summary>输出信息</summary>
        /// <param name="m">权重， 0：必须记录的日志，1：只有日志记录模式为完整时才会记录的日志</param>
        /// <param name="s">信息</param>
        /// <returns>输出信息</returns>
        public void OT(int m, string s)
        {
            if (slog!=null)
            {
                string info = slog.GetLogInfo(m, s);
                if (info != "")
                {
                    bool isqc = false;
                    if (rtxdqhs > rtxhs)
                    {
                        isqc = true;
                        rtxdqhs = 0;
                    }
                    object[] myArray = new object[4];
                    myArray[0] = richText;
                    myArray[1] = info;
                    myArray[2] = isqc;
                    myArray[3] = savehs.ToString();
                    try
                    {
                        richText.BeginInvoke(new wapp.RTBDel(wapp.AppPub.RTB), myArray);
                    }
                    catch
                    {
                    }
                    rtxdqhs += 1;
                }
            }
        } 
    }
}
