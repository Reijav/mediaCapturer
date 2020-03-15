namespace MediaCampturerControlerLib
{
    partial class UserControlVideoCapturer
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (FileWriter != null)
            {
                CerrarWebCam();
                FileWriter.Dispose();
            }
            


            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlVideoCapturer));
            this.listViewImages = new System.Windows.Forms.ListView();
            this.imageListCaptured = new System.Windows.Forms.ImageList(this.components);
            this.buttonGrabar = new System.Windows.Forms.Button();
            this.buttonCapturarImg = new System.Windows.Forms.Button();
            this.buttonObtenerVideo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDispositivos = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageListIconos = new System.Windows.Forms.ImageList(this.components);
            this.timerRecording = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewImages
            // 
            this.listViewImages.HideSelection = false;
            this.listViewImages.LargeImageList = this.imageListCaptured;
            this.listViewImages.Location = new System.Drawing.Point(672, 125);
            this.listViewImages.Name = "listViewImages";
            this.listViewImages.Size = new System.Drawing.Size(463, 483);
            this.listViewImages.SmallImageList = this.imageListCaptured;
            this.listViewImages.TabIndex = 14;
            this.listViewImages.UseCompatibleStateImageBehavior = false;
            // 
            // imageListCaptured
            // 
            this.imageListCaptured.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListCaptured.ImageSize = new System.Drawing.Size(160, 120);
            this.imageListCaptured.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonGrabar
            // 
            this.buttonGrabar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGrabar.ImageIndex = 1;
            this.buttonGrabar.ImageList = this.imageListIconos;
            this.buttonGrabar.Location = new System.Drawing.Point(454, 31);
            this.buttonGrabar.Name = "buttonGrabar";
            this.buttonGrabar.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.buttonGrabar.Size = new System.Drawing.Size(198, 88);
            this.buttonGrabar.TabIndex = 13;
            this.buttonGrabar.Text = "Grabar Video";
            this.buttonGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonGrabar.UseVisualStyleBackColor = true;
            this.buttonGrabar.Click += new System.EventHandler(this.buttonGrabarVideo_Click);
            // 
            // buttonCapturarImg
            // 
            this.buttonCapturarImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCapturarImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCapturarImg.ImageIndex = 2;
            this.buttonCapturarImg.ImageList = this.imageListIconos;
            this.buttonCapturarImg.Location = new System.Drawing.Point(672, 31);
            this.buttonCapturarImg.Name = "buttonCapturarImg";
            this.buttonCapturarImg.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.buttonCapturarImg.Size = new System.Drawing.Size(221, 88);
            this.buttonCapturarImg.TabIndex = 12;
            this.buttonCapturarImg.Text = "Capturar Imagen";
            this.buttonCapturarImg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCapturarImg.UseVisualStyleBackColor = true;
            this.buttonCapturarImg.Click += new System.EventHandler(this.buttonCapturarImg_Click_1);
            // 
            // buttonObtenerVideo
            // 
            this.buttonObtenerVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonObtenerVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonObtenerVideo.ImageIndex = 3;
            this.buttonObtenerVideo.ImageList = this.imageListIconos;
            this.buttonObtenerVideo.Location = new System.Drawing.Point(16, 40);
            this.buttonObtenerVideo.Name = "buttonObtenerVideo";
            this.buttonObtenerVideo.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.buttonObtenerVideo.Size = new System.Drawing.Size(258, 79);
            this.buttonObtenerVideo.TabIndex = 11;
            this.buttonObtenerVideo.Text = "Conectar Con Dispositivo";
            this.buttonObtenerVideo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonObtenerVideo.UseVisualStyleBackColor = true;
            this.buttonObtenerVideo.Click += new System.EventHandler(this.buttonConectarDispositivo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Dispositivos";
            // 
            // comboBoxDispositivos
            // 
            this.comboBoxDispositivos.FormattingEnabled = true;
            this.comboBoxDispositivos.Location = new System.Drawing.Point(103, 13);
            this.comboBoxDispositivos.Name = "comboBoxDispositivos";
            this.comboBoxDispositivos.Size = new System.Drawing.Size(271, 21);
            this.comboBoxDispositivos.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.Location = new System.Drawing.Point(12, 125);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // imageListIconos
            // 
            this.imageListIconos.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIconos.ImageStream")));
            this.imageListIconos.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIconos.Images.SetKeyName(0, "icons8-stop-24.png");
            this.imageListIconos.Images.SetKeyName(1, "icons8-record-48.png");
            this.imageListIconos.Images.SetKeyName(2, "icons8-screenshot-50 (1).png");
            this.imageListIconos.Images.SetKeyName(3, "desconectar.png");
            this.imageListIconos.Images.SetKeyName(4, "connect.png");
            this.imageListIconos.Images.SetKeyName(5, "icons8-stop-48 (1).png");
            this.imageListIconos.Images.SetKeyName(6, "icons8-stop-64.png");
            // 
            // timerRecording
            // 
            this.timerRecording.Interval = 200;
            this.timerRecording.Tick += new System.EventHandler(this.timerRecording_Tick);
            // 
            // UserControlVideoCapturer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewImages);
            this.Controls.Add(this.buttonGrabar);
            this.Controls.Add(this.buttonCapturarImg);
            this.Controls.Add(this.buttonObtenerVideo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDispositivos);
            this.Controls.Add(this.pictureBox1);
            this.Name = "UserControlVideoCapturer";
            this.Size = new System.Drawing.Size(1164, 616);
            this.Load += new System.EventHandler(this.UserControlVideoCapturer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewImages;
        private System.Windows.Forms.Button buttonGrabar;
        private System.Windows.Forms.Button buttonCapturarImg;
        private System.Windows.Forms.Button buttonObtenerVideo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDispositivos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ImageList imageListCaptured;
        private System.Windows.Forms.ImageList imageListIconos;
        private System.Windows.Forms.Timer timerRecording;
    }
}
