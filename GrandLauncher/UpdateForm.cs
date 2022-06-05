using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLauncher
{
    public partial class UpdateForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public UpdateForm()
        {
            InitializeComponent();
        }

        private async void UpdateForm_LoadAsync(object sender, EventArgs e)
        {
            WebClient wbclnt = new WebClient();

            if (File.Exists(Path.Combine(Path.GetTempPath(), "LICH.exe")))
            {
                File.Delete(Path.Combine(Path.GetTempPath(), "LICH.exe"));
            }

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\version.cfg"))
            {
                if (DialogResult.No == MessageBox.Show("Невозможно определить версию программы. Отсутствует \"version.cfg\". Переустановить лаунчер?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                {
                    Application.Exit();
                }

                Hide();

                byte[] data = await wbclnt.DownloadDataTaskAsync(new Uri("https://grandlandlauncher.000webhostapp.com/LICH.7z"));
                File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "LICH.exe"), data);

                Process.Start(Path.Combine(Path.GetTempPath(), "LICH.exe"));
                Application.Exit();
            }

            string newVer = wbclnt.DownloadString("https://pastebin.com/raw/NaKEsmZk");

            string[] lines = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\version.cfg");

            if (lines[0] != newVer)
            {
                Hide();

                byte[] data = await wbclnt.DownloadDataTaskAsync(new Uri("https://grandlandlauncher.000webhostapp.com/LICH.7z"));
                File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "LICH.exe"), data);

                MessageBox.Show("Доступно новое обновление, лаунчер перезагрузится и откроется установщик", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Thread.Sleep(100);
                progressBar1.Value = 100;
                Thread.Sleep(100);
                Process.Start(Path.Combine(Path.GetTempPath(), "LICH.exe"));
                Application.Exit();
            }
            else
            {
                Task.Run(() =>
                {
                    while (progressBar1.Value < 100)
                    {
                        Thread.Sleep(10);
                        progressBar1.Value++;
                    }

                    Hide();

                    Task.Run(() =>
                    {
                        Application.Run(new LoginForm());
                        Close();
                    });
                });
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
