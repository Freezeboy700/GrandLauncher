using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLauncher
{
    public partial class MainForm : ComponentFactory.Krypton.Toolkit.KryptonForm
    {
        Support_Forms.MainSupport mainSupport;
        Support_Forms.MODERN_WARSupport modernWarSupport;
        Support_Forms.SettingsSupport settingsSupport;

        public MainForm()
        {
            InitializeComponent();

            mainSupport = new Support_Forms.MainSupport(this);
            modernWarSupport = new Support_Forms.MODERN_WARSupport(this);
            settingsSupport = new Support_Forms.SettingsSupport(this);

            mainSupport.TopLevel = false;
            Controls.Add(mainSupport);
            mainSupport.Show();

            modernWarSupport.TopLevel = false;
            Controls.Add(modernWarSupport);
            modernWarSupport.Hide();

            settingsSupport.TopLevel = false;
            Controls.Add(settingsSupport);
            settingsSupport.Hide();
        }

        public void ShowModernWar()
        {
            modernWarSupport.Show();
            mainSupport.Hide();
            settingsSupport.Hide();
        }

        public void ShowMain()
        {
            mainSupport.Show();
            settingsSupport.Hide();
            modernWarSupport.Hide();
        }

        public void ShowSettings()
        {
            settingsSupport.Show();
            mainSupport.Hide();
            modernWarSupport.Hide();
        }
    }
}
