﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge.Video.DirectShow;
using Accord.Video.FFMPEG;
using AForge.Video;
using System.Drawing.Imaging;

namespace MediaCampturerControlerLib
{
    public partial class UserControlVideoCapturer: UserControl
    {


        public string path { get; set; }
        public List<string> PathImagenes { get; set; }
        public List<string>  PathVideos { get; set; }


        private FilterInfoCollection MisDispositivos;
        private VideoCaptureDevice MiWebCam;


        private VideoFileWriter FileWriter = new VideoFileWriter();
       //private SaveFileDialog saveAvi;
        private Bitmap Imagen;

        private const string PARAR_GRABAR = "Parar Grabación";
        private const string GRABAR_VIDEO = "Grabar Video";
        private const string DESCONECTAR = "Desconectar de Dispositivo";

        private string nombreArchivoVideo;
        private Image imagenVideo;


        public UserControlVideoCapturer()
        {
            InitializeComponent();
            this.path = @"c:\temp\capturador";
            this.PathImagenes = new List<string>();
            this.PathVideos = new List<string>();
        }



        public UserControlVideoCapturer(string path)
        {
            InitializeComponent();
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            if (!dirinfo.Exists)
            {
                dirinfo.Create();
            }

            this.path = path;
            this.PathImagenes = new List<string>();
            this.PathVideos = new List<string>();
        }


        private void UserControlVideoCapturer_Load(object sender, EventArgs e)
        {
            CargarDispositivos();
        }


        public void CargarDispositivos()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (MisDispositivos != null && MisDispositivos.Count > 0)
            {
                foreach (FilterInfo dispositivo in MisDispositivos)
                {

                    comboBoxDispositivos.Items.Add(dispositivo.Name);
                }
                comboBoxDispositivos.Text = MisDispositivos[0].Name;
            }

        }

        private long numeroPrevio;

        /// <summary>
        /// Evento de obtención de imagenes desde camara, tambien se guarda frames en el video .avi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newFrameEventArgs"></param>
        private void CapturandoImagen(object sender, NewFrameEventArgs newFrameEventArgs)
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {

                long numeroActual = DateTime.Now.Ticks;
                Imagen = (Bitmap)newFrameEventArgs.Frame.Clone();

                pictureBox1.Image = (Bitmap)newFrameEventArgs.Frame.Clone();

                //SI SE ENCUENTRA GRABANDO
                if (buttonGrabar.Text == PARAR_GRABAR && FileWriter != null)
                {
                    var lapsoTiempo = numeroActual - numeroPrevio;
                    var lapsoTiempoTS = new TimeSpan(numeroActual - numeroPrevio);
                                       
                    try
                    {

                        if (lapsoTiempoTS.TotalSeconds > 1)
                        {
                            FileWriter.WriteVideoFrame((Bitmap)newFrameEventArgs.Frame.Clone(), lapsoTiempoTS);
                        }
                        else
                        {
                            FileWriter.WriteVideoFrame((Bitmap)newFrameEventArgs.Frame.Clone());
                        }
                    }
                    catch (Exception er)
                    {

                        FileWriter.WriteVideoFrame((Bitmap)newFrameEventArgs.Frame.Clone());
                    }

                }
            }
        }


        private void CerrarWebCam()
        {
            if (buttonGrabar.Text == PARAR_GRABAR)
            {
                buttonGrabarVideo_Click(this, null);
            }


            Task.Delay(100);
            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                if (FileWriter != null && FileWriter.IsOpen)
                {
                    FileWriter.Close();
                    FileWriter.Dispose();
                }


                MiWebCam.SignalToStop();
                MiWebCam = null;
                pictureBox1.Image = null;
            }

        }

        private void FormCapturador_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarWebCam();
        }

        private void buttonGrabarVideo_Click(object sender, EventArgs e)
        {


            if (buttonGrabar.Text == PARAR_GRABAR)
            {
                timerRecording.Enabled = false;
                buttonGrabar.Text = GRABAR_VIDEO;
                buttonGrabar.ImageIndex = 1;
                if (MiWebCam == null)
                { return; }
                if (MiWebCam.IsRunning)
                {
                    FileWriter.Close();

                    if (imagenVideo != null)
                    {
                        imageListVideos.Images.Add(nombreArchivoVideo ,imagenVideo);
                        listViewIamgenesVideos.Items.Add(nombreArchivoVideo, Path.GetFileName(nombreArchivoVideo), listViewIamgenesVideos.Items.Count );
                        listViewIamgenesVideos.Refresh();
                    }
                   
                }
            }
            else
            {
               
                if (MiWebCam != null && MiWebCam.IsRunning && Imagen!=null)
                {
                    //saveAvi = new SaveFileDialog();
                    //saveAvi.Filter = "Avi Files (*.avi)|*.avi";
                    //if (saveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                    imagenVideo =(Bitmap) Imagen.Clone();
                    timerRecording.Enabled = true ;
                    buttonGrabar.ImageIndex = 0;
                    nombreArchivoVideo = $"{path}{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.avi";
                    numeroPrevio = DateTime.Now.Ticks;
                    int h = MiWebCam.VideoResolution.FrameSize.Height;
                    int w = MiWebCam.VideoResolution.FrameSize.Width;

                    PathVideos.Add(nombreArchivoVideo);

                    FileWriter.Open(nombreArchivoVideo, w, h, 25, VideoCodec.Default, 5000000);
                    FileWriter.WriteVideoFrame(Imagen);
                    buttonGrabar.Text = PARAR_GRABAR;

                    //}
                }
                else
                {
                    MessageBox.Show("No hay conexión a ningún dispositivo de video.");
                }

            }
        }


        private void buttonCapturarImg_Click_1(object sender, EventArgs e)
        {

            if (buttonObtenerVideo.Text == DESCONECTAR)
            {
                
                string nombreArchivo = $"{path}{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.jpg";

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write))
                    {
                        Imagen.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    // MessageBox.Show($"Imagen guardada en {path}");
                }

                //Este codigo causa  "A generic error occurred in GDI+."
                //imagenCapturada.Save(nombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageListCaptured.Images.Add(Imagen);
                PathImagenes.Add(nombreArchivo);
                listViewImages.Items.Add(nombreArchivo, Path.GetFileName(nombreArchivo), listViewImages.Items.Count);
                listViewImages.Refresh();
            }
            else
            {
                MessageBox.Show("No se encuentra conectado a ningún dispositivo.");
            }
        }

        private void buttonConectarDispositivo_Click(object sender, EventArgs e)
        {
            CerrarWebCam();
            if (buttonObtenerVideo.Text != DESCONECTAR)
            {

                if (comboBoxDispositivos.SelectedIndex >= 0)
                {
                    buttonObtenerVideo.Text = DESCONECTAR;
                    int indice = comboBoxDispositivos.SelectedIndex;
                    string nombreVideo = MisDispositivos[indice].MonikerString;
                    MiWebCam = new VideoCaptureDevice(nombreVideo);
                    MiWebCam.VideoResolution = MiWebCam.VideoCapabilities[0];
                    MiWebCam.NewFrame += new NewFrameEventHandler(CapturandoImagen);
                    MiWebCam.Start();
                    buttonObtenerVideo.ImageIndex = 4;
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un dispositivo");
                }

            }
            else
            {
                buttonObtenerVideo.Text = "Conectar con dispositivo";
                buttonObtenerVideo.ImageIndex = 3;
            }

        }



        public void CerrarComponente()
        {
            CerrarWebCam();
        }

        private void timerRecording_Tick(object sender, EventArgs e)
        {
            if (buttonGrabar.ImageIndex == 5)
                buttonGrabar.ImageIndex = 6;
            else
                buttonGrabar.ImageIndex = 5;
        }



        private void listViewImages_DoubleClick(object sender, EventArgs e)
        {
            if (listViewImages.SelectedIndices.Count > 0)
            {
                foreach (int indice in listViewImages.SelectedIndices)
                {
                    System.Diagnostics.Process.Start(listViewImages.Items[indice].Name);
                }

            }
                
        }

        private void listViewIamgenesVideos_DoubleClick(object sender, EventArgs e)
        {
            if (listViewIamgenesVideos.SelectedIndices.Count > 0)
            {
                foreach (int indice in listViewIamgenesVideos.SelectedIndices)
                {
                    System.Diagnostics.Process.Start(listViewIamgenesVideos.Items[indice].Name);
                }

            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {


                if(listaSeleccionada!=null && listaSeleccionada.Equals( "listViewImages"))
                {
                    if (listViewImages.SelectedIndices.Count > 0)
                    {

                        foreach (ListViewItem item in listViewImages.SelectedItems)
                        {


                            listViewImages.Items.Remove(item);
                            PathImagenes.Remove(item.Name);

                        }
                    }
                    listViewImages.Refresh();
                }
                else if(listaSeleccionada != null && listaSeleccionada.Equals("listViewIamgenesVideos"))
                {
                    if (listViewIamgenesVideos.SelectedIndices.Count > 0)
                    {

                        foreach (ListViewItem item in listViewIamgenesVideos.SelectedItems)
                        {


                            listViewIamgenesVideos.Items.Remove(item);
                            PathVideos.Remove(item.Name);

                        }
                    }
                    listViewIamgenesVideos.Refresh();
                }


        }

        private string listaSeleccionada;
        private void listViewImages_Click(object sender, EventArgs e)
        {
            listaSeleccionada = "listViewImages";
        }

        private void listViewIamgenesVideos_Click(object sender, EventArgs e)
        {
            listaSeleccionada = "listViewIamgenesVideos";
        }

        private void listViewIamgenesVideos_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaSeleccionada = "listViewIamgenesVideos";
        }

        private void listViewImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaSeleccionada = "listViewImages";
        }
    }
}
