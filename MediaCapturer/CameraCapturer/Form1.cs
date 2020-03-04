using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Drawing;
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
            long numeroActual = DateTime.Now.Ticks;
            Imagen = (Bitmap)newFrameEventArgs.Frame.Clone();
            pictureBox1.Image = (Bitmap)newFrameEventArgs.Frame.Clone();

            //SI SE ENCUENTRA GRABANDO
            if (buttonGrabar.Text == PARAR_GRABAR)
            {
                var lapsoTiempo = numeroActual - numeroPrevio;
                var lapsoTiempoTS = new TimeSpan(numeroActual - numeroPrevio);

                //if (lapsoTiempoTS.TotalSeconds > 1)
                //{
                //    FileWriter.WriteVideoFrame(Imagen, lapsoTiempoTS);
                //}
                //else
                //{
                    FileWriter.WriteVideoFrame(Imagen);
                //}

                
            }
           // numeroPrevio = numeroActual;
        }

        private void CerrarWebCam()
        {
            buttonGrabar.Text = GRABAR_VIDEO;
            Task.Delay(100); 
            if (MiWebCam!=null && MiWebCam.IsRunning)
            {
                if(FileWriter!=null && FileWriter.IsOpen)
                {
                    FileWriter.Close();
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
                    saveAvi = new SaveFileDialog();
                    saveAvi.Filter = "Avi Files (*.avi)|*.avi";
                    if (saveAvi.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        numeroPrevio = DateTime.Now.Ticks;
                        int h = MiWebCam.VideoResolution.FrameSize.Height;
                        int w = MiWebCam.VideoResolution.FrameSize.Width;
                        FileWriter.Open(saveAvi.FileName, w, h, 25, VideoCodec.Default, 5000000);
                        FileWriter.WriteVideoFrame(Imagen);
                        buttonGrabar.Text = PARAR_GRABAR;

                    }
                }
                else
                {
                    MessageBox.Show("No hay conexión a ningún dispositivo de video.");
                }

            }
        }

        private void buttonCapturarImg_Click_1(object sender, EventArgs e)
        {
            Bitmap imagenCapturada= Imagen;
            string nombreArchivo = $"{path}{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}.jpg";
            imagenCapturada.Save(nombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);
            imageListCaptured.Images.Add(imagenCapturada);
            listViewImages.Refresh();
            listViewImages.Items.Add(nombreArchivo, listViewImages.Items.Count);

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
                    MessageBox.Show("Debe seleccionar un dispositivo");
                }

            }
            else
            {
                buttonObtenerVideo.Text = "Conectar con dispositivo";
            }

        }
    }
}
