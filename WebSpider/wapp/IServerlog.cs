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
    /// <summary>日志记录接口</summary>
    public interface IServerlog
    {
        /// <summary>服务名称</summary>
        string ServerName
        {
            get;
            set;
        }

        /// <summary>服务器日志记录模式 0 完整 1 精简</summary>
        int LogSaveMode
        {
            get;
            set;
        }

        /// <summary>操作序号</summary>
        long czxh
        {
            get;
            set;
        }

        /// <summary>操作时间</summary>
        DateTime ODate
        {
            get;
            set;
        }

        /// <summary>保存并返回日志信息</summary>
        /// <param name="m">权重， 0：必须记录的日志，1：只有日志记录模式为完整时才会记录的日志</param>
        /// <param name="s">信息</param>
        /// <returns>保存并返回日志信息</returns>
        string GetLogInfo(int m, string s);

        /// <summary>设置当前日期并获取与上次操作时间间隔</summary>
        /// <returns>设置当前日期并获取与上次操作时间间隔</returns>
        void OutOperDate();


        /// <summary>写操作日志文件</summary>
        /// <param name="logs">日志信息</param>
        void SaveServerLog(string logs);

        /// <summary>返回日志文件名</summary>
        /// <returns>返回日志文件名</returns>
        string GetLogFileName();
    }
}
