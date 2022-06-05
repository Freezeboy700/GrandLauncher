using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLauncher.Support_Forms
{
    public partial class MODERN_WARSupport : Form
    {
        bool MineInstalled;

        MainForm mainForm;
        public MODERN_WARSupport(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e) { mainForm.ShowMain(); }

        private void MODERN_WARSupport_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War"))
            {
                MineInstalled = false;
                pictureBox3.BackgroundImage = Properties.Resources.BUTTON2;
            }
            else
            {
                MineInstalled = true;
                pictureBox3.BackgroundImage = Properties.Resources.BUTTON;
            }
        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            if (!MineInstalled)
            {
                pictureBox3.Enabled = false;
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War");
                label3.Visible = true;
                progressBar1.Visible = true;
                pictureBox3.Visible = false;
                await Download_libnat();
                label3.Text = "Downloading assets and versions";
                await Download_assver();
                label3.Text = "Downloading mods";
                label3.Location = new Point(label3.Location.X + 19, label3.Location.Y);
                await Download_modsMW();
                progressBar1.Value = progressBar1.Maximum;
                label3.Text = "Extracting Files";
                await Unpack();
                progressBar1.Value = progressBar1.Maximum;
                label3.Text = "Deleting Archives";
                await DeleteZipFiles();
                label3.Visible = false;
                progressBar1.Visible = false;
                pictureBox3.Visible = true;
                MessageBox.Show("Версия сборки \"Modern War\", установлена. Программа перезапустится", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
            else
            {
                new MineOption().StartMinecraft(Convert.ToInt32(File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\settings.cfg")[0].Replace("Gigabytes: ", "")), File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\username.cfg")[0], Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War");
                
            }
        }

        private async Task Download_libnat()
        {
            byte[] data;
            Download:
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += (s, e) =>
                    {
                        progressBar1.Value = e.ProgressPercentage;
                    };
                    data = await wc.DownloadDataTaskAsync(new Uri("https://grandlandlauncher.000webhostapp.com/libnat.zip"));
                }
            }
            catch
            {
                goto Download;
            }
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\libnat.zip", data);
            progressBar1.Value = 0;
        }

        private async Task Download_assver()
        {
            byte[] data;
            Download:
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += (s, e) =>
                    {
                        progressBar1.Value = e.ProgressPercentage;
                    };
                    data = await wc.DownloadDataTaskAsync(new Uri("https://grandlandlauncher.000webhostapp.com/assver.zip"));
                }
            }
            catch
            {
                goto Download;
            }
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\assver.zip", data);
            progressBar1.Value = 0;
        }

        private async Task Download_modsMW()
        {
            byte[] data;
            Download:
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += (s, e) =>
                    {
                        progressBar1.Value = e.ProgressPercentage;
                    };
                    data = await wc.DownloadDataTaskAsync(new Uri("https://gdlmds.000webhostapp.com/modsMW.zip"));
                }
            }
            catch
            {
                goto Download;
            }
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\modsMW.zip", data);
            progressBar1.Value = 0;
        }

        private async Task Unpack()
        {
            //ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\assver.zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\tools");
            using (ZipFile zip = ZipFile.Read(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\assver.zip"))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War");
                }
            }
            //ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\libnat.zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\tools");
            using (ZipFile zip = ZipFile.Read(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\libnat.zip"))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War");
                }
            }
            //ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\modsMW.zip", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\tools");
            using (ZipFile zip = ZipFile.Read(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\modsMW.zip"))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War");
                }
            }
        }

        private async Task DeleteZipFiles()
        {
            Task.Run(() =>
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\libnat.zip");
            });
            Task.Run(() =>
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\assver.zip");
            });
            Task.Run(() =>
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\minecraft\\Modern_War\\modsMW.zip");
            });
        }
    }
}
