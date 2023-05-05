using System.Diagnostics;
using System.IO;
using System.Threading;

namespace OpenCorepiAndBypass
{



    class Program
    {

        static void changeFileName(string oldName,string newName)
        {
            File.Delete(newName); // 删除已存在的文件
            File.Move(oldName, newName); // 将旧文件名改为新文件名
        }

        static void openFile(string fileName)
        {
            Process process = new Process();
            // 设置要启动的文件名，包括完整路径
            process.StartInfo.FileName = fileName;
            // 启动Process对象
            process.Start();
        }
        static void Main(string[] args)
        {

            Program.openFile(@"C:\Users\Administrator\Desktop\genshinhack\GenshinAccount.exe");

            System.Console.WriteLine("打开账户切换文件");


            // 暂停5秒
            Thread.Sleep(5000);


            Program.openFile(@"C:\Users\Administrator\Desktop\genshinhack\3dmigoto\play\3DMigoto Loader.exe");

            System.Console.WriteLine("打开3dm");
            // 暂停1秒
            Thread.Sleep(1000);


            //bypass
            System.Console.WriteLine("bypass");
            Program.changeFileName(@"G:\Genshin Impact\Genshin Impact Game\HoYoKProtect.sys", @"G:\Genshin Impact\Genshin Impact Game\HoYoKProtect.sys.bak");
            Program.changeFileName(@"G:\Genshin Impact\Genshin Impact Game\mhypbase.dll", @"G:\Genshin Impact\Genshin Impact Game\mhypbase.dll.bak");
            Program.changeFileName(@"G:\Genshin Impact\Genshin Impact Game\mhyprot3.Sys", @"G:\Genshin Impact\Genshin Impact Game\mhyprot3.Sys.bak");
            // 暂停1秒
            Thread.Sleep(1000);
            //打开注入器
            System.Console.WriteLine("打开注入器");
            Program.openFile(@"C:\Users\Administrator\Desktop\genshinhack\injector.exe");

            System.Console.WriteLine("暂停15秒");
            // 暂停20秒
            Thread.Sleep(20000);

            //取消bypass
            System.Console.WriteLine("取消bypass");
            Program.changeFileName(@"G:\Genshin Impact\Genshin Impact Game\HoYoKProtect.sys.bak", @"G:\Genshin Impact\Genshin Impact Game\HoYoKProtect.sys");
            Program.changeFileName(@"G:\Genshin Impact\Genshin Impact Game\mhypbase.dll.bak", @"G:\Genshin Impact\Genshin Impact Game\mhypbase.dll");
            Program.changeFileName(@"G:\Genshin Impact\Genshin Impact Game\mhyprot3.Sys.bak", @"G:\Genshin Impact\Genshin Impact Game\mhyprot3.Sys");

            // 暂停1秒
            Thread.Sleep(1000);

            //打开cb
            System.Console.WriteLine("打开cb");
            Program.openFile(@"C:\Users\Administrator\Desktop\genshinhack\Cotton Buds 3.6 - Stable.CETRAINER");


            System.Console.WriteLine("程序完成,回车退出");
            System.Console.Read();
        }
    }
}
