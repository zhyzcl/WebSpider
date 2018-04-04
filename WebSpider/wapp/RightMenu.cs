using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace wapp
{
    /// <summary>给RichTextBox或TextBox增加右键复制，粘贴，全选，剪切，删除，清空菜单功能</summary>
    public partial class RightMenu : ContextMenuStrip
    {
        private System.Windows.Forms.ToolStripMenuItem MenuItemSelAllCopy;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem MenuItemPaset;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCut;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSelAll;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDel;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDelAll;

        public RightMenu()
        {
            NewRightMenu();
        }

        private void NewRightMenu()
        {
            this.MenuItemSelAllCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemPaset = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDel = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemDelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();
            // CmsMenu
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSelAllCopy,
            this.MenuItemCopy,
            this.MenuItemPaset,
            this.MenuItemCut,
            this.MenuItemSelAll,
            this.MenuItemDel,
            this.MenuItemDelAll});
            this.Name = "menuSend";
            this.Size = new System.Drawing.Size(153, 158);
            this.Opened += new System.EventHandler(this.CmsMenu_Opened);
            this.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.CmsMenu_ItemClicked);

            // MenuItemSelAllCopy
            this.MenuItemSelAllCopy.Name = "MenuItemSelAllCopy";
            this.MenuItemSelAllCopy.Size = new System.Drawing.Size(100, 22);
            this.MenuItemSelAllCopy.Text = "全选并复制";

            // MenuItemCopy
            this.MenuItemCopy.Name = "MenuItemCopy";
            this.MenuItemCopy.Size = new System.Drawing.Size(100, 22);
            this.MenuItemCopy.Text = "复制";

            // MenuItemPaset
            this.MenuItemPaset.Name = "MenuItemPaset";
            this.MenuItemPaset.Size = new System.Drawing.Size(100, 22);
            this.MenuItemPaset.Text = "粘贴";

            // MenuItemCut
            this.MenuItemCut.Name = "MenuItemCut";
            this.MenuItemCut.Size = new System.Drawing.Size(100, 22);
            this.MenuItemCut.Text = "剪切"; 

            // MenuItemSelAll 
            this.MenuItemSelAll.Name = "MenuItemSelAll";
            this.MenuItemSelAll.Size = new System.Drawing.Size(100, 22);
            this.MenuItemSelAll.Text = "全选";

            // MenuItemDel
            this.MenuItemDel.Name = "MenuItemDel";
            this.MenuItemDel.Size = new System.Drawing.Size(100, 22);
            this.MenuItemDel.Text = "删除";

            // MenuItemDelAll
            this.MenuItemDelAll.Name = "MenuItemDelAll";
            this.MenuItemDelAll.Size = new System.Drawing.Size(100, 22);
            this.MenuItemDelAll.Text = "清空";

            // RightMenu
            this.Name = "RightMenu";
            this.Size = new System.Drawing.Size(100, 100);
            this.ResumeLayout(false);

        }

        /// <summary>没有选择文本时，复制菜单禁用，剪切板没有文本内容时，粘贴菜单禁用</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsMenu_Opened(object sender, EventArgs e)
        {

            ContextMenuStrip cms = (ContextMenuStrip)sender;
            ToolStripItemCollection tstc = cms.Items;
            for (int i = 0; i < tstc.Count; i++)
            {
                string cname = tstc[i].Name;
                if (cname == "MenuItemCopy" || cname == "MenuItemCut" || cname == "MenuItemDel")
                {
                    string sText = "";
                    if (cms.SourceControl is RichTextBox)
                    {
                        sText = ((RichTextBox)cms.SourceControl).SelectedText;
                    }
                    else if (cms.SourceControl is TextBox)
                    {
                        sText = ((TextBox)cms.SourceControl).SelectedText;
                    }
                    if (sText != "")
                    {
                        tstc[i].Enabled = true;
                    }
                    else
                    {
                        tstc[i].Enabled = false;
                    }
                }
                else if (cname == "MenuItemPaset")
                {
                    if (Clipboard.ContainsText())
                    {
                        tstc[i].Enabled = true;
                    }
                    else
                    {
                        tstc[i].Enabled = false;
                    }
                }
            }
        }

        /// <summary>给RichTextBox或TextBox增加右键复制，粘贴，全选，剪切，删除，清空菜单功能</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextMenuStrip cms = (ContextMenuStrip)sender;
            if (cms.SourceControl is RichTextBox)
            {
                RichTextBox rtbc = (RichTextBox)cms.SourceControl;
                switch (e.ClickedItem.Name)
                {
                    case "MenuItemSelAllCopy":
                        rtbc.Focus();
                        rtbc.SelectAll();
                        rtbc.Copy();
                        break;
                    case "MenuItemCopy":
                        rtbc.Copy();
                        break;
                    case "MenuItemPaset":
                        rtbc.Paste();
                        break;
                    case "MenuItemCut":
                        rtbc.Cut();
                        break;
                    case "MenuItemDelAll":
                        rtbc.Text = "";
                        break;
                    case "MenuItemDel":
                        rtbc.SelectedText = "";
                        break;
                    case "MenuItemSelAll":
                        rtbc.Focus();
                        rtbc.SelectAll();
                        break;
                }
            }
            else if (cms.SourceControl is TextBox)
            {
                TextBox rtbc = (TextBox)cms.SourceControl;
                switch (e.ClickedItem.Name)
                {
                    case "MenuItemSelAllCopy":
                        rtbc.Focus();
                        rtbc.SelectAll();
                        rtbc.Copy();
                        break;
                    case "MenuItemCopy":
                        rtbc.Copy();
                        break;
                    case "MenuItemPaset":
                        rtbc.Paste();
                        break;
                    case "MenuItemCut":
                        rtbc.Cut();
                        break;
                    case "MenuItemDelAll":
                        rtbc.Text = "";
                        break;
                    case "MenuItemDel":
                        rtbc.SelectedText = "";
                        break;
                    case "MenuItemSelAll":
                        rtbc.Focus();
                        rtbc.SelectAll();
                        break;
                }
            }
        }
    }
}
