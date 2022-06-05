using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLauncher.Support_Forms
{
    public partial class MainSupport : Form
    {
        MainForm mainForm;
        public MainSupport(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e) { pictureBox1.BackgroundImage = Properties.Resources.MODERN_WAR_EVENT_SHADOW; }

        private void pictureBox1_MouseLeave(object sender, EventArgs e) { pictureBox1.BackgroundImage = Properties.Resources.MODERN_WAR_EVENT; }

        private void pictureBox1_Click(object sender, EventArgs e) { mainForm.ShowModernWar(); }

        private void pictureBox3_Click(object sender, EventArgs e) { mainForm.ShowSettings(); }

        private void MainSupport_Load(object sender, EventArgs e)
        {

        }
    }
}
