using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Accord.Video.FFMPEG;
using System.Drawing.Imaging;
using System.Reflection;
using static System.Windows.Forms.ImageList;

using Accord.Video;
using Accord.Video.DirectShow;
using System.Threading;
using Accord.Video.VFW;
using System.Linq;

namespace MediaCampturerControlerLib
{
    public partial class UserControlVideoCapturer : UserControl
    {


        public string path { get; set; }

        private List<string> PathImagenesPr;

        private object obj = new object();
        private object obj2 = new object();

        private long ticksInicioGrabado = 0;

        private VideoCodec videoCodecGrabacion = VideoCodec.MPEG4;
        private string contenedorExtencion = "avi";
        private string pathDispositivoDefecto = AppContext.BaseDirectory + "dispositivodefecto.txt";

        public PropiedadesDispositivo DispositivoPorDefecto {
            get {

                var binaryFU = new BinaryFileUtil<PropiedadesDispositivo>();

                var fiDispositivoDefecto = new FileInfo(pathDispositivoDefecto);
                if (!fiDispositivoDefecto.Exists)
                {
                    
                    var propiedadesDefecto = new PropiedadesDispositivo { Entrada="Dispositivo", Formato="HDMI", NombreDispositivo ="Dispositivo"}; 
                    return propiedadesDefecto;
                }
                else
                {
                    return binaryFU.Deserialize(pathDispositivoDefecto);
                }
               
            }

            set {
                var binaryFU = new BinaryFileUtil<PropiedadesDispositivo>();
                var fiDispositivoDefecto = new FileInfo(pathDispositivoDefecto);
                binaryFU.Serialize(value,pathDispositivoDefecto);
                    //File.WriteAllLines(pathDispositivoDefecto, new string[] {value });

            }
        }

        public PropiedadesDispositivo DispositivoPorDefectoMem { get; set; }




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
                        MessageBox.Show(this,"Error, no se pudo cargar uno o varios archivos" + msgerror, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listViewImages.Refresh();
                    LabelCantidadImagenes.Text = listViewImages.Items.Count.ToString();
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
                Parallel.ForEach( PathVideosPr, new ParallelOptions { MaxDegreeOfParallelism=8 }, (pathVideo) =>
                {

                    try
                    {
                        Bitmap imagen = null;

                        var finfo = new FileInfo(pathVideo);

                        if (!finfo.Exists)
                        {
                            throw new Exception("Error");
                        }
                       
                        using (VideoFileReader reader = new VideoFileReader())
                        {
                            reader.Open(pathVideo);

                            var frame= reader.ReadVideoFrame();
                            if (frame != null)
                            {
                                imagen = (Bitmap)reader.ReadVideoFrame().Clone();
                                reader.Close();
                            }
                            else
                            {
                                reader.Close();
                                throw new Exception("Error leyedo archivo");
                            }
                            
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
                labelCntImgVideos.Text = listViewIamgenesVideos.Items.Count.ToString();
                if (error)
                    MessageBox.Show(this,"Error, no se pudo cargar uno o varios archivos. \n" + msgerror, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


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

        private AVIWriter writer = new AVIWriter();

        //private SaveFileDialog saveAvi;
        //private Bitmap Imagen;

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
            
            this.buttonMinimizar.Visible = false;
            buttonMaximizar.Visible = false;
        }


        public UserControlVideoCapturer(string path, string pathDispositivoDefecto)
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

            this.buttonMinimizar.Visible = false;
            buttonMaximizar.Visible = false;
            this.pathDispositivoDefecto = pathDispositivoDefecto+ "dispositivodefecto.txt";
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



            if (comboBoxCapabilitis.Items.Count > 0)
            {

                comboBoxCapabilitis.SelectedIndex = String.IsNullOrEmpty(DispositivoPorDefectoMem.Formato) ? -1 : comboBoxCapabilitis.Items.IndexOf(DispositivoPorDefectoMem.Formato);

                if (comboBoxCapabilitis.SelectedIndex < 0)
                { 

                    //comboBoxDispositivos.Text = MisDispositivos[0].Name;
                    comboBoxCapabilitis.SelectedIndex = 0;
                }

                
            }
            else
            {
                comboBoxCapabilitis.Text = "";
                comboBoxCapabilitis.SelectedIndex = -1;

            }



            //CARGA VIDEO INPUTS DISPONIBLES
            foreach (var input in WebCam.AvailableCrossbarVideoInputs)
            {
                comboBoxInputs.Items.Add(input.Index + "-" + input.Type.ToString());

            }


            if (comboBoxInputs.Items.Count > 0)
            {

                comboBoxInputs.SelectedIndex = String.IsNullOrEmpty(DispositivoPorDefectoMem.Entrada) ? -1:  comboBoxInputs.Items.IndexOf(DispositivoPorDefectoMem.Entrada);

                if (comboBoxInputs.SelectedIndex < 0)
                {
                 
                    //comboBoxDispositivos.Text = MisDispositivos[0].Name;
                    comboBoxInputs.SelectedIndex = 0;
                }

                comboBoxInputs.Enabled = true;
            }
            else
            {
                comboBoxInputs.SelectedIndex = -1;
                comboBoxInputs.Enabled = false;
                comboBoxInputs.Text = "";
            }


            MiWebCam = WebCam;            
        }

        FilterInfoCollection MisDispositivosCompresores;

        public void CargarDispositivos()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            DispositivoPorDefectoMem = DispositivoPorDefecto;

            MisDispositivosCompresores = new FilterInfoCollection(FilterCategory.VideoCompressorCategory);

            if (MisDispositivos != null && MisDispositivos.Count > 0)
            {
                foreach (FilterInfo dispositivo in MisDispositivos)
                {

                    comboBoxDispositivos.Items.Add(dispositivo.Name);
                }


                comboBoxDispositivos.SelectedIndex=  String.IsNullOrEmpty(DispositivoPorDefectoMem.NombreDispositivo) ? -1 :  comboBoxDispositivos.Items.IndexOf(DispositivoPorDefectoMem.NombreDispositivo);

                if (comboBoxDispositivos.SelectedIndex >= 0)
                {
                    comboBoxDispositivos.Text = DispositivoPorDefectoMem.NombreDispositivo;
                }
                else
                {
                    comboBoxDispositivos.Text = MisDispositivos[0].Name;
                    comboBoxDispositivos.SelectedIndex = 0;
                }
                


               
            }

        }

        private long numeroPrevio;
        private long horaInicioGrabacion;

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


                if (newFrameEventArgs.Frame != null)
                {



                  //  Imagen = (Bitmap)newFrameEventArgs.Frame.Clone();

                  // pictureBoxEnUso.Image = Imagen;//(Bitmap)newFrameEventArgs.Frame.Clone();

                    //SI SE ENCUENTRA GRABANDO
                    var lapsoTiempo = numeroActual - numeroPrevio;
                    var lapsoTiempoTS = new TimeSpan(numeroActual - numeroPrevio);

                    if (imagenVideo == null)
                    {
                        imagenVideo = (Bitmap)newFrameEventArgs.Frame.Clone();
                    }

           
                    if (!pararGrabar &&  buttonGrabar.Text == PARAR_GRABAR &&   FileWriter != null && FileWriter.IsOpen)
                    {
                        try
                        {

                            if (lapsoTiempoTS.Milliseconds > 100)
                            {
                                FileWriter.WriteVideoFrame((Bitmap)newFrameEventArgs.Frame.Clone(), lapsoTiempoTS);
                            }
                            else
                            {
                                FileWriter.WriteVideoFrame((Bitmap)newFrameEventArgs.Frame.Clone());
                            }

                            var totalTiempoTS = new TimeSpan(numeroActual - horaInicioGrabacion);

                            if (totalTiempoTS.TotalMilliseconds > 30000)
                            {
                                FileWriter.Flush();
                                horaInicioGrabacion = DateTime.Now.Ticks; ;
                            }



                        }

                        catch (AccessViolationException ex)
                        {
                            FileWriter.WriteVideoFrame(newFrameEventArgs.Frame);
                            MessageBox.Show(this,"Error " + ex);
                        }

                        catch (Exception er)
                        {

                            //FileWriter.WriteVideoFrame(newFrameEventArgs.Frame);
                            // MessageBox.Show(this,this,"Error " + er.Message);
                        }



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
                    FileWriter = null;
                }


                MiWebCam.SignalToStop();
                MiWebCam.WaitForStop();
                MiWebCam.Stop();
                MiWebCam = null;

            }

        }

        private void FormCapturador_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarWebCam();
        }

        bool pararGrabar = false;

        private void buttonGrabarVideo_Click(object sender, EventArgs e)
        {


            if (buttonGrabar.Text == PARAR_GRABAR)  //CUANDO ESTÁ GRABANDO (y se da clic para PARAR DE GRABAR)
            {

                buttonGrabar.Text = GRABAR_VIDEO;
                timerRecording.Stop();


                buttonGrabar.ImageIndex = 1;
                buttonObtenerVideo.Enabled = true;
                if (MiWebCam == null)
                { return; }
                else if (MiWebCam.IsRunning)
                {
                    
                    pararGrabar = true;

                    Thread.Sleep(200);

                    try
                    {
                        FileWriter.Flush();

                        FileWriter.Close();
                        FileWriter.Dispose();
                        FileWriter = null;
                    }
                    catch(Exception er)
                    {
                        MessageBox.Show(this,er.Message);
                    }

                    


                    Task.Factory.StartNew(() =>
                    {
                        listViewIamgenesVideos.Invoke((MethodInvoker)delegate {
                            if (imagenVideo != null)
                            {
                                imageListVideos.Images.Add(nombreArchivoVideo, imagenVideo);
                                listViewIamgenesVideos.Items.Add(nombreArchivoVideo, Path.GetFileName(nombreArchivoVideo), imageListVideos.Images.IndexOfKey(nombreArchivoVideo));
                                listViewIamgenesVideos.Refresh();
                                labelCntImgVideos.Text =  listViewIamgenesVideos.Items.Count.ToString();
                                imagenVideo = null;
                            }

                        });


                    });

                }
            }
            else  //CUANDO ESTA SIN GRABAR Y SE DA CLIC PARA EMPEZAR A GRABAR
            {
                pararGrabar = false;
                if ((MiWebCam == null || !MiWebCam.IsRunning) && comboBoxDispositivos.SelectedIndex >= 0)
                {
                    buttonConectarDispositivo_Click(sender, e);

                }



                if (MiWebCam != null && MiWebCam.IsRunning )//&& Imagen != null)
                {
                    buttonObtenerVideo.Enabled = false;

                    
                    

                    timerRecording.Start();
                    ticksInicioGrabado = DateTime.Now.Ticks;
                    buttonGrabar.ImageIndex = 0;
                    nombreArchivoVideo = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}.{contenedorExtencion}";
                    numeroPrevio = DateTime.Now.Ticks;
                    horaInicioGrabacion = numeroPrevio;
                    int h = MiWebCam.VideoResolution.FrameSize.Height;
                    int w = MiWebCam.VideoResolution.FrameSize.Width;

                    PathVideosPr.Add(nombreArchivoVideo);

                    int bitrate = (MiWebCam.VideoResolution.BitCount) * h * w;
                    int fotogramasPorSegundo = MiWebCam.VideoResolution.AverageFrameRate;

                    try
                    {
                        if (FileWriter != null)
                        {
                            FileWriter.Flush();
                            FileWriter.Close();
                            FileWriter.Dispose();
                            FileWriter = null;
                        }

                        FileWriter = new VideoFileWriter();
                        FileWriter.Open(nombreArchivoVideo, w, h, fotogramasPorSegundo, videoCodecGrabacion, bitrate);

                        buttonGrabar.Text = PARAR_GRABAR;

                    }
                    catch
                    {

                    }




                }
                else
                {
                    MessageBox.Show(this,"No hay conexión a ningún dispositivo de video.", "Conexión" , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }

        }


        private void buttonCapturarImg_Click_1(object sender, EventArgs e)
        {

            if (buttonObtenerVideo.Text == DESCONECTAR)
            {

                string nombreArchivo = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}.jpg";

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write))
                    {

                        var Imagen =  videoSourcePlayerCamera.GetCurrentVideoFrame();
                        if (Imagen != null)
                        {
                            Imagen.Save(memory, ImageFormat.Jpeg);
                            byte[] bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                            imageListCaptured.Images.Add(nombreArchivo, Imagen);
                        }

                    }
                    // MessageBox.Show(this,$"Imagen guardada en {path}");
                }

                //Este codigo causa  "A generic error occurred in GDI+."
                //imagenCapturada.Save(nombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);

                PathImagenes.Add(nombreArchivo);
                listViewImages.Items.Add(nombreArchivo, Path.GetFileName(nombreArchivo), imageListCaptured.Images.IndexOfKey(nombreArchivo));
                LabelCantidadImagenes.Text = listViewImages.Items.Count.ToString();
                listViewImages.Refresh();
            }
            else
            {
                MessageBox.Show(this,"No se encuentra conectado a ningún dispositivo.");
            }

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


                    int alto = MiWebCam.VideoResolution.FrameSize.Height * videoSourcePlayerCamera.Width / MiWebCam.VideoResolution.FrameSize.Width;
                    int diffAlto = alto - videoSourcePlayerCamera.Height;
                    videoSourcePlayerCamera.Height = alto;


                //    listViewIamgenesVideos.Top += diffAlto;

                    labelCntImgVideos.Top = listViewIamgenesVideos.Top - labelCntImgVideos.Height - 2;

                //    listViewImages.Height += diffAlto;


                    if (imageListCaptured.Images.Count == 0)
                    {
                        imageListCaptured.ImageSize = new Size(imageListCaptured.ImageSize.Width, MiWebCam.VideoResolution.FrameSize.Height * imageListCaptured.ImageSize.Width / MiWebCam.VideoResolution.FrameSize.Width);
                    }


                    if (imageListVideos.Images.Count == 0)
                    {
                        imageListVideos.ImageSize = new Size(imageListVideos.ImageSize.Width, MiWebCam.VideoResolution.FrameSize.Height * imageListVideos.ImageSize.Width / MiWebCam.VideoResolution.FrameSize.Width);
                    }







                    if (MiWebCam.AvailableCrossbarVideoInputs.Length > 0)
                    {
                        MiWebCam.CrossbarVideoInput = MiWebCam.AvailableCrossbarVideoInputs[comboBoxInputs.SelectedIndex];
                    }

                    MiWebCam.NewFrame += new NewFrameEventHandler(CapturandoImagen);




                    OpenVideoSource(MiWebCam);

                    // videoSourcePlayerCamera.NewFrame += CapturandoImagen;


                    try
                    {
                        int min, max, step, def;
                        CameraControlFlags ccflags;
                        // VideoProcAmpFlags vpflags;
                        MiWebCam.GetCameraPropertyRange(CameraControlProperty.Exposure, out min, out max, out step, out def, out ccflags);
                    }
                    catch(Exception  ex)
                    {
                        var error= ex.Message;

                    }

                    //guadar dispositivo por defecto
                    this.DispositivoPorDefecto = new PropiedadesDispositivo
                    {
                        NombreDispositivo= comboBoxDispositivos.SelectedItem.ToString(),
                        Entrada = comboBoxInputs.SelectedIndex > -1 ? comboBoxInputs.SelectedItem.ToString() :"",
                        Formato = comboBoxCapabilitis.SelectedIndex > -1 ? comboBoxCapabilitis.SelectedItem.ToString(): "",
                    };


                    buttonObtenerVideo.ImageIndex = 4;

                    buttonMaximizar_Click(sender, e);
                   
                }
                else
                {
                    MessageBox.Show(this,"Debe seleccionar un dispositivo", "Dispositivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                buttonObtenerVideo.Text = "Conectar con dispositivo";
                buttonObtenerVideo.ImageIndex = 3;
                buttonMaximizar.Visible = false;
                if(buttonMinimizar.Visible)
                    buttonMinimizar_Click(sender, e);   
            }


        }


        private void OpenVideoSource(IVideoSource source)
        {
            // set busy cursor
            this.Cursor = Cursors.WaitCursor;

            // stop current video source
            CerrarComponente();

            // start new video source
            
            videoSourcePlayerCamera.VideoSource = source;

            videoSourcePlayerCamera.VideoSource.NewFrame += new NewFrameEventHandler(CapturandoImagen);

            videoSourcePlayerCamera.Start();

            // reset stop watch
            //stopWatch = null;

            // start timer
            //timer.Start();

            this.Cursor = Cursors.Default;
        }




        public void CerrarComponente()
        {
            CloseCurrentVideoSource();
            CerrarWebCam();
        }

        private void CloseCurrentVideoSource()
        {
            if (videoSourcePlayerCamera.VideoSource != null)
            {
                videoSourcePlayerCamera.SignalToStop();

                // wait ~ 3 seconds
                for (int i = 0; i < 30; i++)
                {
                    if (!videoSourcePlayerCamera.IsRunning)
                        break;
                    System.Threading.Thread.Sleep(100);
                }

                if (videoSourcePlayerCamera.IsRunning)
                {
                    videoSourcePlayerCamera.Stop();
                }

                videoSourcePlayerCamera.VideoSource = null;
            }
        }


        private void timerRecording_Tick(object sender, EventArgs e)
        {
            AnimacionTick();
        }

        private void AnimacionTick()
        {


            labelTiempoGrabacion.Invoke((MethodInvoker)delegate
            {

                if (buttonGrabar.ImageIndex == 5)
                    buttonGrabar.ImageIndex = 6;
                else
                    buttonGrabar.ImageIndex = 5;


                long ticksActualGrabando = DateTime.Now.Ticks;

                long ticksTiempoGrabacion = ticksActualGrabando - ticksInicioGrabado;

                var tiempoGrabacion = new TimeSpan(ticksTiempoGrabacion);

                labelTiempoGrabacion.Text = $"{tiempoGrabacion.Hours.ToString("D2")}:{tiempoGrabacion.Minutes.ToString("D2")}:{tiempoGrabacion.Seconds.ToString("D2")}";
            });
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
                    LabelCantidadImagenes.Text = listViewImages.Items.Count.ToString();

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
                    labelCntImgVideos.Text = listViewIamgenesVideos.Items.Count.ToString();
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


                string nombreArchivo = $"{path}\\{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}.jpg";

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write))
                    {
                        imagenDesdeArchivo.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    // MessageBox.Show(this,$"Imagen guardada en {path}");
                }

                //Este codigo causa  "A generic error occurred in GDI+."
                //imagenCapturada.Save(nombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageListCaptured.Images.Add(nombreArchivo, imagenDesdeArchivo);

                PathImagenes.Add(nombreArchivo);
                listViewImages.Items.Add(nombreArchivo, Path.GetFileName(nombreArchivo), imageListCaptured.Images.IndexOfKey(nombreArchivo));
                listViewImages.Refresh();
            }

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

        private Point posicionVideoPlayerCamera;
        private Size sizeVideoPlayerCamera;

        private Point posicionLabelCntImg;
        private Size sizeLabelCntImg;

        private Point posicionLabelCntImgVideos;
        private Size sizeLabelCntImgVideos;

        private void buttonMaximizar_Click(object sender, EventArgs e)
        {
            if(MiWebCam!=null && MiWebCam.VideoResolution != null)
            {
                listViewIamgenesVideos.Visible = false;


                buttonMinimizar.Visible = true;
                buttonMaximizar.Visible = false;



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

                posicionVideoPlayerCamera = videoSourcePlayerCamera.Location;
                sizeVideoPlayerCamera = videoSourcePlayerCamera.Size;

                posicionLabelCntImg = LabelCantidadImagenes.Location;
                sizeLabelCntImg = LabelCantidadImagenes.Size;

                posicionLabelCntImgVideos = labelCntImgVideos.Location;
                sizeLabelCntImgVideos = labelCntImgVideos.Size;

                ModificarPosicionPicture();
            }





        }
        private const int anchoListaImagenes = 230;
        private void ModificarPosicionPicture()
        {

            if (  buttonMinimizar.Visible)
            {

                buttonObtenerVideo.Height = 50;
                buttonObtenerVideo.Top = this.Height - buttonObtenerVideo.Height - 1;
                buttonObtenerVideo.Left = (this.Width - ( buttonObtenerVideo.Width+ labelTiempoGrabacion.Width + buttonGrabar.Width + buttonObtenerVideo.Width + buttonCapturarImg.Width + 30)) / 2;
                buttonObtenerVideo.BringToFront();



                labelTiempoGrabacion.Top = buttonObtenerVideo.Top;
                labelTiempoGrabacion.Left = this.buttonObtenerVideo.Left + this.buttonObtenerVideo.Width + 10;
                labelTiempoGrabacion.BringToFront();



                buttonGrabar.Top = buttonObtenerVideo.Top;
                buttonGrabar.Left = this.labelTiempoGrabacion.Left + this.labelTiempoGrabacion.Width + 10; //(this.Width / 2) - (buttonGrabar.Width) - 5;
                buttonGrabar.BringToFront();
                buttonGrabar.Height = buttonObtenerVideo.Height;

                labelCntImgVideos.Parent = this;
                labelCntImgVideos.Top = buttonGrabar.Top + buttonGrabar.Height - labelCntImgVideos.Height - 3;
                labelCntImgVideos.Left = buttonGrabar.Right - labelCntImgVideos.Width - 3;
                labelCntImgVideos.BringToFront();



                buttonCapturarImg.Top = buttonObtenerVideo.Top;
                buttonCapturarImg.Left = this.buttonGrabar.Left + this.buttonGrabar.Width + 10;  // (this.Width / 2) + 5;
                buttonCapturarImg.BringToFront();
                buttonCapturarImg.Height = buttonGrabar.Height;

                LabelCantidadImagenes.Top = buttonCapturarImg.Top + buttonCapturarImg.Height - LabelCantidadImagenes.Height -3;
                LabelCantidadImagenes.Left = buttonCapturarImg.Right - LabelCantidadImagenes.Width -3 ;
                LabelCantidadImagenes.BringToFront();

                int nuevoAncho = Convert.ToInt32(this.Width);// - anchoListaImagenes);
                int nuevoAlto = MiWebCam.VideoResolution.FrameSize.Height * nuevoAncho / MiWebCam.VideoResolution.FrameSize.Width;

                if (nuevoAlto < buttonGrabar.Top - 2) //- buttonGrabar.Height
                {
                    videoSourcePlayerCamera.Height = nuevoAlto;
                }
                else
                {
                    nuevoAlto = buttonGrabar.Top - 2;
                    nuevoAncho = MiWebCam.VideoResolution.FrameSize.Width * nuevoAlto / MiWebCam.VideoResolution.FrameSize.Height;
                }

                videoSourcePlayerCamera.Size = new Size(nuevoAncho, nuevoAlto);

                if(videoSourcePlayerCamera.Width< this.Width)
                {
                    var diffAncho = this.Width - videoSourcePlayerCamera.Width;
                    videoSourcePlayerCamera.Top = 0;
                    videoSourcePlayerCamera.Left = diffAncho /2;
                }
                else
                {
                    videoSourcePlayerCamera.Top = 0;
                    videoSourcePlayerCamera.Left = 0;
                }
                

                var visibleComponentes = false;
                videoSourcePlayerCamera.Parent = this;


                listViewImages.Parent = this;
                listViewImages.Top = videoSourcePlayerCamera.Top;
                listViewImages.Height = this.Height - buttonCapturarImg.Height - 5;


                listViewImages.Left = videoSourcePlayerCamera.Width;
                listViewImages.Width = anchoListaImagenes - 15;
                listViewImages.BringToFront();


                listViewIamgenesVideos.Parent = this;
                
                CambiarVisibilidadControles(visibleComponentes);

                buttonMinimizar.Left = videoSourcePlayerCamera.Left + videoSourcePlayerCamera.Width - buttonMinimizar.Width;
                buttonMinimizar.Top = videoSourcePlayerCamera.Top;

                buttonMinimizar.BringToFront();

            }
            else
            {
                var distanciaEntreBotones = 10;

                //buttonObtenerVideo.Left = ((this.Width / 2) - ((buttonObtenerVideo.Width + labelTiempoGrabacion.Width + buttonGrabar.Width + buttonCapturarImg.Width + buttonAgregarDesdeArchivo.Width + (4 * distanciaEntreBotones)) / 2));

                //buttonObtenerVideo.BringToFront();

                //labelTiempoGrabacion.Left = this.buttonObtenerVideo.Left + this.buttonObtenerVideo.Width + distanciaEntreBotones;
                //labelTiempoGrabacion.BringToFront();

                //buttonGrabar.Left = this.labelTiempoGrabacion.Left + this.labelTiempoGrabacion.Width + distanciaEntreBotones; //(this.Width / 2) - (buttonGrabar.Width) - 5;
                //buttonGrabar.BringToFront();

                //labelCntImgVideos.Parent = this;
                //labelCntImgVideos.Left = buttonGrabar.Right - labelCntImgVideos.Width - 3;
                //labelCntImgVideos.BringToFront();

                //buttonCapturarImg.Left = this.buttonGrabar.Left + this.buttonGrabar.Width + distanciaEntreBotones;  // (this.Width / 2) + 5;
                //buttonCapturarImg.BringToFront();

                //// LabelCantidadImagenes.Top = buttonCapturarImg.Top + buttonCapturarImg.Height - LabelCantidadImagenes.Height - 3;
                //LabelCantidadImagenes.Left = buttonCapturarImg.Right - LabelCantidadImagenes.Width - 3;
                //LabelCantidadImagenes.BringToFront();

                //this.buttonAgregarDesdeArchivo.Left = buttonCapturarImg.Right + distanciaEntreBotones;

                //buttonMaximizar.BringToFront();
                //buttonMinimizar.BringToFront();



                int nuevoAncho = Convert.ToInt32(panel1.Width - (listViewImages.Width + 4* distanciaEntreBotones));// - anchoListaImagenes);
                int nuevoAlto = listViewIamgenesVideos.Top - (2* distanciaEntreBotones);

                if (MiWebCam != null && MiWebCam.VideoResolution != null)
                {
                    nuevoAlto = MiWebCam.VideoResolution.FrameSize.Height * nuevoAncho / MiWebCam.VideoResolution.FrameSize.Width;

                    if (nuevoAlto < listViewIamgenesVideos.Top - distanciaEntreBotones) //- buttonGrabar.Height
                    {
                        videoSourcePlayerCamera.Height = nuevoAlto;
                    }
                    else
                    {
                        nuevoAlto = listViewIamgenesVideos.Top - (2*distanciaEntreBotones);
                        nuevoAncho = MiWebCam.VideoResolution.FrameSize.Width * nuevoAlto / MiWebCam.VideoResolution.FrameSize.Height;
                    }
                }



                videoSourcePlayerCamera.Size = new Size(nuevoAncho, nuevoAlto);
                buttonMaximizar.Visible = true;
                buttonMaximizar.Left = videoSourcePlayerCamera.Right - buttonMaximizar.Width-2;
                buttonMaximizar.Top = panel1.Top+2;

                //if (videoSourcePlayerCamera.Width < this.Width)
                //{
                //    var diffAncho = this.Width - videoSourcePlayerCamera.Width;
                //    videoSourcePlayerCamera.Top = 0;
                //    videoSourcePlayerCamera.Left = diffAncho / 2;
                //}
                //else
                //{
                //    videoSourcePlayerCamera.Top = 0;
                //    videoSourcePlayerCamera.Left = 0;
                //}


                //var visibleComponentes = false;
                //videoSourcePlayerCamera.Parent = this;




                //listViewImages.Parent = this;
                //listViewImages.Top = videoSourcePlayerCamera.Top;
                //listViewImages.Height = this.Height - buttonCapturarImg.Height - 5;


                //listViewImages.Left = videoSourcePlayerCamera.Width;
                //listViewImages.Width = anchoListaImagenes - 15;
                //listViewImages.BringToFront();


                //listViewIamgenesVideos.Parent = this;

                //CambiarVisibilidadControles(visibleComponentes);

                //buttonMinimizar.Left = videoSourcePlayerCamera.Left + videoSourcePlayerCamera.Width - buttonMinimizar.Width;
                //buttonMinimizar.Top = videoSourcePlayerCamera.Top;

                //buttonMinimizar.BringToFront();
            }




        }

        private void CambiarVisibilidadControles(bool visibleComponentes)
        {
            listViewImages.Visible = visibleComponentes;
            label1.Visible = visibleComponentes;
            comboBoxDispositivos.Visible = visibleComponentes;
            labelInput.Visible = visibleComponentes;
            comboBoxInputs.Visible = visibleComponentes;
            labelCalidad.Visible = visibleComponentes;
            comboBoxCapabilitis.Visible = visibleComponentes;

            buttonAgregarDesdeArchivo.Visible = visibleComponentes;
            panel1.Visible = visibleComponentes;

            videoSourcePlayerCamera.Focus();
        }

        private void buttonMinimizar_Click(object sender, EventArgs e)
        {

            listViewIamgenesVideos.Parent = panel1;
            listViewIamgenesVideos.Visible = true;


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


            listViewImages.Refresh();
            listViewIamgenesVideos.Refresh();

            videoSourcePlayerCamera.Parent = panel1;

            videoSourcePlayerCamera.Size = sizeVideoPlayerCamera;
            videoSourcePlayerCamera.Location = posicionVideoPlayerCamera;

            LabelCantidadImagenes.Location = posicionLabelCntImg;
            LabelCantidadImagenes.Size = sizeLabelCntImg;

            labelCntImgVideos.Parent = panel1;
            labelCntImgVideos.Location = posicionLabelCntImgVideos;
            labelCntImgVideos.Size = sizeLabelCntImgVideos;

            videoSourcePlayerCamera.Focus();

            ModificarPosicionPicture();

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
            var formVideoPlayerVlc = new VideoPlayerVlc(listViewIamgenesVideos.Items[listViewIamgenesVideos.SelectedIndices[0]].Name, path);
            formVideoPlayerVlc.ImagenesSeleccionadasDeVideo += ImagenesSeleccionadasDeVideo;
            formVideoPlayerVlc.ShowDialog(this);


            //var formVideoPlayer = new VideoPlayer(listViewIamgenesVideos.Items[listViewIamgenesVideos.SelectedIndices[0]].Name, path);
            //formVideoPlayer.ImagenesSeleccionadasDeVideo += ImagenesSeleccionadasDeVideo;
            //formVideoPlayer.ShowDialog(this);

        }

        private void ImagenesSeleccionadasDeVideo(object sender, ImagenesSeleccionadasEventArgs e)
        {
            if (e.PathImagenes != null && e.PathImagenes.Count > 0)
            {
                foreach (var pathImagen in e.PathImagenes)
                {
                    listViewImages.Invoke((MethodInvoker)delegate
                    {
                        var imagenDeVideo = (Bitmap)new Bitmap(pathImagen).Clone();
                        imageListCaptured.Images.Add(pathImagen, imagenDeVideo);

                        PathImagenes.Add(pathImagen);
                        listViewImages.Items.Add(pathImagen, Path.GetFileName(pathImagen), imageListCaptured.Images.IndexOfKey(pathImagen));
                    });
                }

                listViewImages.Invoke((MethodInvoker)delegate
                {
                    listViewImages.Refresh();
                });
            }

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

        private void UserControlVideoCapturer_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (e.KeyChar == (char)Keys.Return)
            {
                CapturarImagen();
            }
            else if (e.KeyChar == (char)Keys.Space)
            {
                IniciarPararGrabacion();
            }

        }

        private void buttonProperties_Click(object sender, EventArgs e)
        {
            try
            {
                MiWebCam.DisplayPropertyPage(IntPtr.Zero);
            }
            catch (Exception er)
            {
                MessageBox.Show(this,er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
