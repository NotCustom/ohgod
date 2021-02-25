using System;
using System.Net;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Rain
{
    public partial class boogie : Form
    {
        static string name = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Rain", "Name", null);
        public boogie()
        {
            InitializeComponent();
        }
        private void loadboogie(Object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:\\" + name + "\\boogie.png");
            new SoundPlayer("C:\\"+name+"\\shining.wav").PlayLooping();
        }
        private void no(Object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("No.");
            e.Cancel = true;
        }
    }
}
