using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App;
using App.Win;
using App.Web;


namespace WebSpider
{
    public partial class FrmUserInfo : Form
    {
        /// <summary>是否操作成功</summary>
        public bool IsOper = false;

        /// <summary>编辑时的用户名</summary>
        public string username = "";

        /// <summary>操作模式 0:添加,1:编辑</summary>
        public byte OpMode = 0;

        public DataTable userdt = new DataTable();

        public FrmUserInfo()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public FrmUserInfo(params object[] param)
        {
            InitializeComponent();
            if (param.Length > 1)
            {
                username = param[0].ToString().Trim();
                OpMode = Convert.ToByte(param[1]);
            }
        }

        private void FrmUserInfo_Load(object sender, EventArgs e)
        {
            if (OpMode == 1)
            {
                LoadUserInfo();
            }
        }

        private void LoadUserInfo()
        {
            DataRow[] udrs = wapp.UserList.UsersTable.Select("UserName='" + username + "'");
            if (udrs.Length > 0)
            {
                userdt = DataOften.GetTable(udrs);
            }
            else
            {
                WinOften.MessShow("该用户不存在或已被删除！", 1);
                this.Close();
                return;
            }
            tbfPwd.Enabled = false;
            tbName.Text = DataOften.GetStr(userdt, "UserName");
            tbrealName.Text = DataOften.GetStr(userdt, "RealName");
            tbphone.Text = DataOften.GetStr(userdt, "Phones");
            tbPwd.Text = "      ";
            tbName.Enabled = false;
        }

        public bool IsRunOper()
        {
            string uname = tbName.Text.Trim();
            if (uname == "")
            {
                WinOften.MessShow("用户名不能为空！", 1);
                return false;
            }
            if(!Often.StrIsReg(uname,Often.RegExpStr_Num26LeAndCna))
            {
                WinOften.MessShow("用户名必须由数字、下划线、26个英文大小写字母或者中文组成！", 1);
                return false;
            }
            if (OpMode == 0 || (OpMode == 1 && tbPwd.Text.Trim() != "" ))
            {
                if (tbPwd.Text.Trim() == "")
                {
                    WinOften.MessShow("密码不能为空！", 1);
                    return false;
                }
                if (OpMode==0) 
                {
                    if (tbPwd.Text.Trim() != tbfPwd.Text.Trim())
                    {
                        WinOften.MessShow("密码与确认密码不符！", 1);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btsave_Click(object sender, EventArgs e)
        {
            if (!IsRunOper())
            {
                return;
            }
            string name = tbName.Text.Trim();
            if (OpMode == 0)
            {
                DataRow[] udrs = wapp.UserList.UsersTable.Select("UserName='" + name + "'");
                if (udrs.Length > 0)
                {
                    WinOften.MessShow("用户名已存在！", 1);
                    return;
                }
            }
            int mv = 60;
            wapp.SysUser su = new wapp.SysUser();
            su.UserName = name;
            su.UserPwd = tbPwd.Text.Trim();
            su.ManageLv = mv;
            su.RealName = tbrealName.Text.Trim();
            su.Phones = tbphone.Text.Trim();
            if (OpMode == 1)
            {
                su.LoginDate = Convert.ToDateTime(DataOften.GetStr(userdt, "LoginDate"));
            }
            else 
            {
                su.LoginDate = Convert.ToDateTime("1990-01-01");
            }
            wapp.UserList.SaveUsersTable(su);
            WinOften.MessShow("保存成功！",0);
            IsOper = true;
            this.Close();
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmUserInfo_Shown(object sender, EventArgs e)
        {
            if (wapp.SysConfig.atUser.ManageLv < 60)
            {
                WinOften.MessShow("操作权限不足！", 1);
                this.Close();
            }
        }

    }
}
