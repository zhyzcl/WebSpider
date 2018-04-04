using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using App;
using App.Win;
using App.Web;

namespace wapp
{
    /// <summary>用户对象</summary>
    public class SysUser
    {
        /// <summary>设置获取用户名</summary>
        private string _UserName = "";

        /// <summary>设置获取用户名</summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        /// <summary>设置获取用户登录密码</summary>
        private string _UserPwd = "";

        /// <summary>设置获取用户登录密码</summary>
        public string UserPwd
        {
            get { return _UserPwd; }
            set { _UserPwd = value; }
        }

        /// <summary>设置获取用户真实姓名</summary>
        private string _RealName = "";

        /// <summary>设置获取用户真实姓名</summary>
        public string RealName
        {
            get { return _RealName; }
            set { _RealName = value; }
        }

        /// <summary>设置获取用户权限</summary>
        private int _ManageLv = 0;

        /// <summary>设置获取用户权限</summary>
        public int ManageLv
        {
            get { return _ManageLv; }
            set { _ManageLv = value; }
        }

        /// <summary>设置获取用户登录次数</summary>
        private int _LoginCount = 0;

        /// <summary>设置获取用户登录次数</summary>
        public int LoginCount
        {
            get { return _LoginCount; }
            set { _LoginCount = value; }
        }

        /// <summary>设置获取用户登录时间</summary>
        private DateTime _LoginDate = DateTime.Now;

        /// <summary>设置获取用户登录时间</summary>
        public DateTime LoginDate
        {
            get { return _LoginDate; }
            set { _LoginDate = value; }
        }

        /// <summary>设置获取用户联系电话</summary>
        private string _Phones = "";

        /// <summary>设置获取用户联系电话</summary>
        public string Phones
        {
            get { return _Phones; }
            set { _Phones = value; }
        }
    }
}
