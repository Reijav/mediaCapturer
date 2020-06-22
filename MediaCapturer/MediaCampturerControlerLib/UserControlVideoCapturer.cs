using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AForge.Video.DirectShow;
using Accord.Video.FFMPEG;
using AForge.Video;
using System.Drawing.Imaging;

namespace MediaCampturerControlerLib
{
    public partial class UserControlVideoCapturer : UserControl
    {


        public string path { get; set; }

        private List<string> PathImagenesPr;

        private object obj = new object();
        private object obj2 = new object();

        private long ticksInicioGrabado = 0;


        public List<string> PathImagenes
        {
            get {
                if (PathImagenesPr == null)
                {
                    PathImagenesPr = new List<string>();
                }
                return PathImagenesPr;
            }
            set {

                PathImagenesPr = value;
                var error = false;
                string msgerror = "";

                Task.Factory.StartNew(() =>
                {
                    Parallel.ForEach(PathImagenesPr, (pathImagen) =>
                    {
                        Bitmap imagenCapt = null;

                        try
                        {
                            imagenCapt = (Bitmap) new Bitmap(pathImagen).Clone();

                            lock (obj)
                            {
                                imageListCaptured.Images.Add(pathImagen, imagenCapt);
                                var listViewIte = new ListViewItem()
                                {
                                    Name = pathImagen,
                                    Text = Path.GetFileName(pathImagen),
                                    ImageIndex = imageListCaptured.Images.IndexOfKey(pathImagen),
                                };

                                listViewImages.Items.Add(listViewIte);
                            }


                        }
                        catch(Exception er)
                        {
                            error = true;

                            msgerror += "\n"+er.Message;
                        }


                    });
                    if (error)
                        MessageBox.Show("Error, no se pudo cargar uno o varios archivos" + msgerror, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listViewImages.Refresh();
                }
                );
            }
        }

        private List<string> PathVideosPr;

        public List<string> PathVideos
        {
            get
            {
                if (PathVideosPr == null)
                {
                    PathVideosPr = new List<string>();
                }
                return PathVideosPr;
            }
            set {

                PathVideosPr = value;
                var error = false;
                string msgerror = "";
                Parallel.ForEach(PathVideosPr, (pathVideo) => {
                    
                    try
                    {
                        Bitmap imagen = null;
                        using (VideoFileReader reader = new VideoFileReader())
                        {
                            reader.Open(pathVideo);

                            imagen = (Bitmap)reader.ReadVideoFrame().Clone();

                            reader.Close();
                        }

                        lock (obj)
                        {
                            imageListVideos.Images.Add(pathVideo, imagen);
                            var listViewIte = new ListViewItem()
                            {
                                Name = pathVideo,
                                Text = Path.GetFileName(pathVideo),
                                ImageIndex = imageListVideos.Images.IndexOfKey(pathVideo),
                            };
                            listViewIamgenesVideos.Items.Add(listViewIte);
                        }


                    }
                    catch(Exception er)
                    {
                        error = true;
                        msgerror += msgerror + "\n"; 
                    }




                } );
                if (error)
                    MessageBox.Show("Error, no se pudo cargar uno o varios archivos. \n" + msgerror , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                listViewIamgenesVideos.Refresh();



            }
        }

        private List<String> ArchivosEliminadosPr;

        public List<String> ArchivosEliminados
        {
            get
            {
                if (ArchivosEliminadosPr == null)
                {
                    ArchivosEliminadosPr = new List<string>();
                }
                return ArchivosEliminadosPr;
            }
            set { ArchivosEliminadosPr = value; }
        }



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
            this.PathImagenesPr = new List<string>();
            this.PathVideosPr = new List<string>();
            this.ArchivosEliminadosPr = new List<string>();
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
            this.PathImagenesPr = new List<string>();
            this.PathVideosPr = new List<string>();
            this.ArchivosEliminadosPr = new List<string>();
        }


        private void UserControlVideoCapturer_Load(object sender, EventArgs e)
        {
            CargarDispositivos();
        }


        private void comboBoxDispositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            int indice = comboBoxDispositivos.SelectedIndex;
            string nombreVideo = MisDispositivos[indice].MonikerString;
            var WebCam = new VideoCaptureDevice(nombreVideo);
            comboBoxCapabilitis.Items.Clear();
            comboBoxInputs.Items.Clear();

            //CARGA CAPACIDADES DEL DISPOSITIVO
            foreach (var capability in WebCam.VideoCapabilities)
            {
                string nombre = $"{capability.FrameSize.Width.ToString()} x {capability.FrameSize.Height.ToString()} - FrameRate {capability.AverageFrameRate.ToString()} - {capability.MaximumFrameRate.ToString()} - Bitcount {capability.BitCount.ToString()}";
                comboBoxCapabilitis.Items.Add(nombre);
            }


            //CARGA VIDEO INPUTS DISPONIBLES
            foreach (var input in WebCam.AvailableCrossbarVideoInputs)
            {
                comboBoxInputs.Items.Add(input.Index + "-" +  input.Type.ToString());
                    
            }

            if (comboBoxCapabilitis.Items.Count > 0)
            {
                comboBoxCapabilitis.SelectedIndex = 0;
            }
            else
            {
                comboBoxCapabilitis.SelectedIndex = -1;
            }

            comboBoxInputs.Enabled = false;
            if (comboBoxInputs.Items.Count > 0)
            {
                comboBoxInputs.SelectedIndex = 1;
                comboBoxInputs.Enabled = true;
            }
            else
            {
                comboBoxInputs.SelectedIndex = -1;
            }

            WebCam = null;
        }

        FilterInfoCollection MisDispositivosCompresores;

        public void CargarDispositivos()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            MisDispositivosCompresores = new FilterInfoCollection(FilterCategory.VideoCompressorCategory);

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

                pictureBox1.Image = Imagen;//(Bitmap)newFrameEventArgs.Frame.Clone();

                //SI SE ENCUENTRA GRABANDO
                if (buttonGrabar.Text == PARAR_GRABAR && FileWriter != null)
                {
                    var lapsoTiempo = numeroActual - numeroPrevio;
                    var lapsoTiempoTS = new TimeSpan(numeroActual - numeroPrevio);

                    try
                    {
                        //FileWriter.WriteVideoFrame(Imagen, lapsoTiempoTS);

                        FileWriter.WriteVideoFrame((Bitmap)newFrameEventArgs.Frame.Clone(), lapsoTiempoTS);

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

            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                if (FileWriter != null && FileWriter.IsOpen)
                {
                    FileWriter.Close();
                    FileWriter.Dispose();
                }


                MiWebCam.SignalToStop();
                MiWebCam.WaitForStop();
                MiWebCam.Stop();
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
                else if (MiWebCam.IsRunning)
                {
                    FileWriter.Close();

                    if (imagenVideo != null)
                    {
                        imageListVideos.Images.Add(nombreArchivoVideo, imagenVideo);
                        listViewIamgenesVideos.Items.Add(nombreArchivoVideo, Path.GetFileName(nombreArchivoVideo), imageListVideos.Images.IndexOfKey(nombreArchivoVideo));
                        listViewIamgenesVideos.Refresh();
                    }

                }
            }
            else
            {
                

                if (MiWebCam != null && MiWebCam.IsRunning && Imagen != null)
                {
                    //saveAvi = new SaveFileDialog();
                    //saveAvi.Filter = "Avi Files (*.avi)|*.avi";
                    //if (saveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                    imagenVideo = (Bitmap)Imagen.Clone();
                    timerRecording.Enabled = true;
                    ticksInicioGrabado = DateTime.Now.Ticks;
                    buttonGrabar.ImageIndex = 0;
                    nombreArchivoVideo = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.avi";
                    numeroPrevio = DateTime.Now.Ticks;
                    int h = MiWebCam.VideoResolution.FrameSize.Height;
                    int w = MiWebCam.VideoResolution.FrameSize.Width;

                    PathVideosPr.Add(nombreArchivoVideo);

                    int bitrate = (MiWebCam.VideoResolution.BitCount/8) * h * w;
                    int fotogramasPorSegundo = MiWebCam.VideoResolution.AverageFrameRate;

                    FileWriter.Open(nombreArchivoVideo, w, h, fotogramasPorSegundo, VideoCodec.Default, bitrate);
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

                string nombreArchivo = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff")}.jpg";

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
                imageListCaptured.Images.Add(nombreArchivo,Imagen);
                
                PathImagenes.Add(nombreArchivo);
                listViewImages.Items.Add(nombreArchivo, Path.GetFileName(nombreArchivo), imageListCaptured.Images.IndexOfKey(nombreArchivo));
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

                if (comboBoxDispositivos.SelectedIndex >= 0 && comboBoxCapabilitis.SelectedIndex >= 0)
                {
                    buttonObtenerVideo.Text = DESCONECTAR;
                    int indice = comboBoxDispositivos.SelectedIndex;
                    string nombreVideo = MisDispositivos[indice].MonikerString;
                    MiWebCam = new VideoCaptureDevice(nombreVideo);
                    MiWebCam.VideoResolution = MiWebCam.VideoCapabilities[comboBoxCapabilitis.SelectedIndex];


                    

                    int alto = MiWebCam.VideoResolution.FrameSize.Height * pictureBox1.Width / MiWebCam.VideoResolution.FrameSize.Width;

                    pictureBox1.Height = alto;

                    if (MiWebCam.AvailableCrossbarVideoInputs.Length > 0)
                    {
                        MiWebCam.CrossbarVideoInput = MiWebCam.AvailableCrossbarVideoInputs[comboBoxInputs.SelectedIndex];
                    }

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

            long ticksActualGrabando = DateTime.Now.Ticks;

            long ticksTiempoGrabacion = ticksActualGrabando - ticksInicioGrabado;

            var tiempoGrabacion = new TimeSpan(ticksTiempoGrabacion);

           labelTiempoGrabacion.Text=$"{tiempoGrabacion.Hours.ToString("D2")}:{tiempoGrabacion.Minutes.ToString("D2")}:{tiempoGrabacion.Seconds.ToString("D2")}";

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


            if (listaSeleccionada != null && listaSeleccionada.Equals("listViewImages"))
            {
                if (listViewImages.SelectedIndices.Count > 0)
                {

                    foreach (ListViewItem item in listViewImages.SelectedItems)
                    {

                        imageListCaptured.Images.RemoveAt(item.ImageIndex);
                        listViewImages.Items.Remove(item);
                        PathImagenesPr.Remove(item.Name);
                        ArchivosEliminadosPr.Add(item.Name);

                    }
                }
                listViewImages.Refresh();
            }
            else if (listaSeleccionada != null && listaSeleccionada.Equals("listViewIamgenesVideos"))
            {
                if (listViewIamgenesVideos.SelectedIndices.Count > 0)
                {

                    foreach (ListViewItem item in listViewIamgenesVideos.SelectedItems)
                    {

                        imageListVideos.Images.RemoveAt(item.ImageIndex);
                        listViewIamgenesVideos.Items.Remove(item);
                        PathVideosPr.Remove(item.Name);
                        ArchivosEliminadosPr.Add(item.Name);

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
