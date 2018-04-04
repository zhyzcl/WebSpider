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
    public class SysConfig
    {
        /// <summary>系统内部版本</summary>
        public static string SystemVers = "1.0.0.0";

        /// <summary>系统版本号</summary>
        public static string SystemVersion = "v" + SystemVers;

        /// <summary>系统版本数字</summary>
        public static long SystemVerNum = Convert.ToInt64(SystemVers.Replace(".", ""));

        /// <summary>系统名称</summary>
        public static string SystemName = "WebSpider";

        /// <summary>系统创建日期</summary>
        public static DateTime SystemCreateDate = Convert.ToDateTime("2017-04-15");

        /// <summary>系统发布日期</summary>
        public static DateTime SystemPublishDate = Convert.ToDateTime("2016-04-17");

        /// <summary>当前登录用户</summary>
        public static SysUser atUser = new SysUser();

        /// <summary>系统提示查看次数</summary>
        public static int SysCueSeeCount = 0;

        /// <summary>用户登录缓存表</summary>
        private static DataTable _UserLoginTable;

        /// <summary>用户登录缓存表</summary>
        public static DataTable UserLoginTable
        {
            get 
            {
                if (_UserLoginTable==null) 
                {
                    CreateSysConfig();
                    LoadSysConfig();
                }
                return _UserLoginTable; 
            }
            set { _UserLoginTable = value; }
        }

        /// <summary>用户配置缓存表</summary>
        private static DataTable _UserConifgTable;

        /// <summary>用户配置缓存表</summary>
        public static DataTable UserConifgTable
        {
            get 
            {
                if (_UserConifgTable == null)
                {
                    CreateSysConfig();
                    LoadSysConfig();
                }
                return _UserConifgTable;
            }
            set { _UserConifgTable = value; }
        }

        /// <summary>系统版本信息表</summary>
        private static DataTable _SystemInfo;

        /// <summary>系统版本信息表</summary>
        public static DataTable SystemInfo
        {
            get
            {
                if (_SystemInfo == null)
                {
                    CreateSysConfig();
                    LoadSysConfig();
                }
                return _SystemInfo;
            }
        }

        /// <summary>初始化系统配置</summary>
        public static void NewSysConfig() 
        {
            wapp.AppList.AppBasePath = Application.StartupPath;
            wapp.AppList.AppBasePath = wapp.AppList.AppBasePath.Replace("/", "\\");
            if (!wapp.AppList.AppBasePath.EndsWith("\\"))
            {
                wapp.AppList.AppBasePath = wapp.AppList.AppBasePath + "\\";
            }
            CreateSysConfig();
            LoadSysConfig();
        }

        /// <summary>创建系统配置</summary>
        public static void CreateSysConfig()
        {
            wapp.AppList.SaveConfigPath = wapp.AppList.AppBasePath  + wapp.AppList.AppFilesDirName + "\\";
            FileSys.NewDir(wapp.AppList.SaveConfigPath);
            wapp.AppList.LogSavePath = wapp.AppList.AppBasePath  + wapp.AppList.SaveLogDirName + "\\";
            FileSys.NewDir(wapp.AppList.LogSavePath);
            _UserLoginTable = wapp.AppList.GetConfigDataTable(wapp.AppList.UserLoginTableName);
            _UserConifgTable = wapp.AppList.GetConfigDataTable(wapp.AppList.UserConifgTableName);
            _SystemInfo = wapp.AppList.GetConfigDataTable(wapp.AppList.SystemInfoName);
            string spath = wapp.AppList.SaveConfigPath + _SystemInfo.TableName + ".xml";
            if (File.Exists(spath))
            {
                File.Delete(spath);
            }
            DataRow newRow = _SystemInfo.NewRow();
            newRow["SystemName"] = SystemName;
            newRow["SystemTitle"] = wapp.AppList.SystemShowName;
            newRow["SystemVers"] = SystemVers;
            newRow["SystemVersion"] = SystemVersion;
            newRow["SystemVerNum"] = SystemVerNum;
            newRow["PublishDate"] = SystemPublishDate;
            newRow["SystemInfo"] = wapp.AppList.SystemShowName;
            newRow["CreateDate"] = SystemCreateDate;
            _SystemInfo.Rows.Add(newRow);
            _SystemInfo.WriteXml(spath, XmlWriteMode.WriteSchema);
        }

        /// <summary>读取系统配置信息</summary>
        public static void LoadSysConfig()
        {
            string ulpath = wapp.AppList.SaveConfigPath + _UserLoginTable.TableName + ".xml";
            if (File.Exists(ulpath))
            {
                try
                {
                    _UserLoginTable = new DataTable();
                    _UserLoginTable.ReadXml(ulpath);
                }
                catch
                {
                    _UserLoginTable.Clear();
                }
            }
            string ucpath = wapp.AppList.SaveConfigPath + _UserConifgTable.TableName + ".xml";
            if (File.Exists(ucpath))
            {
                try
                {
                    _UserConifgTable = new DataTable();
                    _UserConifgTable.ReadXml(ucpath);
                }
                catch
                {
                    _UserConifgTable.Clear();
                }
            }
        }

        /// <summary>保存用户配置信息</summary>
        /// <param name="uc">用户信息</param>
        public static void SaveSysConfig(UserConfig uc)
        {
            string ulpath = wapp.AppList.SaveConfigPath + UserLoginTable.TableName + ".xml";
            string ucpath = wapp.AppList.SaveConfigPath + UserConifgTable.TableName + ".xml";
            if (File.Exists(ulpath))
            {
                File.Delete(ulpath);
            }
            if (File.Exists(ucpath))
            {
                File.Delete(ucpath);
            }
            for (int i = 0; i < UserLoginTable.Rows.Count;i++ )
            {
                UserLoginTable.Rows[i]["IsUser"] = 0;
            }
            if(uc.UserName!="")
            {
                string pwd = TripleDes.DesEn(uc.UserPwd.Trim(), wapp.AppList.DeKey).Trim();
                DataRow[] dr = UserLoginTable.Select("UserName='" + uc.UserName + "'");
                if (dr.Length > 0)
                {
                    dr[0]["UserPwd"] = pwd;
                    dr[0]["IsUser"] = 1;
                }
                else 
                {
                    DataRow newRow = UserLoginTable.NewRow();
                    newRow["UserName"] = uc.UserName;
                    newRow["UserPwd"] = pwd;
                    newRow["IsUser"] = 1;        
                    UserLoginTable.Rows.Add(newRow);
                }
                DataRow[] drc = UserConifgTable.Select("UserName='" + uc.UserName + "'");
                if (drc.Length > 0)
                {
                    drc[0]["IsSave"] = uc.IsSave;
                    drc[0]["AutoStat"] = uc.AutoStat;
                    drc[0]["AutoLogin"] = uc.AutoLogin;
                }
                else
                {
                    DataRow newRow = UserConifgTable.NewRow();
                    newRow["UserName"] = uc.UserName;
                    newRow["IsSave"] = uc.IsSave;
                    newRow["AutoStat"] = uc.AutoStat;
                    newRow["AutoLogin"] = uc.AutoLogin;
                    UserConifgTable.Rows.Add(newRow);
                }
            }
            UserLoginTable.WriteXml(ulpath, XmlWriteMode.WriteSchema);
            UserConifgTable.WriteXml(ucpath, XmlWriteMode.WriteSchema);
        }

        /// <summary>返回默认用户配置信息</summary>
        /// <returns>返回默认用户配置信息</returns>
        public static UserConfig GetUserConfig()
        {
            return GetSelectUserConfig("IsUser=1");
        }

        /// <summary>根据用户名和用户登录模式返回默认用户配置信息</summary>
        /// <param name="name">用户名</param>
        /// <returns>根据用户名和用户登录模式返回默认用户配置信息</returns>
        public static UserConfig GetUserConfig(string name) 
        {
            return GetSelectUserConfig("UserName='" + name + "'");
        }

        /// <summary>根据筛选值返回默认用户配置信息</summary>
        /// <param name="sel">筛选值</param>
        /// <returns>根据筛选值返回默认用户配置信息</returns>
        public static UserConfig GetSelectUserConfig(string sel)
        {
            UserConfig uc = new UserConfig();
            if (UserLoginTable.Rows.Count > 0)
            {
                DataRow[] dr = UserLoginTable.Select(sel);
                if (dr.Length > 0)
                {
                    uc.UserName = dr[0]["UserName"].ToString().Trim();
                    uc.UserPwd = TripleDes.DesDe(dr[0]["UserPwd"].ToString().Trim(), wapp.AppList.DeKey).Trim();
                    if (dr[0]["IsUser"].ToString().Trim() == "1")
                    {
                        uc.IsUser = 1;
                    }
                    if (UserConifgTable.Rows.Count > 0)
                    {
                        DataRow[] drc = UserConifgTable.Select("UserName='" + uc.UserName + "'");
                        if (drc.Length > 0)
                        {
                            if (drc[0]["IsSave"].ToString().Trim() == "1")
                            {
                                uc.IsSave = 1;
                            }
                            if (drc[0]["AutoLogin"].ToString().Trim() == "1")
                            {
                                uc.AutoLogin = 1;
                            }
                            if (drc[0]["AutoStat"].ToString().Trim() == "1")
                            {
                                uc.AutoStat = 1;
                            }
                        }
                    }
                }
            }
            return uc;
        }

        /// <summary>设置开机自动运行</summary>
        /// <param name="name">注册键名称</param>
        /// <param name="fileName">开机自动运行程序的路径</param>
        /// <param name="isAutoRun">如果开机自动运行则为true 否则为false</param>
        public static void SetAutoRun(string name, string fileName, bool isAutoRun)
        {
            RegistryKey reg = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    throw new Exception("该文件不存在!");
                }
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                {
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                }
                if (isAutoRun)
                {
                    reg.SetValue(name, fileName);
                }
                else
                {
                    reg.SetValue(name, false);
                }
            }
            catch
            { }
            finally
            {
                if (reg != null)
                {
                    reg.Close();
                }
            }
        }

        /// <summary>设置开机自动运行</summary>
        /// <param name="uc">用户配置</param>
        public static void SetAutoRun(UserConfig uc)
        {
            string spath = Application.ExecutablePath;
            if (uc.AutoStat == 1)
            {
                SysConfig.SetAutoRun(SystemName, spath, true);
            }
            else
            {
                SysConfig.SetAutoRun(SystemName, spath, false);
            }
        }
    }
}
