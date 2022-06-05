using GrandLauncherInstaller.Properties;
using Ionic.Zip;
using Microsoft.Win32;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLauncherInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите начать установку программы \"GrandLand Launcher for Minecraft\"?", "Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                Application.Exit();

            textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox1.Visible = false;
            progressBar1.Visible = true;
            label2.Text = "Проверка";
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland"))
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland", true);
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "GrandLand Launcher.lnk")))
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "GrandLand Launcher.lnk"));
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "GrandLand Launcher.lnk")))
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "GrandLand Launcher.lnk"));
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland");
            progressBar1.Value += 25;
            label2.Text = "Распаковка файлов";
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\Release_05.06.22.zip", Resources.Release_05_06_22);
            await Unpack();
            progressBar1.Value += 25;
            label2.Text = "Скачивание Java Runtime Environment";
            await Download_Java();
            progressBar1.Value += 25;
            label2.Text = "Распаковка Java Runtime Environment";
            await UnpackJava();
            progressBar1.Value += 25;
            label2.Text = "Завершение установки";
            await DeleteZipFiles();
            await CreateShortcuts();
            await CreateUni();
            MessageBox.Show("Программа \"GrandLand Launcher\" успешно установлена", "Setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\GrandLauncher.exe");
            Application.Exit();
        }

        private async Task Unpack()
        {
            using (ZipFile zip = ZipFile.Read(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\Release_05.06.22.zip"))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland");
                }
            }
        }

        private async Task UnpackJava()
        {
            using (ZipFile zip = ZipFile.Read(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\java_x64.zip"))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland");
                }
            }
        }

        private async Task Download_Java()
        {
            byte[] data;
            Download:
            try
            {
                using (WebClient wc = new WebClient())
                {
                    data = await wc.DownloadDataTaskAsync(new Uri("https://grandlandlauncher.000webhostapp.com/java_x64.zip"));
                }
            }
            catch
            {
                goto Download;
            }
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\java_x64.zip", data);
        }

        private async Task DeleteZipFiles()
        {
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\Release_05.06.22.zip");
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\java_x64.zip");
        }

        private async Task CreateUni()
        {
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\GrandLand").SetValue("DisplayName", "GrandLand Launcher");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\GrandLand").SetValue("DisplayIcon", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\GrandLauncher.exe");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\GrandLand").SetValue("DisplayVersion", "0.1");
            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\GrandLand").SetValue("UninstallString", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\unins000.exe");
        }

        private async Task CreateShortcuts()
        {
            ShortCut.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\GrandLauncher.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "GrandLand Launcher.lnk"), "", "GrandLand Launcher for Minecraft");
            ShortCut.Create(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.grandland\\GrandLauncher.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "GrandLand Launcher.lnk"), "", "GrandLand Launcher for Minecraft");
        }
    }
    static class ShellLink
    {
        [ComImport,
        Guid("000214F9-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IShellLinkW
        {
            [PreserveSig]
            int GetPath(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszFile,
                int cch, ref IntPtr pfd, uint fFlags);

            [PreserveSig]
            int GetIDList(out IntPtr ppidl);

            [PreserveSig]
            int SetIDList(IntPtr pidl);

            [PreserveSig]
            int GetDescription(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszName, int cch);

            [PreserveSig]
            int SetDescription(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszName);

            [PreserveSig]
            int GetWorkingDirectory(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszDir, int cch);

            [PreserveSig]
            int SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszDir);

            [PreserveSig]
            int GetArguments(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszArgs, int cch);

            [PreserveSig]
            int SetArguments(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszArgs);

            [PreserveSig]
            int GetHotkey(out ushort pwHotkey);

            [PreserveSig]
            int SetHotkey(ushort wHotkey);

            [PreserveSig]
            int GetShowCmd(out int piShowCmd);

            [PreserveSig]
            int SetShowCmd(int iShowCmd);

            [PreserveSig]
            int GetIconLocation(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszIconPath, int cch, out int piIcon);

            [PreserveSig]
            int SetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszIconPath, int iIcon);

            [PreserveSig]
            int SetRelativePath(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszPathRel, uint dwReserved);

            [PreserveSig]
            int Resolve(IntPtr hwnd, uint fFlags);

            [PreserveSig]
            int SetPath(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszFile);
        }

        [ComImport,
        Guid("00021401-0000-0000-C000-000000000046"),
        ClassInterface(ClassInterfaceType.None)]
        private class shl_link { }

        internal static IShellLinkW CreateShellLink()
        {
            return (IShellLinkW)(new shl_link());
        }
    }

    public static class ShortCut
    {
        public static void Create(
            string PathToFile, string PathToLink,
            string Arguments, string Description)
        {
            ShellLink.IShellLinkW shlLink = ShellLink.CreateShellLink();

            Marshal.ThrowExceptionForHR(shlLink.SetDescription(Description));
            Marshal.ThrowExceptionForHR(shlLink.SetPath(PathToFile));
            Marshal.ThrowExceptionForHR(shlLink.SetArguments(Arguments));

            ((System.Runtime.InteropServices.ComTypes.IPersistFile)shlLink).Save(PathToLink, false);
        }
    }
}
