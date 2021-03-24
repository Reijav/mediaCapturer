using Accord.Video;
using Accord.Video.DirectShow;
using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaCampturerControlerLib
{
    public partial class VideoPlayer : Form
    {

        public string PathVideo { get; set; }
        public string PathBaseImagenes { get; set; }
        public List<string> PathImagenes { get; set; }

        private VideoFileReader reader;

        private Stopwatch stopWatch = null;


        private Dictionary<long, VideoFileReader> diccionarioReader;

        private Image imagenCapt = null;

        private int frameIndex;
        //private Bitmap Imagen;
        private double framePerSecond;
        public event EventHandler<ImagenesSeleccionadasEventArgs> ImagenesSeleccionadasDeVideo;

        public VideoPlayer(string pathVideo, string pathBaseImagenes)
        {
            InitializeComponent();

            reader = new VideoFileReader();

            //this.PathVideo = @"G:\VIDEOS\GeforceExperience\Devil May Cry 5\Devil May Cry 5 2020.04.05 - 15.31.10.51.mp4";//pathVideo;
            this.PathVideo = pathVideo;
            this.PathBaseImagenes = pathBaseImagenes;



            // FileVideoSource fileSource = new FileVideoSource(this.PathVideo);


            // OpenVideoSource(fileSource);



            reader.Open(PathVideo);

            pictureBoxPlayer.Height = this.Height - panelControles.Height - trackBar1.Height - 5;
            pictureBoxPlayer.Width = pictureBoxPlayer.Height * reader.Width / reader.Height;

            if (pictureBoxPlayer.Width > this.Width - listViewCapturas.Width)
            {
                pictureBoxPlayer.Width = this.Width - listViewCapturas.Width;

                pictureBoxPlayer.Height = pictureBoxPlayer.Width * reader.Height / reader.Width;
            }

            trackBar1.Maximum = Convert.ToInt32(reader.FrameCount.ToString());

            timerPlayer.Interval = Convert.ToInt32(1000 / reader.FrameRate.Value) / 4;

            pictureBoxPlayer.Image = (Bitmap)reader.ReadVideoFrame().Clone();
            frameIndex = 0;

            framePerSecond = reader.FrameRate.Value;

            imageListCapturas.ImageSize = new Size(imageListCapturas.ImageSize.Width, reader.Height * imageListCapturas.ImageSize.Width / reader.Width);

            PathImagenes = new List<string>();

        }



        private void OpenVideoSource(IVideoSource source)
        {
            // set busy cursor
            this.Cursor = Cursors.WaitCursor;

            // stop current video source
            CloseCurrentVideoSource();

            // start new video source
            videoSourcePlayerVideo.VideoSource = source;
            videoSourcePlayerVideo.Start();

            // reset stop watch
            stopWatch = null;

            // start timer
            timerVp.Start();

            this.Cursor = Cursors.Default;
        }

        // Close video source if it is running
        private void CloseCurrentVideoSource()
        {
            if (videoSourcePlayerVideo.VideoSource != null)
            {
                videoSourcePlayerVideo.SignalToStop();

                // wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!videoSourcePlayerVideo.IsRunning)
                        break;
                    System.Threading.Thread.Sleep(100);
                }

                if (videoSourcePlayerVideo.IsRunning)
                {
                    videoSourcePlayerVideo.Stop();
                }

                videoSourcePlayerVideo.VideoSource = null;
            }
        }


        private void timerPlayer_Tick(object sender, EventArgs e)
        {

            try
            {
                if (reader != null && reader.IsOpen)
                {

                    var frame = reader.ReadVideoFrame();

                    if (frame != null)
                    {
                        pictureBoxPlayer.Image = (Bitmap)frame;
                        frameIndex++;

                    }
                    else
                    {
                        timerPlayer.Stop();

                    }

                    trackBar1.Value = frameIndex;
                    ObtenerPresentarTiempo();
                    // frame.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,ex.Message);
            }

        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //if (videoSourcePlayerVideo.IsRunning)
            //{
            //    videoSourcePlayerVideo.Stop();

            //}
            //else
            //{
            //    videoSourcePlayerVideo.Start();
            //}


            if (timerPlayer.Enabled)
            {
                PararReproduccion();
            }
            else
            {
                ReproducirPelicula();
            }


        }

        private void ReproducirPelicula()
        {
            timerPlayer.Start();
            btnPlay.Text = "Parar Reproducción";
        }

        private void PararReproduccion()
        {
            timerPlayer.Stop();
            btnPlay.Text = "Reproducir";
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            PararReproduccion();
            frameIndex = frameIndex - Convert.ToInt32(reader.FrameRate.Value * 2);

            if (frameIndex < 0)
            {
                frameIndex = 0;
            }


            reader.Close();
            reader.Open(this.PathVideo);

            for (int index = 0; index <= frameIndex; index++)
            {
                reader.ReadVideoFrame();
            }

            pictureBoxPlayer.Image = (Bitmap)reader.ReadVideoFrame().Clone();

            trackBar1.Value = frameIndex;


        }

        private void btnAdelantar_Click(object sender, EventArgs e)
        {

            PararReproduccion();

            if (reader != null && reader.IsOpen)
            {
                var framesAdelantar = Convert.ToInt32(reader.FrameRate.Value * 2);


                if (frameIndex + framesAdelantar > reader.FrameCount)
                {

                    framesAdelantar = Convert.ToInt32(reader.FrameCount) - frameIndex;
                }

                frameIndex = frameIndex + framesAdelantar;

                for (int i = 0; i < framesAdelantar; i++)
                {
                    reader.ReadVideoFrame();
                }
                if (frameIndex < reader.FrameCount)
                {
                    var frame = reader.ReadVideoFrame();
                    if (frame != null)
                    {
                        pictureBoxPlayer.Image = (Bitmap)reader.ReadVideoFrame().Clone();
                    }

                }

                trackBar1.Value = frameIndex;
            }
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            PararReproduccion();

            if (trackBar1.Value > frameIndex)
            {
                var framesAdelantar = trackBar1.Value - frameIndex;
                frameIndex = frameIndex + framesAdelantar;

                for (int i = 0; i < framesAdelantar; i++)
                {
                    reader.ReadVideoFrame();
                }

            }
            else
            {
                frameIndex = trackBar1.Value;
                reader.Close();
                reader.Open(this.PathVideo);
                for (int index = 0; index < frameIndex - 1; index++)
                {
                    var aux = reader.ReadVideoFrame();

                }
            }


            var imagen = reader.ReadVideoFrame();//reader.ReadVideoFrame();

            if (imagen != null)
                pictureBoxPlayer.Image = (Bitmap)imagen;// (Bitmap)imagen.Clone();

            ObtenerPresentarTiempo();
        }

        private void ObtenerPresentarTiempo()
        {
            var segundoTicks = new TimeSpan(0, 0, 1);


            var tiempoGrabacion = new TimeSpan(0, 0, Convert.ToInt32(frameIndex / framePerSecond));
            labelTiempo.Text = $"{tiempoGrabacion.Hours.ToString("D2")}:{tiempoGrabacion.Minutes.ToString("D2")}:{tiempoGrabacion.Seconds.ToString("D2")}";
        }

        private void buttonCapturarImg_Click(object sender, EventArgs e)
        {

            string nombreArchivo = $"{PathBaseImagenes}\\{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff")}.jpg";
            var Imagen = (Bitmap)pictureBoxPlayer.Image.Clone();

            //var Imagen = videoSourcePlayerVideo.GetCurrentVideoFrame();

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write))
                {

                    Imagen.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
                // MessageBox.Show(this,$"Imagen guardada en {path}");
            }

            //Este codigo causa  "A generic error occurred in GDI+."
            //imagenCapturada.Save(nombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);
            imageListCapturas.Images.Add(nombreArchivo, Imagen);

            listViewCapturas.Items.Add(nombreArchivo, Path.GetFileName(nombreArchivo), imageListCapturas.Images.IndexOfKey(nombreArchivo));
            listViewCapturas.Refresh();

            if (PathImagenes == null)
            {
                PathImagenes = new List<string>();
            }

            PathImagenes.Add(nombreArchivo);
        }

        private void buttonAdjuntarFotosParaInforme_Click(object sender, EventArgs e)
        {
            ImagenesSeleccionadasDeVideo.Invoke(sender, new ImagenesSeleccionadasEventArgs(PathImagenes));
            this.Close();
        }

        private void VideoPlayer_Resize(object sender, EventArgs e)
        {

            pictureBoxPlayer.Height = this.Height - panelControles.Height - trackBar1.Height - 5;

            pictureBoxPlayer.Width = pictureBoxPlayer.Height * reader.Width / reader.Height;

            if (pictureBoxPlayer.Width > this.Width - listViewCapturas.Width)
            {
                pictureBoxPlayer.Width = this.Width - listViewCapturas.Width;

                pictureBoxPlayer.Height = pictureBoxPlayer.Width * reader.Height / reader.Width;


            }
        }

        private void VideoPlayer_Load(object sender, EventArgs e)
        {

        }

        private void VideoPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void timerVp_Tick(object sender, EventArgs e)
        {
            IVideoSource videoSource = videoSourcePlayerVideo.VideoSource;

            if (videoSource != null)
            {
                // get number of frames since the last timer tick
                int framesReceived = videoSource.FramesReceived;

                videoSource.NewFrame += VideoSource_NewFrame;

                if (stopWatch == null)
                {
                    stopWatch = new Stopwatch();
                    stopWatch.Start();
                }
                else
                {
                    stopWatch.Stop();

                    float fps = 1000.0f * framesReceived / stopWatch.ElapsedMilliseconds;

                    labelTiempo.Text = fps.ToString("F2") + " fps";

                    //   fpsLabel.Text = fps.ToString("F2") + " fps";

                    stopWatch.Reset();
                    stopWatch.Start();
                }
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {


            imagenCapt = (Bitmap)eventArgs.Frame.Clone();
        }
    }

    public class ImagenesSeleccionadasEventArgs : EventArgs
    {
        public ImagenesSeleccionadasEventArgs(List<string> paths)
        {
            PathImagenes = paths;
        }

        public List<string> PathImagenes { get; set; }
    }
}
