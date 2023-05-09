using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OpenCorepiAndBypass.src
{
    class FileUtils
    {
        /// <summary>
        /// 修改文件名称
        /// </summary>
        /// <param name="oldName">旧名称路径</param>
        /// <param name="newName">新名称路径</param>
        /// <param name="fileName">文件名称</param>
        public static void ChangeFileName(string oldName, string newName, string fileName)
        {
            File.Delete(newName); // 删除已存在的文件

            string backupDir = @Environment.CurrentDirectory + @"\bak\";

            string backupFile = backupDir + fileName;
            try
            {
                //判断如果当前文件不存在则返回
                if (!File.Exists(oldName))
                {


                    //判断如何oldName+.bak存在 则改为非.bak格式
                    if (File.Exists(oldName + ".bak"))
                    {
                        File.Move(oldName + ".bak", oldName);
                    }
                    else if (File.Exists(backupFile))//判断如果/bak/目录存在则替换到该目录
                    {
                        File.Move(backupFile, oldName);
                    }
                    else
                    {
                        //否则抛出异常

                        throw new FileNotFoundException("要修改的文件不存在,文件为:" + oldName
                            + " 请检查游戏路径是否填错,或者游戏文件缺失 | The file to be modified does not exist. The file is:" +
                            oldName + " Please check whether the game path is filled in incorrectly or the game file is missing");
                    }

                }
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("发生异常：" + ex.Message);
                Console.Read();
                Environment.Exit(1);
            }

            //将要修改的名称备份到运行目录下的/bak/文件名称


            if (!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }


            File.Copy(oldName, backupFile, true);

            // 将旧文件名改为新文件名
            File.Move(oldName, newName);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="fileName">文件路径名称</param>
        public static void OpenFile(string fileName)
        {
            Process process = new Process();
            // 设置要启动的文件名，包括完整路径
            process.StartInfo.FileName = fileName;

            try
            {
                //判断要打开的文件存不存在
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException("要打开的文件不存在,请检查路径,当前错误路径为:" + fileName
                        + " | The file you want to open does not exist, please check the path, the current error path is:" + fileName);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("发生异常：| An exception occurs:" + ex.Message);
                /*Environment.Exit(1);*/
                //手动选择路径 
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("");
                Console.WriteLine("是否手动选择路径：1)是 2)否 |  Whether to manually select a path: 1)yes 2)no");
                string input = Console.ReadLine();
                int choice;
                if (!int.TryParse(input, out choice) || (choice != 1 && choice != 2))
                {
                    Console.WriteLine("无效选择 | Invalid choice。");
                    Environment.Exit(1);
                }

                // 根据用户的选择执行相应的操作
                if (choice == 1)
                {
                    process.StartInfo.FileName = GetFilePath("选择要打开的文件 | Select the file you want to open", "");

                }
                else
                {
                    Console.WriteLine("不选择有可能导致游戏无法运行,等待后续修复 | Not selecting can result in the game not working, waiting for a later fix");
                    Console.ReadLine();
                    Environment.Exit(1);
                };

            }


            // 启动Process对象
            process.Start();
        }

        /// <summary>
        /// 换取文件路径
        /// </summary>
        /// <param name="title">打开窗口的标题</param>
        /// <param name="filter">过滤器文本</param>
        /// <returns></returns>
        public static string GetFilePath(string title, string filter)
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


        /// <summary>
        /// 换取文件夹路径
        /// </summary>
        /// <param name="title">打开窗口的标题</param>
        /// <returns></returns>
        public static string GetFolderPath(string title)
        {
            string folderPath = "";
            Thread t = new Thread((ThreadStart)(() =>
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.SelectedPath = Environment.CurrentDirectory;
                folderBrowserDialog.Description = title;
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath = folderBrowserDialog.SelectedPath;
                }
            }
            ));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            return folderPath + @"\";
        }
    }

}
