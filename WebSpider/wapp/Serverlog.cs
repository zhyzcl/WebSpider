using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    /// <summary>日志记录</summary>
    public class Serverlog : IServerlog
    {
        /// <summary>服务名称</summary>
        private string _ServerName = "";

        /// <summary>服务名称</summary>
        public string ServerName
        {
            get
            {
                return _ServerName;
            }

            set
            {
                _ServerName = value;
            }
        }

        /// <summary>服务器日志记录模式 0 完整 1 精简</summary>
        private int _LogSaveMode = 0;

        /// <summary>服务器日志记录模式 0 完整 1 精简</summary>
        public int LogSaveMode
        {
            get
            {
                return _LogSaveMode;
            }

            set
            {
                _LogSaveMode = value;
            }
        }

        /// <summary>操作序号</summary>
        private long _czxh = 1;

        /// <summary>操作序号</summary>
        public long czxh
        {
            get
            {
                return _czxh;
            }

            set
            {
                _czxh = value;
            }
        }

        /// <summary>操作时间</summary>
        private DateTime _ODate = DateTime.Now;

        /// <summary>操作时间</summary>
        public DateTime ODate
        {
            get
            {
                return _ODate;
            }

            set
            {
                _ODate = value;
            }
        }

        /// <summary>保存并返回日志信息</summary>
        /// <param name="m">权重， 0：必须记录的日志，1：只有日志记录模式为完整时才会记录的日志</param>
        /// <param name="s">信息</param>
        /// <returns>保存并返回日志信息</returns>
        public string GetLogInfo(int m, string s)
        {
            if (m ==0 || LogSaveMode==0)
            {
                string info = "[" + ServerName + "][" + czxh.ToString() + "] >>> " + s + "[" + DateOften.ReFDateTime("{$Year}-{$Month}-{$Day} {$Hour}:{$Minute}:{$Second}", DateTime.Now) + "]\r\n\r\n";
                SaveServerLog(info);
                OutOperDate();
                return info;
            }
            return "";
        }

        /// <summary>设置当前日期并获取与上次操作时间间隔</summary>
        /// <returns>设置当前日期并获取与上次操作时间间隔</returns>
        public void OutOperDate()
        {
            czxh += 1;
            DateTime rq = DateTime.Now;
            TimeSpan df = rq - ODate;
            ODate = rq;
        }


        /// <summary>写操作日志文件</summary>
        /// <param name="logs">日志信息</param>
        public void SaveServerLog(string logs)
        {
            string lpath = wapp.AppList.LogSavePath + GetLogFileName();
            FileStream fs = null;
            byte[] bContent = Encoding.GetEncoding("utf-8").GetBytes(logs);
            try
            {
                if (File.Exists(lpath))
                {
                    fs = File.OpenWrite(lpath);
                }
                else
                {
                    fs = File.Create(lpath);
                }
                fs.Position = fs.Length;
                fs.Write(bContent, 0, bContent.Length);
            }
            catch
            {
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>返回日志文件名</summary>
        /// <returns>返回日志文件名</returns>
        public string GetLogFileName()
        {
            string fname = "";
            DateTime dqrq = DateTime.Now;
            string lsp = wapp.AppList.LogSavePath;
            string lname = DateOften.ReFDateTime("{$Year}{$Month}{$Day}{$Hour}", dqrq);
            int n = 1;
            bool isoper = true;
            while (isoper)
            {
                isoper = false;
                string nums = Often.LStrDup(n.ToString(), "0", 7);
                fname = ServerName + "_" + lname + nums + ".txt";
                string fpath = wapp.AppList.LogSavePath + fname;
                if (File.Exists(fpath))
                {
                    FileInfo info = new FileInfo(fpath);
                    if (info.Length >= 2097152)
                    {
                        isoper = true;
                    }
                }
                n++;
            }
            return fname;
        }
    }
}
