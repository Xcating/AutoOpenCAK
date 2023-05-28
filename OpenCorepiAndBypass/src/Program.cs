using OpenCorepiAndBypass.src;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace OpenCorepiAndBypass
{



    class Program
    {
        const string VERSION = "v0.0.6";

        /// <summary>
        /// 绘制版本启动
        /// </summary>
        private static void DrawVersion()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                        AutoOpenCAK                       ║");
            Console.WriteLine("║                                                          ║");
            Console.Write("║                 ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(VERSION);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("原神");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(",");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("启动");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("!!!!");
            Console.WriteLine("                   ║");
            Console.WriteLine("║                      绕过米哈游检测                      ║");
            Console.WriteLine("║                请确定你已经配置好config.ini              ║");
            Console.WriteLine("║                       路径只支持英文                     ║");
            Console.WriteLine("║                                                          ║");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╚══════════════════════════════════════════════════════════╝");
            Console.ResetColor();

        }

        /// <summary>
        /// 获取资源管理器
        /// </summary>
        /// <returns>资源管理器</returns>
        private static ResourceManager GetResourceManager()
        {

            bool loop = true;

            // 使用while循环来重复执行以下代码，直到loop为false
            while (loop)
            {
                // 输出语言选择菜单
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("请选择语言：| Please select a language:");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. 中文 | 1. Chinese");
                Console.WriteLine("2. 英文 | 2. English");
                Console.ResetColor();

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
            //绘制版本消息
            DrawVersion();

            //获取资源管理器
            ResourceManager rm = GetResourceManager();

            //判断是否存在配置文件
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            //读取配置文件
            IniFile ini = new IniFile(@Environment.CurrentDirectory + @"\config.ini");
            if (ini != null)
            {
                Console.WriteLine(rm.GetString("Conf_Success"));
            }
            //判断是否使用快速启动模式
            bool faststart = false;
            if (args.Contains("--faststart"))
            {
                faststart = true;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("开始快速启动模式 | FastStart Mode");
            }
            
            //获取服务器区别
            string server = ini.ReadValue("Settings", "server");

            //设置游戏data路径
            string GameDataPathName = "";
            string channelId = "";
            //获取游戏路径
            string GamePath = ini.ReadValue("Settings", "GamePath") + @"\";

            if (server.Contains("ys") || server.Contains("bilibili"))
            {
                GameDataPathName = @"\YuanShen_Data\";
                //添加b服sdk,从bak获取
                if (server.Contains("ys"))
                {
                    channelId = "1";
                    //如果存在sdk,则删除
                    File.Delete(@GamePath + GameDataPathName + @"\Plugins\" + "PCGameSDK.dll");

                }
                else
                {
                    channelId = "14";
                    //如果不存在,则添加
                    string backupDir = @Environment.CurrentDirectory + @"\bak\";

                    //从bak下复制一份
                    File.Copy(backupDir + "PCGameSDK.dll", @GamePath + GameDataPathName + @"\Plugins\" + "PCGameSDK.dll",true);
                }
            }
            else if (server.Contains("gs"))
            {
                GameDataPathName = @"\GenshinImpact_Data\";
            }

            //处理b服官服互转
            //读取官方文件config.ini
            IniFile GSini = new IniFile(@GamePath + @"\config.ini");
            GSini.WriteValue("General", "channel", channelId);


            Console.WriteLine(rm.GetString("Server_Info") + "  " + server);



            //账号切换器
            string genshinAccount = ini.ReadValue("Settings", "GenshinAccount");
            if (genshinAccount.Contains("true"))
            {
                Console.WriteLine(rm.GetString("Change_message"));
                string genshinAccountPath = ini.ReadValue("Settings", "GenshinAccountPath");

                Console.WriteLine(rm.GetString("Load_Path"));

                FileUtils.OpenFile(@genshinAccountPath);

                Thread.Sleep(5000);
            }
            else
            {
                Console.WriteLine(rm.GetString("Cancle_Open") + rm.GetString("AccountSwitcher"));
            }

            Console.WriteLine("");

            //3dm
            string ThreeDM = ini.ReadValue("Settings", "ThreeDM");
            if (ThreeDM.Contains("true"))
            {
                Console.WriteLine(rm.GetString("Open_Agree") + "3dm");
                string ThreeDMPath = ini.ReadValue("Settings", "ThreeDMPath");

                Console.WriteLine(rm.GetString("Load_Path"));
                FileUtils.OpenFile(ThreeDMPath);

                // 暂停1秒
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine(rm.GetString("Cancle_Open") + "3dm");
            }

            Console.WriteLine("");


            //执行过检测
            string ByPass = ini.ReadValue("Settings", "ByPass");
            if (ByPass.Contains("true"))
            {
                Console.WriteLine(rm.GetString("Open_Agree") + rm.GetString("Bypass_Name"));



                Console.WriteLine(GamePath);


                //判断文件夹是否存在,不存在报错并且重新选择
                if (!Directory.Exists(GamePath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(rm.GetString("Game_Path_Error"));
                    // 重新选择路径的代码
                    GamePath = FileUtils.GetFolderPath(rm.GetString("Game_Path_Select_Info"));
                    if (GamePath.Length <= 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(rm.GetString("Game_Path_Length_Error"));
                        Console.Read();
                        Environment.Exit(1);
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine(rm.GetString("Bypass_Message"));
                Console.WriteLine("");
                Console.WriteLine(rm.GetString("HoYoKProtect_no_exist"));

                //绕过保护进程
                FileUtils.ChangeFileName(@GamePath + "HoYoKProtect.sys", @GamePath + "HoYoKProtect.sys.bak", "HoYoKProtect.sys");
                FileUtils.ChangeFileName(@GamePath + "mhypbase.dll", @GamePath + "mhypbase.dll.bak", "mhypbase.dll");
                FileUtils.ChangeFileName(@GamePath + "mhyprot3.Sys", @GamePath + "mhyprot3.Sys.bak", "mhyprot3.Sys");


                //处理上传错误程序
                FileUtils.ChangeFileName(@GamePath + GameDataPathName + "blueReporter.exe",
                    @GamePath + GameDataPathName + "blueReporter.exe.bak",
                    "blueReporter.exe");

                FileUtils.ChangeFileName(@GamePath + GameDataPathName + "upload_crash.exe",
                    @GamePath + GameDataPathName + "upload_crash.exe.bak",
                    "upload_crash.exe");

                FileUtils.ChangeFileName(@GamePath + GameDataPathName + @"\Plugins\" + "crashreport.exe",
                    @GamePath + GameDataPathName + @"\Plugins\" + "crashreport.exe.bak",
                    "crashreport.exe");


                //断开网络

                //判断是否断开
                string Internet = ini.ReadValue("Settings", "Internet");
                if (Internet.Contains("true"))
                {
                    InternetUtil.OffInternet();
                }

                // 暂停1秒
                Thread.Sleep(2000);

            }
            Console.WriteLine("");
            //执行注入

            string Injector = ini.ReadValue("Settings", "Injector");
            if (Injector.Contains("true"))
            {
                Console.WriteLine(rm.GetString("Open_Agree") + rm.GetString("Injector"));
                string InjectorPath = ini.ReadValue("Settings", "InjectorPath");
                FileUtils.OpenFile(InjectorPath);

                // 暂停15秒
                Console.WriteLine(rm.GetString("Wait_Injector_Time"));
                Thread.Sleep(20000);
            }
            else
            {
                //未选择执行注入器程序
                //直接运行游戏
                Console.WriteLine("未选择启动注入器,直接运行原神程序");

                GamePath = ini.ReadValue("Settings", "GamePath") + @"\";

                //根据服务器来选择 如果为 ys 为国服 gs为外服
                if (server.Contains("ys")|| server.Contains("bilibili"))
                {
                    FileUtils.OpenFile(GamePath + @"YuanShen.exe");
                }
                else if (server.Contains("gs"))
                {
                    FileUtils.OpenFile(GamePath + @"GenshinImpact.exe");
                }
                Thread.Sleep(20000);

            }


            //恢复文件操作
            if (ByPass.Contains("true"))
            {
                GamePath = ini.ReadValue("Settings", "GamePath") + @"\";

                Console.WriteLine(rm.GetString("Cahcoe_Bypass_Message"));

                FileUtils.ChangeFileName(@GamePath + "HoYoKProtect.sys.bak", @GamePath + "HoYoKProtect.sys", "HoYoKProtect.sys.bak");
                FileUtils.ChangeFileName(@GamePath + "mhypbase.dll.bak", @GamePath + "mhypbase.dll", "mhypbase.dll.bak");
                FileUtils.ChangeFileName(@GamePath + "mhyprot3.Sys.bak", @GamePath + "mhyprot3.Sys", "mhyprot3.Sys.bak");



                //处理上传错误程序
                FileUtils.ChangeFileName(@GamePath + GameDataPathName + "blueReporter.exe.bak",
                    @GamePath + GameDataPathName + "blueReporter.exe",
                    "blueReporter.exe.bak");

                FileUtils.ChangeFileName(@GamePath + GameDataPathName + "upload_crash.exe.bak",
                    @GamePath + GameDataPathName + "upload_crash.exe",
                    "upload_crash.exe.bak");

                FileUtils.ChangeFileName(@GamePath + GameDataPathName + @"\Plugins\" + "crashreport.exe.bak",
                    @GamePath + GameDataPathName + @"\Plugins\" + "crashreport.exe",
                    "crashreport.exe.bak");



                string Internet = ini.ReadValue("Settings", "Internet");
                if (Internet.Contains("true"))
                {
                    //开启网络
                    InternetUtil.OpenInternet();
                }

                // 暂停1秒
                Thread.Sleep(2000);
                Console.WriteLine("");
            }

            //CB运行
            string CB = ini.ReadValue("Settings", "CB");
            if (CB.Contains("true"))
            {
                string CBPath = ini.ReadValue("Settings", "CBPath");
                FileUtils.OpenFile(CBPath);
                Console.WriteLine(rm.GetString("Open_Agree") + "CB");

            }

            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(rm.GetString("Exit_Message"));

            Console.Read();
            Console.Read();
        }


    }
}
