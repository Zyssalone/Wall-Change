using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows.Forms;
using Microsoft.Win32;


namespace WallpaperChanger
{
    public partial class Form1 : Form
    {
        private string[] imageFiles;
        private int currentImageIndex = 0;
        private System.Windows.Forms.Timer wallpaperChangeTimer;
        private System.Windows.Forms.Timer secondTimer;
        private string selectedFolderPath;
        private int countdown;
        private ContextMenuStrip context;

        public Form1()
        {
            InitializeComponent();
            InitializeApp();
        }

        private void InitializeApp()
        {
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            startButton.Enabled = false;
            stopButton.Enabled = false;

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);

            context = new ContextMenuStrip();
            context.Items.Add("Exit", null, ExitApp_Click);
            notifyIcon.ContextMenuStrip = context;
            AddToStartup();
        }

        private void AddToStartup()
        {
            try
            {
                string appName = "WallpaperChanger";
                string appPath = Application.ExecutablePath;

                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

                if (key.GetValue(appName) == null)
                {
                    key.SetValue(appName, appPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add to startup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectDirectoryButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolderPath = folderDialog.SelectedPath;
                    imageFiles = Directory.GetFiles(selectedFolderPath, "*.*")
                        .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                       file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                       file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                       file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                                       file.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                        .ToArray();

                    startButton.Enabled = imageFiles.Length > 0;
                }
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (int.TryParse(intervalTextBox.Text, out int interval) && interval > 0 && imageFiles != null && imageFiles.Length > 0)
            {
                wallpaperChangeTimer = new System.Windows.Forms.Timer();
                wallpaperChangeTimer.Interval = interval * 1000;
                wallpaperChangeTimer.Tick += WallpaperChangeTimer_Tick;
                wallpaperChangeTimer.Start();

                secondTimer = new System.Windows.Forms.Timer();
                secondTimer.Interval = 1000;
                secondTimer.Tick += SecondTimer_Tick;
                secondTimer.Start();


                countdown = interval;
                countdownLabel.Text = $"Next change in: {countdown}s";

                startButton.Enabled = false;
                stopButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid time interval and select a folder with images.");
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (wallpaperChangeTimer != null)
            {
                wallpaperChangeTimer.Stop();
                wallpaperChangeTimer.Dispose();
                wallpaperChangeTimer = null;
                secondTimer.Stop();
                secondTimer.Dispose();
                secondTimer = null; 
            }

            stopButton.Enabled = false;
            startButton.Enabled = true;
            countdownLabel.Text = "Next change in: 0s";
        }

        private void WallpaperChangeTimer_Tick(object? sender, EventArgs e)
        {
            if (imageFiles != null && imageFiles.Length > 0)
            {
                currentImageIndex = (currentImageIndex + 1) % imageFiles.Length;
                SetWallpaper(imageFiles[currentImageIndex]);
            }
        }

        private void SecondTimer_Tick(object? sender, EventArgs e)
        {
            if (countdown > 1)
            {
                countdown--;
            } else
            {
                countdown = wallpaperChangeTimer.Interval / 1000;
            }
                countdownLabel.Text = $"Next change in: {countdown}s";
        }

        private void SetWallpaper(string filePath)
        {
            SystemParametersInfo(0x0014, 0, filePath, 0x0001 | 0x0002);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon.Visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            notifyIcon.Visible = true;
        }

        private void ExitApp_Click(object? sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.ExitThread();
        }
    }
}
