using OpenCorepiAndBypass.Properties;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
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

        //语言控制器
        private static ResourceManager GetResourceManager()
        {
            bool loop = true;

            // 使用while循环来重复执行以下代码，直到loop为false
            while (loop)
            {
                // 输出语言选择菜单
                Console.WriteLine("请选择语言：| Please select a language:");
                Console.WriteLine("1. 中文 | 1. Chinese");
                Console.WriteLine("2. 英文 | 2. English");

                // 读取用户输入的选择
                string choice = Console.ReadLine();

                // 使用switch语句来根据选择设置语言和退出循环
                switch (choice)
                {
                    case "1":
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                        loop = false; // 设置loop为false，退出循环
                        break;
                    case "2":
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                        loop = false; // 设置loop为false，退出循环
                        break;
                    default:
                        Console.WriteLine("无效选择！| Invalid selection!");
                        break;
                }
            }


            // 创建资源管理器对象
            ResourceManager rm = new System.Resources.ResourceManager("OpenCorepiAndBypass.Properties.Strings", Assembly.GetExecutingAssembly());


            return rm;
        }


        static void Main(string[] args)
        {
            ResourceManager rm = GetResourceManager();



            //TODO
            //判断是否存在配置文件 初始化配置待完成


            src.IniFile ini = new src.IniFile(@Environment.CurrentDirectory + @"\config.ini");
            if (ini != null)
            {
                System.Console.WriteLine(rm.GetString("Conf_Success"));
            }

            string genshinAccount = ini.ReadValue("Settings", "GenshinAccount");
            if (genshinAccount.Contains("true"))
            {
                System.Console.WriteLine(rm.GetString("Change_message"));
                string genshinAccountPath = ini.ReadValue("Settings", "GenshinAccountPath");

                System.Console.WriteLine(rm.GetString("Load_Path"));
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

                System.Console.WriteLine(rm.GetString("Load_Path"));
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
