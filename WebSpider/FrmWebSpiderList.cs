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
    public partial class FrmWebSpiderList : Form
    {
        public DataTable cldt = new DataTable();

        public string pids = "";

        public FrmWebSpiderList(params object[] param)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (param.Length > 0)
            {
                pids = param[0].ToString().Trim();
            }
        }

        private void FrmLeave_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            LoadPInfo();
            LoadSpiderInfoList();
        }

        private void LoadPInfo()
        {
            DataRow[] idrs = wapp.WebSpiderList.WebSpiderTable.Select("SpiderID='" + pids + "'");
            if (idrs.Length <= 0)
            {
                WinOften.MessShow("该信息不存在或已被删除！", 1);
                this.Close();
                return;
            }
        }

        private void LoadSpiderInfoList()
        {
            DataRow[] drs = wapp.WebSpiderInfoList.WebSpiderInfoTable.Select("SpiderID='" + pids + "'");
            cldt = DataOften.GetTable(drs);
            contentlist.Items.Clear();
            for (int i = 0; i < cldt.Rows.Count; i++)
            {
                ListViewItem itema = new ListViewItem(cldt.Rows[i]["OrderNum"].ToString().Trim(), 0);
                itema.SubItems.Add(cldt.Rows[i]["ListTitle"].ToString().Trim());
                itema.SubItems.Add(cldt.Rows[i]["ListCode"].ToString().Trim());
                itema.SubItems.Add(WebOften.GetListVal(wapp.AppList.ContentType(), cldt.Rows[i]["ContentType"].ToString().Trim()));
                itema.SubItems.Add(cldt.Rows[i]["ListID"].ToString().Trim());
                contentlist.Items.AddRange(new ListViewItem[] { itema });
            }
        }

        private void contentlist_DoubleClick(object sender, EventArgs e)
        {
            if (contentlist.SelectedItems.Count > 0)
            {
                int idindex = contentlist.SelectedItems[0].SubItems.Count - 1;
                string ids = contentlist.SelectedItems[0].SubItems[idindex].Text.Trim();
                if (ids != "")
                {
                    using (FrmWebSpiderListInfo form = new FrmWebSpiderListInfo(ids, pids, "1"))
                    {
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.ShowDialog(this);
                        if (form.IsOper)
                        {
                            LoadSpiderInfoList();
                        }
                    }
                }
            }
        }

        private void btadd_Click(object sender, EventArgs e)
        {
            using (FrmWebSpiderListInfo form = new FrmWebSpiderListInfo("", pids, "0"))
            {
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog(this);
                if(form.IsOper)
                {
                    LoadSpiderInfoList();
                }
            }
        }

        private void btdel_Click(object sender, EventArgs e)
        {
            if (contentlist.SelectedItems.Count > 0)
            {
                DialogResult result = MessageBox.Show("确认删除？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                int idindex = contentlist.SelectedItems[0].SubItems.Count - 1;
                string ids = contentlist.SelectedItems[0].SubItems[idindex].Text.Trim();
                if (ids != "")
                {
                    DataRow[] drs = wapp.WebSpiderInfoList.WebSpiderInfoTable.Select("ListID='" + ids + "'");
                    if (drs.Length > 0)
                    {
                        drs[0].Delete();
                        wapp.WebSpiderInfoList.SaveWebSpiderInfoTable(null);
                        LoadSpiderInfoList();
                    }
                }
            }
        }

        private void btsort_Click(object sender, EventArgs e)
        {
            wapp.WebSpiderInfoList.SetTableOrderNum(pids);
            LoadSpiderInfoList();
        }
    }
}
