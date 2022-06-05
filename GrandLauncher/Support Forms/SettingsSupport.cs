using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLauncher.Support_Forms
{
    public partial class SettingsSupport : Form
    {
        MainForm mainForm;
        public SettingsSupport(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e) { mainForm.ShowMain(); }

        private void SettingsSupport_Load(object sender, EventArgs e)
        {
            ComputerInfo computerInfo = new ComputerInfo();
            ulong mem = computerInfo.TotalPhysicalMemory / 1024 / 1024;
            if (mem <= 4096)
            {
                // 4gb
                pictureBox5.BackgroundImage = Properties.Resources.LABEL547657;
                metroTrackBar1.Maximum = 3;
            }
            else if (mem <= 8192)
            {
                // 8gb
            }
            else if (mem <= 16384)
            {
                // 16gb
                pictureBox5.BackgroundImage = Properties.Resources.LABEL324656;
                metroTrackBar1.Maximum = 15;
            }
            else if (mem <= 32768)
            {
                // 32gb
                pictureBox5.BackgroundImage = Properties.Resources.LABEL46676432;
                metroTrackBar1.Maximum = 31;
            }
            else
            {
                // 8gb
            }

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\settings.cfg"))
            {
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\settings.cfg").Close();
            }

            string[] settings = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\settings.cfg");

            int gigabyteCount = Convert.ToInt32(settings[0].Replace("Gigabytes: ", ""));

            metroTrackBar1.Value = gigabyteCount - 1;

            label1.Text = $"{settings[0].Replace("Gigabytes: ", "")}GB";
        }

        private void metroTrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label1.Text = $"{metroTrackBar1.Value + 1}GB";
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            string GB = label1.Text.Replace("GB", "");

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\settings.cfg"))
            {
                File.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\settings.cfg").Close();
            }

            string[] settings = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\settings.cfg");

            settings[0] = $"Gigabytes: {GB}";

            File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\settings.cfg", settings);
        }
    }
}
