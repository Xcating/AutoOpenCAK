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
        /// 改变文件名称
        /// </summary>
        /// <param name="oldName">旧的名称</param>
        /// <param name="newName">新的名称</param>
         public static void ChangeFileName(string oldName, string newName)
        {
            File.Delete(newName); // 删除已存在的文件
            File.Move(oldName, newName); // 将旧文件名改为新文件名
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
    }
}
