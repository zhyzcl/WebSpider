using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Runtime.InteropServices;
using App;
using App.Win;
using App.Web;

namespace WebSpider
{
    public partial class FrmManage : Form
    {
        /// <summary>弹出窗体数组</summary>
        private Dictionary<string, Form> Frm = new Dictionary<string, Form>();
        /// <summary>是否允许退出系统</summary>
        private bool IsExitSystem = true;
        public static string BasePath = Application.StartupPath;
        public System.Timers.Timer aTimer;

        public DataTable wsdt = new DataTable();

        public FrmManage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = wapp.AppList.SystemShowName + "==管理";
        }

        private void PayManage_Load(object sender, EventArgs e)
        {
            WriteRegEdit();
            tssluserinfo.Text = " 当前登录用户：" + wapp.SysConfig.atUser.UserName + "，  登录日期：" + wapp.SysConfig.atUser.LoginDate.ToString() + "。  ";
            LoadWebSpiderList();
        }

        private void timea_Tick(object sender, EventArgs e)
        {
            tssltotime.Text = DateTime.Now.ToString();
        }

        /// <summary>是否允许退出</summary>
        /// <returns>是否允许退出</returns>
        private bool IsExit()
        {
            if (WinOften.IsFormAction())
            {
                if (MessageBox.Show("当前有操作正在执行中，确认退出？", "系统警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        #region 系统方法

        private void PayManage_Activated(object sender, EventArgs e)
        {

        }

        /// <summary>注销</summary>
        private void LoginOut()
        {
            if (IsExit())
            {
                FormLogin login = new FormLogin();
                login.Show();
                IsExitSystem = false;
                this.Close();
            }
        }

        /// <summary>退出系统</summary>
        private void ExitSystem()
        {
            if (IsExit())
            {
                this.Dispose();
                Application.Exit();
            }
        }

        /// <summary>关闭所有窗口</summary>
        private void CloseAllForm()
        {
            wapp.AppPub.CloseForms(this);
        }

        /// <summary>窗体关闭事件</summary>
        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            if (IsExitSystem)
            {
                Application.Exit();
            }
            else
            {
                IsExitSystem = true;
            }
        }

        /// <summary>窗体关闭之前事件</summary>
        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetting();
            if (!IsExit())
            {
                e.Cancel = true;
            }
        }

        /// <summary>关闭保存设置</summary>
        private void SaveSetting()
        {
        }

        /// <summary>注销</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuLogin_Click(object sender, EventArgs e)
        {
            LoginOut();
        }

        /// <summary>退出系统</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEixt_Click(object sender, EventArgs e)
        {
            ExitSystem();
        }

        private bool ShowAstrict()
        {
            WinOften.MessShow("该功能正在建设中，暂时无法使用！");
            return false;
        }

        #endregion

        #region 自定义方法

        public bool IsAdmin()
        {
            if (wapp.SysConfig.atUser.ManageLv < 100)
            {
                WinOften.MessShow("操作权限不足！", 1);
                return false;
            }
            return true;
        }

        private void TlSMIlog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Explorer.exe", wapp.AppList.LogSavePath);
        }

        #endregion

        private void WriteRegEdit()
        {
            RegEdit re = new RegEdit(5, true);
            re.SetKey(wapp.AppList.InstallPath, "InstallPath", BasePath);
        }

        private string GetSystemDirPath(byte rk, string runPath, string keyname)
        {
            RegEdit re = new RegEdit(rk, true);
            return re.ReadKey(runPath, keyname);
        }

        private void timera_Tick(object sender, EventArgs e)
        {

        }

        private void MenuUserManage_Click(object sender, EventArgs e)
        {
            using (FrmUserManage form = new FrmUserManage())
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
            }
        }

        private void LoadWebSpiderList()
        {
            wsdt = wapp.WebSpiderList.WebSpiderTable;
            wslist.Items.Clear();
            for (int i = 0; i < wsdt.Rows.Count; i++)
            {
                ListViewItem itema = new ListViewItem(wsdt.Rows[i]["OrderNum"].ToString().Trim(), 0);
                itema.SubItems.Add(wsdt.Rows[i]["SpiderName"].ToString().Trim());
                itema.SubItems.Add(wsdt.Rows[i]["UrlList"].ToString().Trim());
                itema.SubItems.Add(wsdt.Rows[i]["ExcelSavePath"].ToString().Trim());
                itema.SubItems.Add(WebOften.GetListVal(wapp.AppList.IsYesNo(), wsdt.Rows[i]["IsSaveHtml"].ToString().Trim()));
                itema.SubItems.Add(wsdt.Rows[i]["HtmlSavePath"].ToString().Trim());
                itema.SubItems.Add(DateOften.ReFDateTime("{$Year}-{$Month}-{$Day} {$Hour}:{$Minute}", wsdt.Rows[i]["GatherDate"].ToString().Trim()));
                itema.SubItems.Add(wsdt.Rows[i]["GatherCount"].ToString().Trim());
                itema.SubItems.Add(wsdt.Rows[i]["SpiderID"].ToString().Trim());
                wslist.Items.AddRange(new ListViewItem[] { itema });
            }
        }

        private void wslist_DoubleClick(object sender, EventArgs e)
        {
            if (wslist.SelectedItems.Count > 0)
            {
                int idindex = wslist.SelectedItems[0].SubItems.Count - 1;
                string ids = wslist.SelectedItems[0].SubItems[idindex].Text.Trim();
                if (ids != "")
                {
                    using (FrmWebSpiderInfo form = new FrmWebSpiderInfo(ids, "1"))
                    {
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ShowDialog(this);
                        if (form.IsOper)
                        {
                            LoadWebSpiderList();
                        }
                    }
                }
            }
        }

        private void btadd_Click(object sender, EventArgs e)
        {
            using (FrmWebSpiderInfo form = new FrmWebSpiderInfo("", "0"))
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if (form.IsOper)
                {
                    LoadWebSpiderList();
                }
            }
        }

        private void btdel_Click(object sender, EventArgs e)
        {
            if (wslist.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("确认删除？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                int idindex = wslist.SelectedItems[0].SubItems.Count - 1;
                string ids = wslist.SelectedItems[0].SubItems[idindex].Text.Trim();
                if (ids != "")
                {
                    DataRow[] ldrs = wapp.WebSpiderList.WebSpiderTable.Select("SpiderID='" + ids + "'");
                    if (ldrs.Length > 0)
                    {
                        bool isdels = false;
                        DataRow[] lidrs = wapp.WebSpiderInfoList.WebSpiderInfoTable.Select("SpiderID='" + ids + "'");
                        for (int i = 0; i < lidrs.Length; i++)
                        {
                            lidrs[i].Delete();
                            isdels = true;
                        }
                        if (isdels)
                        {
                            wapp.WebSpiderInfoList.SaveWebSpiderInfoTable(null);
                        }
                        ldrs[0].Delete();
                        wapp.WebSpiderList.SaveWebSpiderTable(null);
                        LoadWebSpiderList();
                    }
                }
            }
        }

        private void btsort_Click(object sender, EventArgs e)
        {
            wapp.WebSpiderList.SetTableOrderNum();
            LoadWebSpiderList();
        }
    }
}
