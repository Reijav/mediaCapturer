using Accord.Video;
using Accord.Video.DirectShow;
using Accord.Video.FFMPEG;

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CameraCapturer
{
    public partial class FormCapturador : Form
    {

        private string path = @"C:\temp\capturador\";
        private FilterInfoCollection MisDispositivos;
        private VideoCaptureDevice MiWebCam;


        private VideoFileWriter FileWriter = new VideoFileWriter();
        private SaveFileDialog saveAvi;
        private Bitmap Imagen;

        private const string PARAR_GRABAR = "Parar Grabación";
        private const string GRABAR_VIDEO = "Grabar Video";
        private const string DESCONECTAR = "Desconectar de Dispositivo";

        public FormCapturador()
        {
            InitializeComponent();
        }

        public FormCapturador(string path)
        {
            InitializeComponent();
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            if (!dirinfo.Exists)
            {
                dirinfo.Create();
            }

            this.path = path;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarDispositivos();
        }

        public void CargarDispositivos()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            if(MisDispositivos!=null && MisDispositivos.Count > 0)
            {
                foreach(FilterInfo dispositivo in MisDispositivos)
                {
                    
                    comboBoxDispositivos.Items.Add(dispositivo.Name);
                }
                comboBoxDispositivos.Text = MisDispositivos[0].Name;
            }

        }

        private long numeroPrevio ;

        private void CapturandoImagen(object sender , NewFrameEventArgs newFrameEventArgs)
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {

                long numeroActual = DateTime.Now.Ticks;
                Imagen = (Bitmap)newFrameEventArgs.Frame.Clone();

                //System.Drawing.Imaging.BitmapData bmpData =
                //Imagen.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                //Imagen.PixelFormat);


                pictureBox1.Image = (Bitmap)newFrameEventArgs.Frame.Clone();

                //SI SE ENCUENTRA GRABANDO
                if (buttonGrabar.Text == PARAR_GRABAR && FileWriter!=null)
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
                    catch(Exception er)
                    {
                        
                        FileWriter.WriteVideoFrame((Bitmap)newFrameEventArgs.Frame.Clone());
                    }

                }
            }
           // numeroPrevio = numeroActual;
        }

        private void CerrarWebCam()
        {
            if (buttonGrabar.Text == PARAR_GRABAR)
            {
                buttonGrabarVideo_Click(this,null);
            }


                Task.Delay(100); 
            if (MiWebCam!=null && MiWebCam.IsRunning)
            {
                if(FileWriter!=null && FileWriter.IsOpen)
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
                buttonGrabar.Text = GRABAR_VIDEO;
                if (MiWebCam == null)
                { return; }
                if (MiWebCam.IsRunning)
                {
                    //this.FinalVideo.Stop();
                    FileWriter.Close();
                    //this.AVIwriter.Close();
                   // pictureBox1.Image = null;
                }
            }
            else
            {
                if(MiWebCam!=null && MiWebCam.IsRunning)
                {
                    //saveAvi = new SaveFileDialog();
                    //saveAvi.Filter = "Avi Files (*.avi)|*.avi";
                    //if (saveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    //{

                    var nombreArchivo = $"{path}{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.avi";
                        numeroPrevio = DateTime.Now.Ticks;
                        int h = MiWebCam.VideoResolution.FrameSize.Height;
                        int w = MiWebCam.VideoResolution.FrameSize.Width;
                        FileWriter.Open(nombreArchivo, w, h, 25, VideoCodec.Default, 5000000);
                        FileWriter.WriteVideoFrame(Imagen);
                        buttonGrabar.Text = PARAR_GRABAR;

                    //}
                }
                else
                {
                    MessageBox.Show(this,"No hay conexión a ningún dispositivo de video.");
                }

            }
        }

        private void buttonCapturarImg_Click_1(object sender, EventArgs e)
        {

            if (buttonObtenerVideo.Text == DESCONECTAR)
            {
                Bitmap imagenCapturada = (Bitmap)Imagen.Clone();
                string nombreArchivo = $"{path}{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.jpg";

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write))
                    {
                        imagenCapturada.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                   // MessageBox.Show(this,$"Imagen guardada en {path}");
                }

                //Este codigo causa  "A generic error occurred in GDI+."
                //imagenCapturada.Save(nombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageListCaptured.Images.Add(imagenCapturada);
                listViewImages.Refresh();
                listViewImages.Items.Add(nombreArchivo, listViewImages.Items.Count);
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
                if (comboBoxDispositivos.SelectedIndex >= 0)
                {
                    buttonObtenerVideo.Text = DESCONECTAR;
                    int indice = comboBoxDispositivos.SelectedIndex;
                    string nombreVideo = MisDispositivos[indice].MonikerString;
                    MiWebCam = new VideoCaptureDevice(nombreVideo);
                    MiWebCam.VideoResolution = MiWebCam.VideoCapabilities[0];
                    MiWebCam.NewFrame += new NewFrameEventHandler(CapturandoImagen);
                    MiWebCam.Start();
                }
                else
                {
                    MessageBox.Show(this,"Debe seleccionar un dispositivo");
                }

            }
            else
            {
                buttonObtenerVideo.Text = "Conectar con dispositivo";
            }

        }
    }
}
