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
    /// <summary>用户配置类</summary>
    public class UserConfig
    {
        /// <summary>设置或获取用户名</summary>
        private string _UserName = "";

        /// <summary>设置或获取用户名</summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>设置或获取用户密码</summary>
        private string _UserPwd = "";

        /// <summary>设置或获取用户密码</summary>
        public string UserPwd
        {
            get { return _UserPwd; }
            set { _UserPwd = value; }
        }

        /// <summary>设置或获取是否是默认登录用户</summary>
        private byte _IsUser = 0;

        /// <summary>设置或获取是否是默认登录用户</summary>
        public byte IsUser
        {
            get { return _IsUser; }
            set { _IsUser = value; }
        }

        /// <summary>设置或获取用户是否保存密码</summary>
        private byte _IsSave = 0;

        /// <summary>设置或获取用户是否保存密码</summary>
        public byte IsSave
        {
            get { return _IsSave; }
            set { _IsSave = value; }
        }

        /// <summary>设置或获取是否开机自动运行</summary>
        private byte _AutoStat = 0;

        /// <summary>设置或获取是否开机自动运行</summary>
        public byte AutoStat
        {
            get { return _AutoStat; }
            set { _AutoStat = value; }
        }

        /// <summary>设置或获取是否自动登录</summary>
        private byte _AutoLogin = 0;

        /// <summary>设置或获取是否自动登录</summary>
        public byte AutoLogin
        {
            get { return _AutoLogin; }
            set { _AutoLogin = value; }
        }
    }
}
