using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace OpenCorepiAndBypass.src
{
    class IniFile
    {
        string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public IniFile(string INIPath)
        {
            path = INIPath;
        }
        /// <summary>
        /// 写ini文件值
        /// </summary>
        /// <param name="Section">所属区域</param>
        /// <param name="Key">k</param>
        /// <param name="Value">v</param>
        public void WriteValue(string Section, string Key, string Value)
        {
            try
            {
                WritePrivateProfileString(Section, Key, Value, path);
            }
            catch (Exception ex)
            {
                throw new Exception("无法写入 INI 文件。", ex);
            }
        }
        /// <summary>
        /// 读ini文件值
        /// </summary>
        /// <param name="Section">区域</param>
        /// <param name="Key">k</param>
        /// <returns>v</returns>
        public string ReadValue(string Section, string Key)
        {
            if (!File.Exists(path))
            {
                return "";
            }

            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();
        }
    }
}
