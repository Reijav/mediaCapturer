using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops;
using Vlc.DotNet.Forms;

namespace MediaCampturerControlerLib
{
    public partial class VideoPlayerVlc : Form
    {

        private string PathVideo;
        private string PathBaseImagenes;

        private long snapshotInterval = 0;
        private List<string> PathImagenes = new List<string>();

        public int a = 0;
        public int c = 0;
        public delegate void UpdateControlsDelegate();

        private int videoWidth = 800;
        private int videoHeight = 640;

        private int frecuenciaTrack = 100;

        private VlcControl vlcControl1;

        public event EventHandler<ImagenesSeleccionadasEventArgs> ImagenesSeleccionadasDeVideo;

        private Boolean usandoTrackbar = false;

        private void VlcControl1_VlcLibDirectoryNeeded1(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

        }

        public VideoPlayerVlc(string pathVideo, string pathBaseImagenes)
        {

            this.vlcControl1 = new Vlc.DotNet.Forms.VlcControl();
            this.vlcControl1.BeginInit();

            this.vlcControl1.BackColor = System.Drawing.Color.Black;
            this.vlcControl1.Location = new System.Drawing.Point(2, 0);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(653, 399);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 0;
            this.vlcControl1.Text = "vlcControl1";
            this.vlcControl1.VlcLibDirectory = null;
            this.vlcControl1.VlcLibDirectoryNeeded += VlcControl1_VlcLibDirectoryNeeded1;
            this.vlcControl1.VlcMediaplayerOptions = new[]
            {
                "-vv",
            };

            this.vlcControl1.EndInit();
            this.Controls.Add(this.vlcControl1);



            InitializeComponent();

            this.PathVideo = pathVideo;
            // this.PathVideo = @"G:\VIDEOS\GeforceExperience\Devil May Cry 5\Devil May Cry 5 2020.04.05 - 15.31.10.51.mp4";
            this.PathBaseImagenes = pathBaseImagenes;

            this.vlcControl1.PositionChanged += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPositionChangedEventArgs>(this.vlcControl1_PositionChanged);

            this.vlcControl1.Playing += new System.EventHandler<VlcMediaPlayerPlayingEventArgs>(SetProgresMax);

            this.vlcControl1.EndReached += VlcControl1_EndReached;

            this.vlcControl1.Capture = true;
            this.vlcControl1.SetMedia(new System.IO.FileInfo(this.PathVideo));

            this.vlcControl1.TimeChanged += (sender, e) =>
            {
                // Maps the time to a 5-seconds interval to take a snapshot every 5 seconds
                snapshotInterval = e.NewTime; /// 5000;

                trackBarVideo.Invoke((MethodInvoker)delegate
                {
                    var valorTrack = Convert.ToInt32(snapshotInterval / frecuenciaTrack);
                    if (!usandoTrackbar && valorTrack >= trackBarVideo.Minimum && valorTrack <= trackBarVideo.Maximum)
                    {
                        trackBarVideo.Value = valorTrack;
                    }
                });

            };



        }

        public VideoPlayerVlc( string pathBaseImagenes)
        {





            this.vlcControl1 = new Vlc.DotNet.Forms.VlcControl();
            this.vlcControl1.BeginInit();

            this.vlcControl1.BackColor = System.Drawing.Color.Black;
            this.vlcControl1.Location = new System.Drawing.Point(2, 0);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(653, 399);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 0;
            this.vlcControl1.Text = "vlcControl1";
            this.vlcControl1.VlcLibDirectory = null;
            this.vlcControl1.VlcLibDirectoryNeeded += VlcControl1_VlcLibDirectoryNeeded1;
            this.vlcControl1.VlcMediaplayerOptions = new[]
            {
            "-vv",
            };

            this.vlcControl1.EndInit();
            this.Controls.Add(this.vlcControl1);

            this.PathBaseImagenes = pathBaseImagenes;

            InitializeComponent();

            var result = openFileVideoDialog.ShowDialog();
            if (result.Equals(DialogResult.OK))
            {

                this.PathVideo = openFileVideoDialog.FileName;

                this.vlcControl1.PositionChanged += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPositionChangedEventArgs>(this.vlcControl1_PositionChanged);

                this.vlcControl1.Playing += new System.EventHandler<VlcMediaPlayerPlayingEventArgs>(SetProgresMax);

                this.vlcControl1.EndReached += VlcControl1_EndReached;

                this.vlcControl1.Capture = true;

                this.vlcControl1.SetMedia(new System.IO.FileInfo(this.PathVideo));

                this.vlcControl1.TimeChanged += (sender, e) =>
                {
                    // Maps the time to a 5-seconds interval to take a snapshot every 5 seconds
                    snapshotInterval = e.NewTime; /// 5000;

                    trackBarVideo.Invoke((MethodInvoker)delegate
                    {
                        var valorTrack = Convert.ToInt32(snapshotInterval / frecuenciaTrack);
                        if (!usandoTrackbar && valorTrack >= trackBarVideo.Minimum && valorTrack <= trackBarVideo.Maximum)
                        {
                            trackBarVideo.Value = valorTrack;
                        }
                    });

                };
            }
            else
            {
                this.Close();
            }




        }

        private void VlcControl1_EndReached(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {

            ThreadPool.QueueUserWorkItem(_ =>
            {

                vlcControl1.VlcMediaPlayer.Pause();

                this.vlcControl1.SetMedia(new System.IO.FileInfo(this.PathVideo));

                vlcControl1.Time = vlcControl1.Length - 1;

                buttonPlayPause.Invoke((MethodInvoker)delegate { buttonPlayPause.Text = "Reproducir"; trackBarVideo.Value = 0; });

                vlcControl1.Time = 0;

                ObtenerPresentarTiempo();

            });


        }

        private void ObtenerDatosVideo()
        {
            var mediaInformation = vlcControl1.GetCurrentMedia().Tracks;

            foreach(var mediaTrack in mediaInformation)
            {
                if (mediaTrack.TrackInfo is VideoTrack)
                {
                    videoHeight = Convert.ToInt32(((VideoTrack)mediaTrack.TrackInfo).Height);
                    videoWidth = Convert.ToInt32(((VideoTrack)mediaTrack.TrackInfo).Width);
                    break;
                }
            }
            

           

        }

        private void RedimensionarVlcControl()
        {
            this.vlcControl1.Height = trackBarVideo.Location.Y;

            this.vlcControl1.Width = this.vlcControl1.Height * videoWidth / videoHeight;



            if (this.vlcControl1.Width > this.Width - listViewCapturas.Width - 25)
            {
                this.vlcControl1.Width = this.Width - listViewCapturas.Width - 25;

                this.vlcControl1.Height = this.vlcControl1.Width * videoHeight / videoWidth;


            }


            imageListCapturas.ImageSize = new Size(imageListCapturas.ImageSize.Width, imageListCapturas.ImageSize.Width * videoHeight / videoWidth);

            this.trackBarVideo.Width = this.vlcControl1.Width;
        }

        private void vlcControl1_PositionChanged(object sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
            if (this.vlcControl1!=null && !this.vlcControl1.IsDisposed)
                this.InvokeUpdateControls();
        }


        public void InvokeUpdateControls()
        {
            
            
            if (this.InvokeRequired)


            {
                this.Invoke(new UpdateControlsDelegate(currentTrackTime));
            }
            else
            {
                currentTrackTime();
            }
        }

        private void currentTrackTime()
        {
            try
            {
                ObtenerPresentarTiempo();
            }
            catch (Exception ex)
            {

            }

        }


        private void SetProgresMax(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            Invoke(new Action(() =>
            {
                trackBarVideo.Value = trackBarVideo.Minimum;
                var vlc = (VlcControl)sender;

                if ((int)vlc.Length < 10000)
                {
                    frecuenciaTrack = 100;
                }
                else if ((int)vlc.Length >= 10000 && (int)vlc.Length < 50000)
                {
                    frecuenciaTrack = 200;
                }
                else
                {
                    frecuenciaTrack = 400;
                }

                trackBarVideo.Maximum = ((int)vlc.Length - 1) / frecuenciaTrack;
                //a = (int)vlc.Length / 1000; // Length (s)
                //c = a / 60; // Length (m)
                //a = a % 60; // Length (s)
                //label1.Text = 0 + "/" + c + ":" + a;

                ObtenerDatosVideo();

                RedimensionarVlcControl();

            }));
        }




        private void buttonPlayPause_Click(object sender, EventArgs e)
        {
            if (this.vlcControl1.IsPlaying)
            {
                this.vlcControl1.Pause();
                buttonPlayPause.Text = "Reproducir";
            }
            else
            {
                this.vlcControl1.Play();
                buttonPlayPause.Text = "Pausar";
            }


        }

        private void buttonAdelantar_Click(object sender, EventArgs e)
        {

            if (vlcControl1.Length > 6000 && vlcControl1.Length - vlcControl1.Time > 6000)
            {
                vlcControl1.VlcMediaPlayer.Time += 6000;
            }
            else
            {
                vlcControl1.VlcMediaPlayer.Time = vlcControl1.Length - 1;
                trackBarVideo.Value = trackBarVideo.Maximum;
            }
            ObtenerPresentarTiempo();
        }



        private void buttonSnapShot_Click(object sender, EventArgs e)
        {

            ThreadPool.QueueUserWorkItem(_ =>
            {
                string nombreArchivo = $"{PathBaseImagenes}\\{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff")}.jpg";

                var fileInfo = new FileInfo(nombreArchivo);
                if (this.vlcControl1.TakeSnapshot(Path.Combine(PathBaseImagenes, nombreArchivo), (uint)videoWidth,(uint) videoHeight) && fileInfo.Exists)
                {

                    var nuevoNombreArchivo=  nombreArchivo.Replace(".jpg",".bmp");
                    
                    var ImgDrawing = Image.FromFile(nombreArchivo);
                    ImgDrawing.Save(nuevoNombreArchivo , ImageFormat.Bmp);

                    

                    var Imagen = new Bitmap(nuevoNombreArchivo);

                    if (this.InvokeRequired)
                    {

                        listViewCapturas.Invoke((MethodInvoker)delegate
                        {
                            imageListCapturas.Images.Add(nuevoNombreArchivo, Imagen);
                            listViewCapturas.Items.Add(nuevoNombreArchivo, Path.GetFileName(nuevoNombreArchivo), imageListCapturas.Images.IndexOfKey(nuevoNombreArchivo));
                            listViewCapturas.Refresh();
                            if (PathImagenes == null)
                            {
                                PathImagenes = new List<string>();
                            }

                            PathImagenes.Add(nuevoNombreArchivo);
                        });

                    }
                    else
                    {
                        imageListCapturas.Images.Add(nuevoNombreArchivo, Imagen);
                        listViewCapturas.Items.Add(nuevoNombreArchivo, Path.GetFileName(nuevoNombreArchivo), imageListCapturas.Images.IndexOfKey(nuevoNombreArchivo));
                        listViewCapturas.Refresh();
                        if (PathImagenes == null)
                        {
                            PathImagenes = new List<string>();
                        }

                        PathImagenes.Add(nuevoNombreArchivo);
                    }

                }

            });

        }

        private void trackBarVideo_Scroll(object sender, EventArgs e)
        {

        }


        private void ObtenerPresentarTiempo()
        {
            try
            {
                var segundoTicks = new TimeSpan(0, 0, 1);

                var tiempoGrabacion = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(this.vlcControl1.Time));
                label1.Text = $"{tiempoGrabacion.Hours.ToString("D2")}:{tiempoGrabacion.Minutes.ToString("D2")}:{tiempoGrabacion.Seconds.ToString("D2")}";
            }
            catch (Exception ex)
            {

            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (vlcControl1.VlcMediaPlayer.Time > 6000)
            {
                vlcControl1.VlcMediaPlayer.Time -= 6000;
                ObtenerPresentarTiempo();
            }
            else
            {
                vlcControl1.VlcMediaPlayer.Time = 0;
                trackBarVideo.Value = trackBarVideo.Minimum;
                ObtenerPresentarTiempo();
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {

                this.vlcControl1.Stop();
                ImagenesSeleccionadasDeVideo.Invoke(sender, new ImagenesSeleccionadasEventArgs(PathImagenes));
                this.vlcControl1.Invoke((MethodInvoker)delegate { this.Close(); });
            }
            );
        }

        private void VideoPlayerVlc_FormClosing(object sender, FormClosingEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {

                this.vlcControl1.Stop();
                if (this.vlcControl1.GetCurrentMedia() != null)
                    this.vlcControl1.GetCurrentMedia().Dispose();
                if (!this.vlcControl1.IsDisposed)
                    this.vlcControl1.Dispose();

            }
            );



        }

        private void VideoPlayerVlc_Resize(object sender, EventArgs e)
        {
            RedimensionarVlcControl();
        }

        private void VideoPlayerVlc_Load(object sender, EventArgs e)
        {

            buttonPlayPause_Click(sender, e);


        }

        private void trackBarVideo_MouseUp(object sender, MouseEventArgs e)
        {

            ThreadPool.QueueUserWorkItem(_ =>
            {
                trackBarVideo.Invoke((MethodInvoker)delegate
                {


                    this.vlcControl1.Time = trackBarVideo.Value * frecuenciaTrack;
                    ObtenerPresentarTiempo();
                    this.vlcControl1.Pause();
                    buttonPlayPause.Text = "Pausar";

                    usandoTrackbar = false;


                    
                });

            });
        }

        private void trackBarVideo_MouseDown(object sender, MouseEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                trackBarVideo.Invoke((MethodInvoker)delegate
                {

                    usandoTrackbar = true;
                    this.vlcControl1.Pause();
                    buttonPlayPause.Text = "Reproducir";
                    ObtenerPresentarTiempo();

                });
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

        }
    }
}
