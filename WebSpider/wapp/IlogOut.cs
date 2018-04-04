using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    /// <summary>日志输出接口</summary>
    public interface IlogOut
    {
        /// <summary>监视器当前行数</summary>
        long rtxdqhs
        {
            get;
            set;
        }

        /// <summary>监视器最大行数</summary>
        long rtxhs
        {
            get;
            set;
        }

        /// <summary>保留监视信息</summary>
        StringBuilder savehs
        {
            get;
            set;
        }

        /// <summary>监视器文本显示框</summary>
        System.Windows.Forms.RichTextBox richText
        {
            get;
            set;
        }

        /// <summary>日志记录接口</summary>
        wapp.IServerlog slog
        {
            get;
            set;
        }

        /// <summary>输出信息</summary>
        /// <param name="m">权重， 0：必须记录的日志，1：只有日志记录模式为完整时才会记录的日志</param>
        /// <param name="s">信息</param>
        /// <returns>输出信息</returns>
        void OT(int m, string s);
    }
}
