using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Web.Security;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using App;
using App.Win;
using App.Web;

namespace WebSpider
{
    public partial class FrmUserManage : Form
    {
        public DataTable userdt = new DataTable();

        public FrmUserManage()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FrmLeave_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            LoadUserList();
        }

        private void LoadUserList()
        {
            userdt = wapp.UserList.UsersTable;
            userlist.Items.Clear();
            for (int i = 0; i < userdt.Rows.Count; i++)
            {
                ListViewItem itema = new ListViewItem(userdt.Rows[i]["UserName"].ToString().Trim(), 0);
                itema.SubItems.Add(userdt.Rows[i]["RealName"].ToString().Trim());
                itema.SubItems.Add(WebOften.GetListVal(wapp.AppList.UserManageLv(), userdt.Rows[i]["ManageLv"].ToString().Trim()));
                itema.SubItems.Add(DateOften.ReFDateTime("{$Year}-{$Month}-{$Day} {$Hour}:{$Minute}", userdt.Rows[i]["LoginDate"].ToString().Trim()));
                itema.SubItems.Add(userdt.Rows[i]["LoginCount"].ToString().Trim());
                itema.SubItems.Add(userdt.Rows[i]["Phones"].ToString().Trim());
                userlist.Items.AddRange(new ListViewItem[] { itema });
            }
        }

        private void userlist_DoubleClick(object sender, EventArgs e)
        {
            if (userlist.SelectedItems.Count > 0)
            {
                string uname = userlist.SelectedItems[0].SubItems[0].Text.Trim();
                if (uname!="")
                {
                    using (FrmUserInfo form = new FrmUserInfo(uname, "1"))
                    {
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ShowDialog(this);
                        if (form.IsOper)
                        {
                            LoadUserList();
                        }
                    }
                }
            }
        }

        private void btadd_Click(object sender, EventArgs e)
        {
            using (FrmUserInfo form = new FrmUserInfo("", "0"))
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if(form.IsOper)
                {
                    LoadUserList();
                }
            }
        }

        private void btdel_Click(object sender, EventArgs e)
        {
            if (userlist.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("确认删除用户？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                string uname = userlist.SelectedItems[0].SubItems[0].Text.Trim();
                if (uname!="")
                {
                    DataRow[] udrs = wapp.UserList.UsersTable.Select("UserName='" + uname + "'");
                    if (udrs.Length > 0)
                    {
                        if (udrs[0]["UserName"].ToString().Trim() == wapp.SysConfig.atUser.UserName.Trim())
                        {
                            WinOften.MessShow("不能删除当前用户！", 1);
                            return;
                        }
                        udrs[0].Delete();
                        wapp.UserList.SaveUsersTable(null);
                        LoadUserList();
                    }
                }
            }
        }
    }
}
