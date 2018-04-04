using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSpider
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Boolean createdNew; //返回是否赋予了使用线程的互斥体初始所属权
            System.Threading.Mutex instance = new System.Threading.Mutex(true, wapp.SysConfig.SystemName, out createdNew); //同步基元变量
            //赋予了线程初始所属权，也就是首次使用互斥体
            if (!createdNew)
            {
                return;
            }
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //如果是管理员，则直接运行
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                wapp.SysConfig.NewSysConfig();
                FormLogin fmLogin = new FormLogin();
                Application.Run(fmLogin);
                instance.ReleaseMutex();
            }
            else
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //设置运行文件
                startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                //如果不是管理员，则启动UAC
                System.Diagnostics.Process.Start(startInfo);
                //退出
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
