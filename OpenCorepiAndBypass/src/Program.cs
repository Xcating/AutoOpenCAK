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

            string genshinAccount = ini.ReadValue("Settings", "GenshinAccount");
            if (genshinAccount.Contains("true"))
            {
                string genshinAccountPath = ini.ReadValue("Settings", "GenshinAccountPath");
                Program.OpenFile(@genshinAccountPath);
                System.Console.WriteLine("打开账户切换文件");

                // 暂停5秒
                Thread.Sleep(5000);
            }
            

            string ThreeDM = ini.ReadValue("Settings", "ThreeDM");
            if (ThreeDM.Contains("true"))
            {
                string ThreeDMPath = ini.ReadValue("Settings", "ThreeDMPath");
                Program.OpenFile(ThreeDMPath);
                System.Console.WriteLine("打开3dm");

                // 暂停1秒
                Thread.Sleep(1000);
            }

            string ByPass = ini.ReadValue("Settings", "ByPass");
            if (ByPass.Contains("true"))
            {
                string GamePath = ini.ReadValue("Settings", "GamePath");

                System.Console.WriteLine("bypass");

                Program.ChangeFileName(@GamePath + "HoYoKProtect.sys", @GamePath + "HoYoKProtect.sys.bak");
                Program.ChangeFileName(@GamePath + "mhypbase.dll", @GamePath + "mhypbase.dll.bak");
                Program.ChangeFileName(@GamePath + "mhyprot3.Sys", @GamePath + "mhyprot3.Sys.bak");


                // 暂停1秒
                Thread.Sleep(1000);
            }

            string Injector = ini.ReadValue("Settings", "Injector");
            if (Injector.Contains("true"))
            {
                string InjectorPath = ini.ReadValue("Settings", "InjectorPath");
                Program.OpenFile(InjectorPath);
                System.Console.WriteLine("打开注入器");

                // 暂停15秒
                System.Console.WriteLine("暂停15秒");
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


            
            //打开cb
            System.Console.WriteLine("打开cb");
            Program.OpenFile(@"C:\Users\Administrator\Desktop\genshinhack\Cotton Buds 3.6 - Stable.CETRAINER");


            string CB = ini.ReadValue("Settings", "CB");
            if (CB.Contains("true"))
            {
                string CBPath = ini.ReadValue("Settings", "CBPath");
                Program.OpenFile(CBPath);
                System.Console.WriteLine("打开CB");

            }



            System.Console.WriteLine("程序完成,回车退出");
            System.Console.Read();
        }
    }
}
