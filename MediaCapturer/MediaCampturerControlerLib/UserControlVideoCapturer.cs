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
using System.Reflection;
using static System.Windows.Forms.ImageList;

namespace MediaCampturerControlerLib
{
    public partial class UserControlVideoCapturer : UserControl
    {


        public string path { get; set; }

        private List<string> PathImagenesPr;

        private object obj = new object();
        private object obj2 = new object();

        private long ticksInicioGrabado = 0;

        private PictureBox pictureBoxEnUso;


        public List<string> PathImagenes
        {
            get
            {
                if (PathImagenesPr == null)
                {
                    PathImagenesPr = new List<string>();
                }
                return PathImagenesPr;
            }
            set
            {

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
                            imagenCapt = (Bitmap)new Bitmap(pathImagen).Clone();

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
                        catch (Exception er)
                        {
                            error = true;

                            msgerror += "\n" + er.Message;
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
            set
            {

                PathVideosPr = value;
                var error = false;
                string msgerror = "";
                List<ListViewItem> listaViewItems = new List<ListViewItem>();
                Parallel.ForEach(PathVideosPr, (pathVideo) =>
                {

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
                            listaViewItems.Add(listViewIte);
                            //listViewIamgenesVideos.Items.Add(listViewIte);
                        }


                    }
                    catch (Exception er)
                    {
                        //error = true;
                        msgerror += msgerror + "\n";

                        Assembly myAssembly = Assembly.GetExecutingAssembly();
                        Stream myStream = myAssembly.GetManifestResourceStream("MediaCampturerControlerLib.imagenVideoDefault.jpg");
                        Bitmap imagen = new Bitmap(myStream);

                        lock (obj)
                        {
                            imageListVideos.Images.Add(pathVideo, imagen);
                            var listViewIte = new ListViewItem()
                            {
                                Name = pathVideo,
                                Text = Path.GetFileName(pathVideo),
                                ImageIndex = imageListVideos.Images.IndexOfKey(pathVideo),
                            };
                            listaViewItems.Add(listViewIte);
                            //listViewIamgenesVideos.Items.Add(listViewIte);
                        }
                    }
                });
                listViewIamgenesVideos.Items.AddRange(listaViewItems.ToArray());
                if (error)
                    MessageBox.Show("Error, no se pudo cargar uno o varios archivos. \n" + msgerror, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


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
            this.pictureBoxEnUso = this.pictureBox1;
            this.buttonMinimizar.Visible = false;
            buttonMaximizar.Visible = false;
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
            this.pictureBoxEnUso = this.pictureBox1;
            this.buttonMinimizar.Visible = false;
            buttonMaximizar.Visible = false;
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
                comboBoxInputs.Items.Add(input.Index + "-" + input.Type.ToString());

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

                pictureBoxEnUso.Image = Imagen;//(Bitmap)newFrameEventArgs.Frame.Clone();

                //SI SE ENCUENTRA GRABANDO
                if (buttonGrabar.Text == PARAR_GRABAR && FileWriter != null)
                {
                    var lapsoTiempo = numeroActual - numeroPrevio;
                    var lapsoTiempoTS = new TimeSpan(numeroActual - numeroPrevio);

                    try
                    {
                        //FileWriter.WriteVideoFrame(Imagen, lapsoTiempoTS);

                        FileWriter.WriteVideoFrame(newFrameEventArgs.Frame);//, lapsoTiempoTS);

                    }
                    catch (Exception er)
                    {

                        FileWriter.WriteVideoFrame(newFrameEventArgs.Frame);
                        MessageBox.Show("Error " + er.Message);
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
                pictureBoxEnUso.Image = null;
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
                //if(this.pictureBox1.Name == this.pictureBoxEnUso.Name)
                //{
                buttonGrabar.Text = GRABAR_VIDEO;
                //}

                buttonGrabar.ImageIndex = 1;
                buttonObtenerVideo.Enabled = true;
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
                    buttonObtenerVideo.Enabled = false;

                    //saveAvi = new SaveFileDialog();
                    //saveAvi.Filter = "Avi Files (*.avi)|*.avi";
                    //if (saveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{
                    imagenVideo = (Bitmap)Imagen.Clone();
                    timerRecording.Enabled = true;
                    ticksInicioGrabado = DateTime.Now.Ticks;
                    buttonGrabar.ImageIndex = 0;
                    nombreArchivoVideo = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff")}.avi";
                    numeroPrevio = DateTime.Now.Ticks;
                    int h = MiWebCam.VideoResolution.FrameSize.Height;
                    int w = MiWebCam.VideoResolution.FrameSize.Width;

                    PathVideosPr.Add(nombreArchivoVideo);

                    int bitrate = (MiWebCam.VideoResolution.BitCount) * h * w;
                    int fotogramasPorSegundo = MiWebCam.VideoResolution.AverageFrameRate;

                    FileWriter.Open(nombreArchivoVideo, w, h, fotogramasPorSegundo, VideoCodec.Default, bitrate);
                    FileWriter.WriteVideoFrame(Imagen);

                    //if (this.pictureBox1.Name == this.pictureBoxEnUso.Name)
                    //{
                    buttonGrabar.Text = PARAR_GRABAR;
                    //}
                    //}
                }
                else
                {
                    MessageBox.Show("No hay conexión a ningún dispositivo de video.");
                }

            }
            this.pictureBoxEnUso.Focus();
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

                imageListCaptured.Images.Add(nombreArchivo, Imagen);

                PathImagenes.Add(nombreArchivo);
                listViewImages.Items.Add(nombreArchivo, Path.GetFileName(nombreArchivo), imageListCaptured.Images.IndexOfKey(nombreArchivo));
                listViewImages.Refresh();
            }
            else
            {
                MessageBox.Show("No se encuentra conectado a ningún dispositivo.");
            }
            this.pictureBoxEnUso.Focus();
        }

        

        private void buttonConectarDispositivo_Click(object sender, EventArgs e)
        {
            CerrarWebCam();
            if (buttonObtenerVideo.Text != DESCONECTAR)
            {

                if (comboBoxDispositivos.SelectedIndex >= 0 && comboBoxCapabilitis.SelectedIndex >= 0)
                {
                    buttonMaximizar.Visible = true;

                    buttonObtenerVideo.Text = DESCONECTAR;
                    int indice = comboBoxDispositivos.SelectedIndex;
                    string nombreVideo = MisDispositivos[indice].MonikerString;
                    MiWebCam = new VideoCaptureDevice(nombreVideo);
                    MiWebCam.VideoResolution = MiWebCam.VideoCapabilities[comboBoxCapabilitis.SelectedIndex];

                    int alto = MiWebCam.VideoResolution.FrameSize.Height * pictureBox1.Width / MiWebCam.VideoResolution.FrameSize.Width;
                    int diffAlto = alto - pictureBox1.Height;
                    pictureBox1.Height = alto;


                    listViewIamgenesVideos.Top += diffAlto;

                    listViewImages.Height += diffAlto;


                    if(imageListCaptured.Images.Count == 0)
                    {
                        imageListCaptured.ImageSize = new Size(imageListCaptured.ImageSize.Width, MiWebCam.VideoResolution.FrameSize.Height * imageListCaptured.ImageSize.Width / MiWebCam.VideoResolution.FrameSize.Width);
                    }


                    if (imageListVideos.Images.Count == 0)
                    {
                        imageListVideos.ImageSize = new Size(imageListVideos.ImageSize.Width, MiWebCam.VideoResolution.FrameSize.Height * imageListVideos.ImageSize.Width / MiWebCam.VideoResolution.FrameSize.Width);
                    }
                    

                    


                    listViewImages.Refresh();

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
                    MessageBox.Show("Debe seleccionar un dispositivo", "Dispositivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                buttonObtenerVideo.Text = "Conectar con dispositivo";
                buttonObtenerVideo.ImageIndex = 3;
                buttonMaximizar.Visible = false;
            }
            this.pictureBoxEnUso.Focus();
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

            labelTiempoGrabacion.Text = $"{tiempoGrabacion.Hours.ToString("D2")}:{tiempoGrabacion.Minutes.ToString("D2")}:{tiempoGrabacion.Seconds.ToString("D2")}";

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

        private VideoFileReader reader;
        private int frameIndex;
        private int maxCacheImagenes = 200;
        private List<Bitmap> listaBitmap = new List<Bitmap>();




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


        private void timerPlaying_Tick(object sender, EventArgs e)
        {
            if (reader != null && reader.IsOpen)
            {
                var frame = reader.ReadVideoFrame();

                if (frame != null)
                {
                    pictureBoxEnUso.Image = (Bitmap)frame.Clone();

                    if (listaBitmap.Count >= maxCacheImagenes)
                    {
                        listaBitmap.RemoveAt(0);
                    }

                    listaBitmap.Add((Bitmap)pictureBoxEnUso.Image);
                    frameIndex++;

                }
                else
                {
                    timerPlaying.Stop();
                    //reader.Close();
                    //reader.Open(listViewIamgenesVideos.Items[listViewIamgenesVideos.SelectedIndices[0]].Name);

                    //pictureBoxEnUso.Image = (Bitmap)reader.ReadVideoFrame();
                    //frameIndex = 0;


                    listaBitmap = new List<Bitmap>();
                }
                trackBar1.Value = frameIndex;

            }


        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            timerPlaying.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timerPlaying.Stop();
        }

        private void buttonRetroceder_Click(object sender, EventArgs e)
        {

            timerPlaying.Stop();
            frameIndex = frameIndex - Convert.ToInt32(reader.FrameRate.Value * 2);

            if (frameIndex < 0)
            {
                frameIndex = 0;
            }


            reader.Close();
            reader.Open(listViewIamgenesVideos.Items[listViewIamgenesVideos.SelectedIndices[0]].Name);

            for (int index = 0; index <= frameIndex; index++)
            {
                reader.ReadVideoFrame();
            }

            pictureBoxEnUso.Image = (Bitmap)reader.ReadVideoFrame(frameIndex).Clone();
            trackBar1.Value = frameIndex;
        }

        private void buttonAdelantar_Click(object sender, EventArgs e)
        {

            timerPlaying.Stop();

            if(reader!=null && reader.IsOpen)
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
                    pictureBoxEnUso.Image = (Bitmap)reader.ReadVideoFrame().Clone();
                }

                trackBar1.Value = frameIndex;
            }

        }



        // New frame event handler, which is invoked on each new available video frame
        private void video_NewFrame(object sender, Accord.Video.NewFrameEventArgs eventArgs)
        {
            // get new frame
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone(); ;
            pictureBoxEnUso.Image = bitmap;
            // process the frame
        }



        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (listaSeleccionada != null && listaSeleccionada.Equals("listViewImages"))
            {
                if (listViewImages.SelectedIndices.Count > 0)
                {
                    List<ListViewItem> listaEliminar = new List<ListViewItem>();
                    foreach (ListViewItem item in listViewImages.SelectedItems)
                    {

                        // imageListCaptured.Images.RemoveAt( item.ImageIndex);
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

                        //  imageListVideos.Images.RemoveAt(item.ImageIndex);
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

        private void buttonAgregarDesdeArchivo_Click(object sender, EventArgs e)
        {
            if (openFileDialogImagen.ShowDialog() == DialogResult.OK)
            {
                string fileExtention = Path.GetExtension(openFileDialogImagen.FileName);

                Bitmap imagenDesdeArchivo = new Bitmap(openFileDialogImagen.OpenFile());


                string nombreArchivo = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff")}.jpg";

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write))
                    {
                        imagenDesdeArchivo.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    // MessageBox.Show($"Imagen guardada en {path}");
                }

                //Este codigo causa  "A generic error occurred in GDI+."
                //imagenCapturada.Save(nombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageListCaptured.Images.Add(nombreArchivo, imagenDesdeArchivo);

                PathImagenes.Add(nombreArchivo);
                listViewImages.Items.Add(nombreArchivo, Path.GetFileName(nombreArchivo), imageListCaptured.Images.IndexOfKey(nombreArchivo));
                listViewImages.Refresh();
            }
            this.pictureBoxEnUso.Focus();

        }

        public Boolean SavingVideo()
        {
            return !buttonObtenerVideo.Enabled;
        }

        public void CapturarImagen()
        {
            buttonCapturarImg_Click_1(null, null);
        }

        public void IniciarPararGrabacion()
        {
            buttonGrabarVideo_Click(null, null);
        }

        private Point posicionInicialBtnGrabar;
        private Size sizeInicialBtnGrabar;

        private Point posicionInicialBtnCapturarImg;
        private Size sizeInicialBtnCapturarImg;

        private Point posicionInicialListViewImagenes;
        private Size sizeInicialListViewImagenes;

        private Point posicionCronometroRegistro;
        private Size sizeInicialCronometroRegistro;

        private Point posicionBotonObtenerVideo;
        private Size sizeBotonObtenerVideo;

        private void buttonMaximizar_Click(object sender, EventArgs e)
        {

            pictureBox1.Visible = false;
            listViewIamgenesVideos.Visible = false;

            buttonMinimizar.Visible = true;
            pictureBoxMaximizado.Visible = true;


            posicionInicialBtnGrabar = buttonGrabar.Location;
            sizeInicialBtnGrabar = buttonGrabar.Size;

            posicionInicialBtnCapturarImg = buttonCapturarImg.Location;
            sizeInicialBtnCapturarImg = buttonCapturarImg.Size;

            posicionInicialListViewImagenes = listViewImages.Location;
            sizeInicialListViewImagenes = listViewImages.Size;

            posicionCronometroRegistro = labelTiempoGrabacion.Location;
            sizeInicialCronometroRegistro = labelTiempoGrabacion.Size;

            posicionBotonObtenerVideo = buttonObtenerVideo.Location;
            sizeBotonObtenerVideo = buttonObtenerVideo.Size;

            ModificarPosicionPicture();

            

        }
        private const int anchoListaImagenes = 230;
        private void ModificarPosicionPicture()
        {
            
            if (pictureBoxMaximizado.Visible && buttonMinimizar.Visible)
            {
                pictureBoxEnUso = pictureBoxMaximizado;

                //MiWebCam.VideoResolution.FrameSize.Width

                int nuevoAncho = Convert.ToInt32(this.Width - anchoListaImagenes);
                int nuevoAlto = MiWebCam.VideoResolution.FrameSize.Height * nuevoAncho / MiWebCam.VideoResolution.FrameSize.Width;

                if (nuevoAlto < this.Height - buttonGrabar.Height - 10)
                {
                    pictureBoxEnUso.Height = nuevoAlto;
                }
                else
                {
                    nuevoAlto = this.Height - buttonGrabar.Height - 10;
                    nuevoAncho = MiWebCam.VideoResolution.FrameSize.Width * nuevoAlto / MiWebCam.VideoResolution.FrameSize.Height;
                }

                pictureBoxEnUso.Size = new Size(nuevoAncho, nuevoAlto);

                pictureBoxEnUso.Top = 0;
                pictureBoxEnUso.Left = 0;


                listViewImages.Parent = this;
                listViewImages.Top = pictureBoxEnUso.Top;
                listViewImages.Height = this.Height - buttonCapturarImg.Height - 5;


                listViewImages.Left = pictureBoxEnUso.Width;
                listViewImages.Width = anchoListaImagenes - 15;
                listViewImages.BringToFront();



                buttonObtenerVideo.Top = this.Height - buttonCapturarImg.Height - 5;
                buttonObtenerVideo.Left = 180 + 10;
                buttonObtenerVideo.BringToFront();


                labelTiempoGrabacion.Top = this.Height - (buttonCapturarImg.Height / 2);
                labelTiempoGrabacion.Left = this.buttonObtenerVideo.Left + this.buttonObtenerVideo.Width + 10;
                labelTiempoGrabacion.BringToFront();


                buttonGrabar.Top = this.Height - buttonGrabar.Height - 5;
                buttonGrabar.Left = this.labelTiempoGrabacion.Left + this.labelTiempoGrabacion.Width + 10; //(this.Width / 2) - (buttonGrabar.Width) - 5;
                buttonGrabar.BringToFront();


                buttonCapturarImg.Top = this.Height - buttonCapturarImg.Height - 5;
                buttonCapturarImg.Left = this.buttonGrabar.Left + this.buttonGrabar.Width + 10;  // (this.Width / 2) + 5;
                buttonCapturarImg.BringToFront();



                buttonMinimizar.Left = pictureBoxMaximizado.Width - buttonMinimizar.Width;
                buttonMinimizar.Top = pictureBoxMaximizado.Top;

                buttonMinimizar.BringToFront();


                var visibleComponentes = false;

                CambiarVisibilidadControles(visibleComponentes);
                //buttonObtenerVideo
                //labelTiempoGrabacion
                //buttonGrabar
                //buttonCapturarImg
                //buttonAgregarDesdeArchivo


            }


        }

        private void CambiarVisibilidadControles(bool visibleComponentes)
        {
            label1.Visible = visibleComponentes;
            comboBoxDispositivos.Visible = visibleComponentes;
            labelInput.Visible = visibleComponentes;
            comboBoxInputs.Visible = visibleComponentes;
            labelCalidad.Visible = visibleComponentes;
            comboBoxCapabilitis.Visible = visibleComponentes;
        }

        private void buttonMinimizar_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            listViewIamgenesVideos.Visible = true;

            pictureBoxEnUso = pictureBox1;

            pictureBoxMaximizado.Visible = false;
            buttonMinimizar.Visible = false;
            buttonGrabar.Location = posicionInicialBtnGrabar;
            buttonGrabar.Size = sizeInicialBtnGrabar;


            buttonCapturarImg.Location = posicionInicialBtnCapturarImg;
            buttonCapturarImg.Size = sizeInicialBtnCapturarImg;
            listViewImages.Parent = panel1;
            listViewImages.Size = sizeInicialListViewImagenes;
            posicionInicialListViewImagenes.Y = 0;
            listViewImages.Location = posicionInicialListViewImagenes;

            labelTiempoGrabacion.Location = posicionCronometroRegistro;
            labelTiempoGrabacion.Size = sizeInicialCronometroRegistro;

            buttonObtenerVideo.Location = posicionBotonObtenerVideo;
            buttonObtenerVideo.Size = sizeBotonObtenerVideo;

            CambiarVisibilidadControles(true);


        }

        private void trackBar1_TabIndexChanged(object sender, EventArgs e)
        {
            // pictureBoxEnUso.Image=(Bitmap) reader.ReadVideoFrame(trackBar1.Value).Clone();
        }

        private void trackBar1_MouseLeave(object sender, EventArgs e)
        {
            //  pictureBoxEnUso.Image = (Bitmap)reader.ReadVideoFrame(trackBar1.Value).Clone();
        }



        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {


            // buttonRetroceder_Click(sender, e);

            // pictureBoxEnUso.Image = (Bitmap)reader.ReadVideoFrame(trackBar1.Value).Clone();
            //Task.Factory.StartNew(()=> pictureBoxEnUso.Image = (Bitmap)reader.ReadVideoFrame(trackBar1.Value).Clone() );
        }

        private void obtenerFotoDeVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var formVideoPlayer = new VideoPlayer(listViewIamgenesVideos.Items[listViewIamgenesVideos.SelectedIndices[0]].Name,path);
            formVideoPlayer.ImagenesSeleccionadasDeVideo += ImagenesSeleccionadasDeVideo;
            formVideoPlayer.ShowDialog(this);
            
        }

        private void ImagenesSeleccionadasDeVideo(object sender, ImagenesSeleccionadasEventArgs e)
        {
            if(e.PathImagenes!=null && e.PathImagenes.Count > 0)
            {
                foreach (var pathImagen in e.PathImagenes)
                {
                   var imagenDeVideo = (Bitmap)new Bitmap(pathImagen).Clone();
                    imageListCaptured.Images.Add(pathImagen, imagenDeVideo);

                    PathImagenes.Add(pathImagen);
                    listViewImages.Items.Add(pathImagen, Path.GetFileName(pathImagen), imageListCaptured.Images.IndexOfKey(pathImagen));
                }


                listViewImages.Refresh();
            }

        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            frameIndex = trackBar1.Value;
            reader.Close();
            reader.Open(listViewIamgenesVideos.Items[listViewIamgenesVideos.SelectedIndices[0]].Name);

            for (int index = 0; index <= frameIndex; index++)
            {
                reader.ReadVideoFrame();
            }
            pictureBoxEnUso.Image = (Bitmap)reader.ReadVideoFrame();


        }

        private void UserControlVideoCapturer_SizeChanged(object sender, EventArgs e)
        {
            ModificarPosicionPicture();
        }

        private void listViewImages_MouseEnter(object sender, EventArgs e)
        {
            obtenerFotoDeVideoToolStripMenuItem.Visible = false;
        }



        private void listViewIamgenesVideos_MouseEnter(object sender, EventArgs e)
        {
            obtenerFotoDeVideoToolStripMenuItem.Visible = true;
        }
    }
}
