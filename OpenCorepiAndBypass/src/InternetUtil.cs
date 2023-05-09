using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;
namespace OpenCorepiAndBypass.src
{
    class InternetUtil
    {

        public static void OpenInternet()
        {


            ProcessStartInfo processStartInfo = new ProcessStartInfo("ipconfig", "/renew");
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.UseShellExecute = true;
            Process.Start(processStartInfo);


        }   


        public static void OffInternet()
        {

            ProcessStartInfo processStartInfo = new ProcessStartInfo("ipconfig", "/release");
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.UseShellExecute = true;
            Process.Start(processStartInfo);


        }

    }
}
