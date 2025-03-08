using System.Drawing;
using System.Windows.Forms;

namespace WallpaperChanger
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Label titleLabel;
        private Button selectDirectoryButton;
        private TextBox intervalTextBox;
        private Button startButton;
        private Button stopButton;
        private NotifyIcon notifyIcon;
        private Label countdownLabel;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            notifyIcon = new NotifyIcon(components);

            titleLabel = new Label();
            selectDirectoryButton = new Button();
            intervalTextBox = new TextBox();
            startButton = new Button();
            stopButton = new Button();

            SuspendLayout();

            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(50, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(228, 41);
            titleLabel.Text = "Wallpaper Changer";
            titleLabel.Scale(new SizeF(0.001F, 0.001F));
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // selectDirectoryButton
            // 
            selectDirectoryButton.BackColor = Color.FromArgb(108, 117, 128);
            selectDirectoryButton.FlatStyle = FlatStyle.Flat;
            selectDirectoryButton.ForeColor = Color.White;
            selectDirectoryButton.Location = new Point(50, 80);
            selectDirectoryButton.Name = "selectDirectoryButton";
            selectDirectoryButton.Size = new Size(200, 40);
            selectDirectoryButton.Text = "Select Folder";
            selectDirectoryButton.UseVisualStyleBackColor = false;
            selectDirectoryButton.Click += SelectDirectoryButton_Click;

            // 
            // intervalTextBox
            // 
            intervalTextBox.Location = new Point(80, 140);
            intervalTextBox.Name = "intervalTextBox";
            intervalTextBox.Size = new Size(140, 30);
            intervalTextBox.TextAlign = HorizontalAlignment.Center;
            intervalTextBox.PlaceholderText = "Interval (Seconds)";

            // 
            // startButton
            // 
            startButton.BackColor = Color.Green;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.ForeColor = Color.White;
            startButton.Location = new Point(80, 200);
            startButton.Name = "startButton";
            startButton.Size = new Size(140, 40);
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += StartButton_Click;

            // 
            // stopButton
            // 
            stopButton.BackColor = Color.Red;
            stopButton.FlatStyle = FlatStyle.Flat;
            stopButton.ForeColor = Color.White;
            stopButton.Location = new Point(80, 260);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(140, 40);
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = false;
            stopButton.Click += StopButton_Click;
            stopButton.Enabled = false;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 39, 45);
            ClientSize = new Size(300, 350);
            Controls.Add(titleLabel);
            Controls.Add(selectDirectoryButton);
            Controls.Add(intervalTextBox);
            Controls.Add(startButton);
            Controls.Add(stopButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Wallpaper Changer";
            ResumeLayout(false);
            PerformLayout();

            // 
            // countdownLabel
            // 
            // Initialize countdown label
            countdownLabel = new Label
            {
                AutoSize = true,
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(80, 380),
                Name = "countdownLabel",
                Size = new System.Drawing.Size(140, 20),
                Text = "Next change in: 0s"
            };
            Controls.Add(countdownLabel);
        }
    }
}
