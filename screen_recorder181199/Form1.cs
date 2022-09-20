using NAudio.Wave;
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

namespace screen_recorder181199
{
    public partial class Form1 : Form
    {
        bool folderSelected = false;
        bool isRecording = false;
        string outputPath = "";
        string finalVidName = "FinalVideo.mp4";
        List<string[]> itemsInLbFormats;
        Rectangle bounds;
        Thread showScreen;

        ScreenRecorder screenRecorder;
        public Form1()
        {
            InitializeComponent();
            refreshAudioSources();
            showScreen = new Thread(CaptureScreen);
            itemsInLbFormats = new List<string[]>();
            string[] formatsForMPEG = new string[] { ".mp4", ".3gp", ".3g2", ".mkv" };
            string[] formatsForH264 = new string[] { ".m4v", ".mp4", ".mkv", ".flv"};
            itemsInLbFormats.Add(formatsForMPEG);
            itemsInLbFormats.Add(formatsForH264);

            lbFramerate.Items.Add("10");
            lbFramerate.Items.Add("20");
            lbFramerate.Items.Add("25");
            pbScreenRec.SizeMode = PictureBoxSizeMode.StretchImage;
            if (lwSources.Items.Count > 0)
            {
                lwSources.Items[0].Selected = true;
                lwSources.Select(); // po default se selektira audio input izvor
            }
            lbCodec.SelectedIndex = 0;
            lbFormats.SelectedIndex = 0; // default selekcija za da se izbegnat errors.
           // screenRecorder = new ScreenRecorder(Screen.PrimaryScreen.Bounds, Directory.GetCurrentDirectory(), this);
            updateLbFormats();
            showScreen.Start();
        }
        public string getSelectedCodec()
        {
            return lbCodec.SelectedItem.ToString();
        }
        public void setProgressBarValue(double value)
        {
            pbAudio.Value = (int)value;
        }
        private void CaptureScreen()
        {
            while (true)
            {
                Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics g = Graphics.FromImage(bm);
                g.CopyFromScreen(0, 0, 0, 0, bm.Size);
                pbScreenRec.Image = bm;
                Thread.Sleep(timerRec.Interval);
            }
        }
        private void slctFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "Select an output folder";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                outputPath = folderBrowser.SelectedPath;
                folderSelected = true;

                this.bounds = Screen.PrimaryScreen.Bounds;
                screenRecorder = new ScreenRecorder(bounds, outputPath, this);
            }
            else
            {
                MessageBox.Show("Select a valid folder", "Error");
            }
        }

        public ListView getItems()
        {
            return lwSources;
        }

        private void timerRec_Tick(object sender, EventArgs e)
        {
            screenRecorder.RecordVideo();
            lblTime.Text = screenRecorder.getElapsed();
        }

        private void recBtn_Click(object sender, EventArgs e)
        {
            if (folderSelected)
            {
                isRecording = true;
                timerRec.Start();
                screenRecorder.RecordAudio();
                lockControls();
            }
            else
            {
                MessageBox.Show("Select an output folder before recording", "Error");
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (!timerRec.Enabled) return;
            pbScreenRec.Image = null;
            timerRec.Stop();
            screenRecorder.Stop();
            //Application.Restart();

            lblTime.Text = "00:00:00"; // reset na timer-ot
            isRecording = false;
            screenRecorder = new ScreenRecorder(bounds, outputPath, this); // se instancira screenRecorder-ot od ponovo so istite parametri
            showControls();
        }
        private void refreshAudioSources()
        {
            lwSources.Items.Clear();
            List<NAudio.Wave.WaveInCapabilities> sources = new List<NAudio.Wave.WaveInCapabilities>();

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            foreach (var source in sources)
            {
                ListViewItem item = new ListViewItem(source.ProductName);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, source.Channels.ToString()));
                lwSources.Items.Add(item);
            }

        }

        private void btnRefresh_click(object sender, EventArgs e)
        {
            refreshAudioSources();
        }

        private void btnAudioRec_Click(object sender, EventArgs e)
        {
        }

        private void btnStopAudio_Click(object sender, EventArgs e)
        {

        }
        private void updateLbFormats()
        {
            lbFormats.Items.Clear();
            foreach (string s in itemsInLbFormats[lbCodec.SelectedIndex])
            {
                lbFormats.Items.Add(s);
            }
        }
        private void lbCodec_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateLbFormats();
        }

        public string getFileExtension()
        {
            if (lbFormats.SelectedItem == null) return ".avi";
            return lbFormats.SelectedItem.ToString();
        }

        private void lbFramerate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(isRecording) MessageBox.Show("Currently recording, no effect in change", "Error");
            else if (screenRecorder == null)
            {
                MessageBox.Show("Select an output folder before adjusting the framerate", "Error");
            }
            else
            {
                screenRecorder.setFramerate(int.Parse(lbFramerate.SelectedItem.ToString()));
                this.timerRec.Interval = 100 / int.Parse(lbFramerate.SelectedItem.ToString()); // 100ms / odbraniot framerate za kolku sliki da kolku ticks da ima timer-ot vo sekunda, odnosno kolku screenshots
            }

        }

        private void lbFormats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isRecording) MessageBox.Show("Currently recording, no effect in change", "Error");
        }

        private void lockControls()
        {
            lbCodec.Hide();
            lbFormats.Hide();
            lbFramerate.Hide();
            recBtn.Enabled = false;
            lwSources.Hide();
            slctFolder.Enabled = false;
            label2.Hide();
            label3.Hide();
            label4.Hide();
            btnRefresh.Hide();
        }

        private void showControls()
        {
            lbCodec.Show();
            lbFormats.Show();
            lbFramerate.Show();
            recBtn.Enabled = true;
            lwSources.Show();
            slctFolder.Enabled = true;
            label2.Show();
            label3.Show();
            label4.Show();
            btnRefresh.Show();
        }
    }
}
