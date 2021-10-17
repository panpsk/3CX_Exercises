using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Download
{
    public partial class Form1 : Form
    {
        WebClient client;
        private Stopwatch _stopWatch;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            string url = txtURL.Text;

            Thread thread = new Thread(() =>
            {
                Uri uri = new Uri(url);
                string filename = Path.GetFileName(uri.AbsolutePath);
                client.DownloadFileAsync(uri, Application.StartupPath + "/" + filename);

            });
            thread.Start();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTimeToCompletion.Text = "";
            client = new WebClient();
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            btnDownload.Enabled = true;
            MessageBox.Show("Download Complete!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Minimum = 0;
                double received = double.Parse(e.BytesReceived.ToString());
                double total = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = Math.Round((received / total * 100), 2);
                lblPercentage.Text = string.Format("{0:0.##}", percentage.ToString()) + "%";
                progressBar1.Value = int.Parse(Math.Round(percentage, 0).ToString());

                var secondsToComplete = decimal.Parse((_stopWatch.ElapsedMilliseconds * e.TotalBytesToReceive / e.BytesReceived / 1000).ToString());


                if (progressBar1.Value == 0)
                    lblTimeToCompletion.Text = "Time Left : Estimating...";
                else
                {
                    double timeLeft = (100 - progressBar1.Value) * _stopWatch.ElapsedMilliseconds / progressBar1.Value/1000;
                    TimeSpan ts = TimeSpan.FromSeconds(timeLeft);
                    lblTimeToCompletion.Text = String.Format("Time Left: {0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds);
                    if (timeLeft < 60)
                        lblTimeToCompletion.Text = String.Format("Time Left: {0} sec", ts.Seconds);
                    else if (timeLeft < 3600)
                        lblTimeToCompletion.Text = String.Format("Time Left: {0} min {1} sec", ts.Minutes, ts.Seconds);
                    else if (timeLeft < 86400)
                        lblTimeToCompletion.Text = String.Format("Time Left: {0} hours {1} min {2} sec", ts.Hours, ts.Minutes, ts.Seconds);
                    else
                        lblTimeToCompletion.Text = String.Format("Time Left: {0} days {1} hours {2} min {3} sec", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);

            }
        }));
        }
}
}
