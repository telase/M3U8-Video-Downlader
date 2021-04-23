using M3U8Downloader.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace M3U8Downloader
{

    public partial class Form1 : Form
 {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Settings.Default.dil == "English")
            {
                Localization.Culture = new CultureInfo("en-US");
            }
            else
            {
                Localization.Culture = new CultureInfo("");
            }
            dilSeçimiToolStripMenuItem.Text = Localization.dilSecimi;
            btnDownload.Text = Localization.btnDownload;
            btnExit.Text = Localization.btnExit;
            button1.Text = Localization.button1;
            button3.Text = Localization.button3;
            label1.Text = Localization.label1;
            label2.Text = Localization.label2;
            label4.Text = Localization.label4;
        }

        private FolderBrowserDialog FolderBrowserDialog;
        private string SaveLocationBox_Text;

        public Form1(string saveLocationBox_Text)
        {
            SaveLocationBox_Text = saveLocationBox_Text;
        }

        public Form1(TextBox SaveLocationBox)
        {
            this.saveLocationBox = SaveLocationBox;
        }

        public void ExecuteCommand(string command)
        {

            //Create process

            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "CMD.exe";
            pProcess.StartInfo.Arguments = command;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;

            //pProcess.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandler);
            // Start the asynchronous read
            pProcess.Start();
            pProcess.BeginOutputReadLine();
            pProcess.WaitForExit();
            pProcess.Close();
        }
       
        


        private void TxtM3U8_MouseClick(object sender, MouseEventArgs e)
        {
            txtM3U8.SelectAll();
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Text = "Downloading...";
            bgDownload.RunWorkerAsync();
        }

public void Download()
        {
            string m3u8 = txtM3U8.Text;
            var targetPath = Path.Combine(saveLocationBox.Text, textBox_Name.Text);
            string cmd = "/C ffmpeg -i \"" + m3u8 + "\" -acodec copy -vcodec copy -absf aac_adtstoasc \"" + targetPath + ".mp4\"";
            ExecuteCommand(cmd);
        }

        private void BgDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            Download();
        }

        private void BgDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("FINIS");
            btnDownload.Text = "DOWNLOAD";
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TxtM3U8_TextChanged(object sender, EventArgs e)
        {
            txtM3U8.ForeColor = Color.Black;
            txtM3U8.Font = new Font(txtM3U8.Font, FontStyle.Regular);
        }

        private void TxtM3U8_Leave(object sender, EventArgs e)
        {
            txtM3U8.ForeColor = SystemColors.GrayText;
            txtM3U8.Font = new Font(txtM3U8.Font, FontStyle.Italic);
        }

        private void TextBox_Name_TextChanged(object sender, EventArgs e)
        {
            txtM3U8.SelectAll();
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                WorkingDirectory = @"c:\",
                FileName = "SendSignal.exe",
                Arguments = "ffmpeg.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            process.StartInfo = startInfo;
            process.Start();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog = new FolderBrowserDialog();
            DialogResult = FolderBrowserDialog.ShowDialog();

            saveLocationBox.Clear();
            saveLocationBox.AppendText(FolderBrowserDialog.SelectedPath);
            saveLocationBox.ReadOnly = true;
        }

        private void SaveLocationBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {
        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }

        private void Label2_Click(object sender, EventArgs e)
        {
        }

        private void türkçeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.dil = "Türkçe";
            Settings.Default.Save();
            Application.Restart();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.dil = "English";
            Settings.Default.Save();
            Application.Restart();
        }

        private void dilSecimiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
