using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLauncher
{
    public partial class LoginForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public LoginForm()
        {
            InitializeComponent();
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

        private void metroTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                string txt = "";
                char[] txtChar = metroTextBox1.Text.ToCharArray();
                if (metroTextBox1.Text != "" && metroTextBox1.Text != " ")
                {
                    for (int i = 0; i < txtChar.Length; i++)
                    {
                        if ((txtChar[i] >= 'A' && txtChar[i] <= 'Z') || (txtChar[i] >= 'a' && txtChar[i] <= 'z') || (txtChar[i] >= '0' && txtChar[i] <= '9') || txtChar[i] == '_')
                        {

                        }
                        else
                        {
                            txtChar[i] = '_';
                        }
                        txt += txtChar[i];
                    }

                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\username.cfg"))
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\username.cfg");
                }
                else
                {
                    return;
                }


                File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\username.cfg", txt);

                Hide();

                Task.Run(() =>
                {
                    Application.Run(new MainForm());
                    Close();
                });
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\username.cfg"))
                metroTextBox1.Text = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\username.cfg");
        }
    }
}
