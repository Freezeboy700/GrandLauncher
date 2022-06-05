using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLandUninstall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить программу \"GrandLand Launcher for Minecraft\"?", "Uninstall", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "GrandLand Launcher.lnk")))
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "GrandLand Launcher.lnk"));
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "GrandLand Launcher.lnk")))
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "GrandLand Launcher.lnk"));
            MessageBox.Show("Программа \"GrandLand Launcher for Minecraft\" удалена", "Uninstall", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\GrandLand");
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland"))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $"/c timeout /t 5 /nobreak && rd /s /q \"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland"}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            }
            Application.Exit();
        }
    }
}
