using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Web.Security;
using App;
using App.Win;

namespace WebSpider
{
    public partial class FormLogin : Form
    {
        /// <summary>用户配置</summary>
        wapp.UserConfig ucfg = new wapp.UserConfig();

        public FormLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = wapp.AppList.SystemShowName + "==登录通道";
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {            
            WinOften.AnimateWindow(this.Handle, 500, 16);
            //设置主窗口关闭按钮不可用
            int hMenu = WinOften.GetSystemMenu(this.Handle.ToInt32(), 0);
            WinOften.RemoveMenu(hMenu, 0xF060, 0x1000);
            ucfg = wapp.SysConfig.GetUserConfig();
            LoadUserConfig();
            LoadLists();
        }

        /// <summary>读取用户配置信息</summary>
        private void LoadUserConfig()
        {
            cBsave.Checked = false;
            if (ucfg.UserName != "")
            {
                cBName.Text = ucfg.UserName;
                tPwd.Text = ucfg.UserPwd;
                if (ucfg.IsSave == 1)
                {
                    cBsave.Checked = true;
                }
                wapp.SysConfig.SetAutoRun(ucfg);
            }
        }

        /// <summary>读取登录模式</summary>
        private void LoadLists()
        {
            cBName.Items.Clear();
            List<string> li = new List<string>();
            DataTable namedt = wapp.SysConfig.UserLoginTable;
            for (int i = 0; i < namedt.Rows.Count; i++)
            {
                string val = namedt.Rows[i]["UserName"].ToString().Trim();
                if (li.IndexOf(val) == -1)
                {
                    cBName.Items.Add(val);
                    li.Add(val);
                    if (namedt.Rows[i]["IsUser"].ToString().Trim() == "1")
                    {
                        cBName.SelectedIndex = cBName.Items.Count - 1;
                    }
                }
            }
            if (cBName.Items.Count > 0 && cBName.SelectedIndex < 0)
            {
                cBName.SelectedIndex = 0;
            }
        }

        /// <summary>保存用户配置信息</summary>
        private void SaveUserConfig()
        {
            ucfg.UserName = cBName.Text.Trim();
            ucfg.UserPwd = tPwd.Text.Trim();
            if (cBsave.Checked)
            {
                ucfg.IsSave = 1;
            }
            else
            {
                ucfg.IsSave = 0;
                ucfg.UserPwd = "";
            }
            wapp.SysConfig.SaveSysConfig(ucfg);
            wapp.SysConfig.SetAutoRun(ucfg);
        }

        /// <summary>回车键与Tab键关联</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            string pakey = e.KeyChar.ToString().ToLower();
            if (e.KeyChar == (char)13)
            {
                UserLogin();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserLogin();
        }

        private void UserLogin()
        {
            wapp.SysConfig.atUser = new wapp.SysUser();
            string Name = cBName.Text.Trim();
            if (Name == "")
            {
                WinOften.MessShow("用户名不能为空！", 1);
                return;
            }
            string Pwd = tPwd.Text.Trim();
            if (Pwd == "")
            {
                WinOften.MessShow("密码不能为空！", 1);
                return;
            }
            if (wapp.UserList.UsersTable.Rows.Count <= 0)
            {
                wapp.SysUser su = new wapp.SysUser();
                su.UserName = "admin";
                su.UserPwd = "123456";
                su.RealName = "admin";
                su.Phones = "";
                su.ManageLv = 60;
                su.LoginCount = 1;
                su.LoginDate = DateTime.Now;
                wapp.UserList.SaveUsersTable(su);
            }
            string ePwd = TripleDes.DesEn(Pwd, wapp.AppList.DeKey).Trim();
            DataRow[] udrs = wapp.UserList.UsersTable.Select("UserName='" + Name + "' and UserPwd='" + ePwd + "'");
            if (udrs.Length > 0)
            {
                wapp.SysConfig.atUser.UserName = udrs[0]["UserName"].ToString().Trim();
                wapp.SysConfig.atUser.UserPwd = Pwd;
                wapp.SysConfig.atUser.ManageLv = Convert.ToInt32(udrs[0]["ManageLv"]);
                wapp.SysConfig.atUser.RealName = udrs[0]["RealName"].ToString().Trim();
                wapp.SysConfig.atUser.Phones = udrs[0]["Phones"].ToString().Trim();
                wapp.SysConfig.atUser.LoginCount = Convert.ToInt32(udrs[0]["LoginCount"]) + 1;
                wapp.SysConfig.atUser.LoginDate = DateTime.Now;
                wapp.UserList.SaveUsersTable(wapp.SysConfig.atUser);
                SaveUserConfig();
            }
            if (wapp.SysConfig.atUser.ManageLv == 0)
            {
                WinOften.MessShow("用户名或密码错误！", 1);
                return;
            }
            else
            {
                this.Hide();
                ShowFrm();
            }
        }

        private void ShowFrm()
        {
            FrmManage main = new FrmManage();
            main.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cBName_Leave(object sender, EventArgs e)
        {
            string name = cBName.Text.Trim();
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            if (cBsave.Checked && cBName.Text.Trim() != "" && tPwd.Text.Trim() != "" && ucfg.AutoLogin == 1)
            {
                UserLogin();
            }
        }

        private void tPwd_Leave(object sender, EventArgs e)
        {
            GetUserConfig();
        }

        private void GetUserConfig()
        {
            string name = cBName.Text.Trim();
            if (name != ucfg.UserName && name != "")
            {
                ucfg = wapp.SysConfig.GetUserConfig(name);
                LoadUserConfig();
            }
        }
    }
}
