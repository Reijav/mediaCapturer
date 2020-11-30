using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private int frameIndex;
        private Bitmap Imagen;
        public event EventHandler<ImagenesSeleccionadasEventArgs> ImagenesSeleccionadasDeVideo;

        public VideoPlayer(string pathVideo, string pathBaseImagenes)
        {
            InitializeComponent();

            reader = new VideoFileReader();

            this.PathVideo = pathVideo;
            this.PathBaseImagenes = pathBaseImagenes;

            reader.Open(PathVideo);
            trackBar1.Maximum =Convert.ToInt32( reader.FrameCount.ToString());

            timerPlayer.Interval =Convert.ToInt32(1000 / reader.FrameRate.Value );

            pictureBoxPlayer.Image = (Bitmap)reader.ReadVideoFrame().Clone();
            frameIndex = 0;

            imageListCapturas.ImageSize = new Size(imageListCapturas.ImageSize.Width, reader.Height * imageListCapturas.ImageSize.Width / reader.Width);

            PathImagenes = new List<string>();

        }

        private void timerPlayer_Tick(object sender, EventArgs e)
        {
            if (reader != null && reader.IsOpen)
            {
                var frame = reader.ReadVideoFrame();

                if (frame != null)
                {
                    pictureBoxPlayer.Image = (Bitmap)frame.Clone();
                    frameIndex++;

                }
                else
                {
                    timerPlayer.Stop();

                }
                trackBar1.Value = frameIndex;

            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {

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
                    pictureBoxPlayer.Image = (Bitmap)reader.ReadVideoFrame().Clone();
                }

                trackBar1.Value = frameIndex;
            }
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            timerPlayer.Stop();
            frameIndex = trackBar1.Value;
           
        
            reader.Close();
            reader.Open(this.PathVideo);

            for (int index = 0; index < frameIndex-1; index++)
            {
                reader.ReadVideoFrame();
            }

            var imagen = reader.ReadVideoFrame();

            if(imagen!=null)
                pictureBoxPlayer.Image = (Bitmap)imagen.Clone();


        }

        private void buttonCapturarImg_Click(object sender, EventArgs e)
        {
            string nombreArchivo = $"{PathBaseImagenes}\\{DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff")}.jpg";

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write))
                {
                    Imagen =(Bitmap) pictureBoxPlayer.Image.Clone();
                    Imagen.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
                // MessageBox.Show($"Imagen guardada en {path}");
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
