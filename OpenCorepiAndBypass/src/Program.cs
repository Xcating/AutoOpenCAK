using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OpenCorepiAndBypass
{



    class Program
    {
        //改变文件名称
        static void ChangeFileName(string oldName, string newName)
        {
            File.Delete(newName); // 删除已存在的文件
            File.Move(oldName, newName); // 将旧文件名改为新文件名
        }

        //打开文件
        static void OpenFile(string fileName)
        {
            Process process = new Process();
            // 设置要启动的文件名，包括完整路径
            process.StartInfo.FileName = fileName;
            // 启动Process对象
            process.Start();
        }

        //获取文件路径
        static string GetFilePath(string title, string filter)
        {
            string filePath = "";
            Thread t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = filter;
                openFileDialog.Title = title;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                }
            }
            ));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            return filePath;
        }



        static void Main(string[] args)
        {

            //TODO
            //判断是否存在配置文件 初始化配置待完成


            src.IniFile ini = new src.IniFile(@Environment.CurrentDirectory+@"\config.ini");
            if (ini != null)
            {
                System.Console.WriteLine("配置文件加载成功");
            }

            string genshinAccount = ini.ReadValue("Settings", "GenshinAccount");
            if (genshinAccount.Contains("true"))
            {
                System.Console.WriteLine("已确定打开账户切换程序:请切换账号,过程将等待你五秒");
                string genshinAccountPath = ini.ReadValue("Settings", "GenshinAccountPath");

                System.Console.WriteLine("正在读取打开对应程序,确保路径准确");
                Program.OpenFile(@genshinAccountPath);
                
                // 暂停5秒
                Thread.Sleep(5000);
            }
            else
            {
                System.Console.WriteLine("已取消打开账号切换器");
            }
            

            string ThreeDM = ini.ReadValue("Settings", "ThreeDM");
            if (ThreeDM.Contains("true"))
            {
                System.Console.WriteLine("已确定打开3dm模型切换工具");
                string ThreeDMPath = ini.ReadValue("Settings", "ThreeDMPath");

                System.Console.WriteLine("正在读取打开对应程序,确保路径准确");
                Program.OpenFile(ThreeDMPath);
                System.Console.WriteLine("打开3dm");

                // 暂停1秒
                Thread.Sleep(1000);
            }
            else
            {
                System.Console.WriteLine("已取消打开3dm");
            }

            string ByPass = ini.ReadValue("Settings", "ByPass");
            if (ByPass.Contains("true"))
            {
                System.Console.WriteLine("已确定绕过原神反作弊");
                string GamePath = ini.ReadValue("Settings", "GamePath");

                System.Console.WriteLine("绕过准备完成,如读取注入器失败导致无法启动游戏,请使用修复文件修复反作弊");
                

                Program.ChangeFileName(@GamePath + "HoYoKProtect.sys", @GamePath + "HoYoKProtect.sys.bak");
                Program.ChangeFileName(@GamePath + "mhypbase.dll", @GamePath + "mhypbase.dll.bak");
                Program.ChangeFileName(@GamePath + "mhyprot3.Sys", @GamePath + "mhyprot3.Sys.bak");



                // 暂停1秒
                Thread.Sleep(1000);
            }

            string Injector = ini.ReadValue("Settings", "Injector");
            if (Injector.Contains("true"))
            {

                System.Console.WriteLine("已确定打开注入器");
                string InjectorPath = ini.ReadValue("Settings", "InjectorPath");
                Program.OpenFile(InjectorPath);

                // 暂停15秒
                System.Console.WriteLine("暂停15秒等待注入");
                Thread.Sleep(20000);
            }

            
            //取消bypass
            if (ByPass.Contains("true"))
            {
                string GamePath = ini.ReadValue("Settings", "GamePath");

                System.Console.WriteLine("取消bypass");

                Program.ChangeFileName(@GamePath + "HoYoKProtect.sys.bak", @GamePath + "HoYoKProtect.sys");
                Program.ChangeFileName(@GamePath + "mhypbase.dll.bak", @GamePath + "mhypbase.dll");
                Program.ChangeFileName(@GamePath + "mhyprot3.Sys.bak", @GamePath + "mhyprot3.Sys");


                // 暂停1秒
                Thread.Sleep(1000);
            }


            string CB = ini.ReadValue("Settings", "CB");
            if (CB.Contains("true"))
            {
                string CBPath = ini.ReadValue("Settings", "CBPath");
                Program.OpenFile(CBPath);
                System.Console.WriteLine("打开CB");

            }


            System.Console.WriteLine("程序完成,回车退出");

            System.Console.Read();
            System.Console.Read();
        }
    }
}
