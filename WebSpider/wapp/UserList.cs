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
    /// <summary>用户内存列表</summary>
    public class UserList
    {
        /// <summary>用户列表缓存表</summary>
        private static DataTable _UsersTable;

        /// <summary>用户列表缓存表</summary>
        public static DataTable UsersTable
        {
            get
            {
                if (_UsersTable == null)
                {
                    CreateUsersTable();
                    LoadUsersTable();
                }
                return _UsersTable;
            }
            set { _UsersTable = value; }
        }

        /// <summary>创建用户列表</summary>
        public static void CreateUsersTable()
        {
            _UsersTable = wapp.AppList.GetConfigDataTable(wapp.AppList.UsersTableName);
        }

        /// <summary>读取用户列表</summary>
        public static void LoadUsersTable()
        {
            string upath = wapp.AppList.SaveConfigPath + _UsersTable.TableName + ".xml";
            if (File.Exists(upath))
            {
                try
                {
                    _UsersTable = new DataTable();
                    _UsersTable.ReadXml(upath);
                }
                catch
                {
                    _UsersTable.Clear();
                }
            }
        }

        /// <summary>保存用户列表</summary>
        /// <param name="su">用户对象</param>
        public static void SaveUsersTable(SysUser su)
        {
            string upath = wapp.AppList.SaveConfigPath + _UsersTable.TableName + ".xml";
            if (File.Exists(upath))
            {
                File.Delete(upath);
            }
            if (su!=null)
            {
                string pwd = "";
                if (su.UserPwd.Trim()!="")
                {
                    pwd = TripleDes.DesEn(su.UserPwd.Trim(), wapp.AppList.DeKey).Trim();
                }  
                DataRow[] dr = _UsersTable.Select("UserName='" + su.UserName + "'");
                if (dr.Length > 0)
                {
                    if (pwd!="") 
                    {
                        dr[0]["UserPwd"] = pwd;
                    }
                    dr[0]["RealName"] = su.RealName;
                    dr[0]["ManageLv"] = su.ManageLv;
                    dr[0]["Phones"] = su.Phones;
                    dr[0]["LoginDate"] = su.LoginDate;
                    dr[0]["LoginCount"] = su.LoginCount;
                }
                else
                {
                    DataRow newRow = _UsersTable.NewRow();
                    newRow["UserName"] = su.UserName;
                    newRow["UserPwd"] = pwd;
                    newRow["RealName"] = su.RealName;
                    newRow["ManageLv"] = su.ManageLv;
                    newRow["Phones"] = su.Phones;
                    newRow["LoginDate"] = su.LoginDate;
                    newRow["LoginCount"] = su.LoginCount;
                    _UsersTable.Rows.Add(newRow);
                }
            }
            _UsersTable.WriteXml(upath, XmlWriteMode.WriteSchema);
        }
    }
}
