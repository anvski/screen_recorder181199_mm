using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Accord.Video.FFMPEG;
using System;
using NAudio.Wave;
using Xabe.FFmpeg;
using System.Threading.Tasks;
using screen_recorder181199.Properties;
using System.Linq;

namespace screen_recorder181199
{
    class ScreenRecorder
    {
        private Rectangle bounds;
        private string outputPath = "";
        private string tempPath = "";
        private int fileCount = 1;
        private int frameRate = 10; // default vrednost 10
        private List<string> inputImageSequence = new List<string>();

        private string audioName = "audiorec.wav";
        private string videoName = "video";
        private string finalName = "FinalVideo";
        private string extensionFinal = ".avi";
        private Form1 form; // cuva sostojba od formata 

        public WaveFileWriter waveWriter;
        public WaveIn sourceStream;
        public bool isRecordingAudio;

        Stopwatch watch = new Stopwatch();

        public static class NativeMethods
        {
            [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        }

        public ScreenRecorder(Rectangle b, string outPath, Form1 form)
        {
            CreateTempFolder("tempScreenCaps");
            bounds = b;
            outputPath = outPath;
            this.form = form;
            this.isRecordingAudio = false;
            this.OpenAudioStream(); // pri kreiranje na ScreenRecorder objekt, se otvora stream za vcituvanje na podatoci od vlezen ured.
        }
        private void CreateTempFolder(string name)
        {
            if (Directory.Exists("C://"))
            {
                string pathName = $"C://{name}";
                Directory.CreateDirectory(pathName);
                tempPath = pathName;
            }
            else
            {
                string pathName = $"C://Documents//{name}";
                Directory.CreateDirectory(pathName);
                tempPath = pathName;
            }
        }

        private void DeletePath(string targetDir)
        {
            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeletePath(dir);
            }

            Directory.Delete(targetDir, false);
        }

        public string getElapsed()
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", watch.Elapsed.Hours, watch.Elapsed.Minutes, watch.Elapsed.Seconds);
        }

        public void RecordVideo()
        {
            //se zemaat screenshots so koi podocna se kreira video fajl
            if (!watch.IsRunning)
                watch.Start();

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                }
                string name = tempPath + "//screenshot-" + fileCount + ".png";
                bitmap.Save(name, ImageFormat.Png);
                inputImageSequence.Add(name);
                fileCount++;
            }
        }

        public void OpenAudioStream()
        {
            if (form.getItems().Items.Count == 0) return;
            if (form.getItems().SelectedItems.Count == 0) return;
            int deviceNumber = form.getItems().SelectedItems[0].Index; // sekogas kje bide edno selektirano, no gi dava kako array pa [0] za pristap
            sourceStream = new NAudio.Wave.WaveIn();
            //NAudio.Wave.WaveFormat
            sourceStream.DeviceNumber = deviceNumber;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

            sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceSteram_DataAvailable);
            string finalAudioName = Path.Combine(outputPath, audioName);

            waveWriter = new NAudio.Wave.WaveFileWriter(finalAudioName, sourceStream.WaveFormat);

            sourceStream.StartRecording();
        }

        public void RecordAudio()
        {
            isRecordingAudio = true;
        }

        public void convertAudioToAac()
        {
            string finalAudioName = Path.Combine(outputPath, audioName);
            using (MediaFoundationReader reader = new MediaFoundationReader(finalAudioName))
            {
                string[] rawName = finalAudioName.Split('.');
                string convertedAudio = rawName[0] + ".mp4";
                MediaFoundationEncoder.EncodeToAac(reader, convertedAudio);
            }
        }

        public void convertAudioToMp3()
        {
            string finalAudioName = Path.Combine(outputPath, audioName);
            using (MediaFoundationReader reader = new MediaFoundationReader(finalAudioName))
            {
                string[] rawName = finalAudioName.Split('.');
                string convertedAudio = rawName[0] + ".mp3";
                MediaFoundationEncoder.EncodeToMp3(reader, convertedAudio);
            }
        }
        private void sourceSteram_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            if (isRecordingAudio)
            {
                if (waveWriter == null) return;
                waveWriter.Write(args.Buffer, 0, args.BytesRecorded);
                waveWriter.Flush();
            }
            float max = 0;
            // se pretpostavuva deka e 16-bitno audio
            for (int index = 0; index < args.BytesRecorded; index += 2)
            {
                short sample = (short)((args.Buffer[index + 1] << 8) |
                                        args.Buffer[index + 0]);
                // se pretvara vo float
                var sample32 = sample / 44100f;
                // najdi absolutna vrednost
                if (sample32 < 0) sample32 = -sample32;
                // najdi max vrednost
                if (sample32 > max) max = sample32;
            }

            form.setProgressBarValue(max * 100);
        }

        private void SaveAudio()
        {
            isRecordingAudio = false;
            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }
            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }
        }

        public void setFramerate(int fr)
        {
            this.frameRate = fr;
        }
        private void SaveVideo(int width, int height)
        {
            extensionFinal = form.getFileExtension();
            videoName = videoName + extensionFinal;

            using (VideoFileWriter vFWriter = new VideoFileWriter())
            {
                if(form.getSelectedCodec().Equals("MPEG"))
                    vFWriter.Open(outputPath + "//" + videoName, width, height, frameRate, Accord.Video.FFMPEG.VideoCodec.MPEG4);
                else if(form.getSelectedCodec().Equals("H264"))
                    vFWriter.Open(outputPath + "//" + videoName, width, height, frameRate, Accord.Video.FFMPEG.VideoCodec.H264);
                else vFWriter.Open(outputPath + "//" + videoName, width, height, frameRate, Accord.Video.FFMPEG.VideoCodec.MPEG4);
                // default se izbira MPEG4

                foreach (string imageLocation in inputImageSequence)
                {
                    Bitmap imageFrame = System.Drawing.Image.FromFile(imageLocation) as Bitmap;
                    vFWriter.WriteVideoFrame(imageFrame);
                    imageFrame.Dispose();
                }

                vFWriter.Close();
            }
        }
        private void CombineVideoAndAudio(string video, string audio)
        {
            if (form.getFileExtension().Equals(".flv") || form.getFileExtension().Equals(".m4v") || form.getFileExtension().Equals(".3gp") || form.getFileExtension().Equals(".3g2"))
            {
                convertAudioToAac(); // ako se izbrani ovie extensions na krajniot rezultat (video + audio), se upotrebuva audio enkodirano so AAC
                //no bidejkji vo C# polesno se snima i zapisuva audio vo WAV format, se konvertira od WAV vo AAC.
            }
            string args = $"/c ffmpeg -i \"{video}\" -i \"{audio}\" -c:v copy -c:a aac {finalName + extensionFinal}";
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                FileName = "cmd.exe",
                WorkingDirectory = outputPath,
                Arguments = args
            };

            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }
        public void Stop()
        {
            watch.Stop();

            int width = bounds.Width;
            int height = bounds.Height;

            SaveAudio();

            SaveVideo(width, height);

            CombineVideoAndAudio(videoName, audioName);

            DeletePath(tempPath);

            //convertAudioToMp3();
           // OpenAudioStream(); // se otvora AudioStream od novo za da funkcionira progress-bar-ot
        }
    }
}