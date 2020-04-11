﻿namespace MediaCampturerControlerLib
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
            this.contextMenuStripEliminar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListCaptured = new System.Windows.Forms.ImageList(this.components);
            this.buttonGrabar = new System.Windows.Forms.Button();
            this.imageListIconos = new System.Windows.Forms.ImageList(this.components);
            this.buttonCapturarImg = new System.Windows.Forms.Button();
            this.buttonObtenerVideo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDispositivos = new System.Windows.Forms.ComboBox();
            this.timerRecording = new System.Windows.Forms.Timer(this.components);
            this.imageListVideos = new System.Windows.Forms.ImageList(this.components);
            this.comboBoxCapabilitis = new System.Windows.Forms.ComboBox();
            this.comboBoxInputs = new System.Windows.Forms.ComboBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelCalidad = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewIamgenesVideos = new System.Windows.Forms.ListView();
            this.listViewImages = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStripEliminar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripEliminar
            // 
            this.contextMenuStripEliminar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStripEliminar.Name = "contextMenuStripEliminar";
            this.contextMenuStripEliminar.Size = new System.Drawing.Size(118, 26);
            this.contextMenuStripEliminar.Text = "Eliminar";
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
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
            this.buttonGrabar.Location = new System.Drawing.Point(454, 40);
            this.buttonGrabar.Name = "buttonGrabar";
            this.buttonGrabar.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.buttonGrabar.Size = new System.Drawing.Size(198, 79);
            this.buttonGrabar.TabIndex = 13;
            this.buttonGrabar.Text = "Grabar Video";
            this.buttonGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonGrabar.UseVisualStyleBackColor = true;
            this.buttonGrabar.Click += new System.EventHandler(this.buttonGrabarVideo_Click);
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
            // buttonCapturarImg
            // 
            this.buttonCapturarImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCapturarImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCapturarImg.ImageIndex = 2;
            this.buttonCapturarImg.ImageList = this.imageListIconos;
            this.buttonCapturarImg.Location = new System.Drawing.Point(672, 40);
            this.buttonCapturarImg.Name = "buttonCapturarImg";
            this.buttonCapturarImg.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.buttonCapturarImg.Size = new System.Drawing.Size(221, 79);
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
            this.buttonObtenerVideo.Location = new System.Drawing.Point(12, 40);
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
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
            this.comboBoxDispositivos.SelectedIndexChanged += new System.EventHandler(this.comboBoxDispositivos_SelectedIndexChanged);
            // 
            // timerRecording
            // 
            this.timerRecording.Interval = 200;
            this.timerRecording.Tick += new System.EventHandler(this.timerRecording_Tick);
            // 
            // imageListVideos
            // 
            this.imageListVideos.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListVideos.ImageSize = new System.Drawing.Size(80, 60);
            this.imageListVideos.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // comboBoxCapabilitis
            // 
            this.comboBoxCapabilitis.FormattingEnabled = true;
            this.comboBoxCapabilitis.Location = new System.Drawing.Point(656, 12);
            this.comboBoxCapabilitis.Name = "comboBoxCapabilitis";
            this.comboBoxCapabilitis.Size = new System.Drawing.Size(271, 21);
            this.comboBoxCapabilitis.TabIndex = 16;
            // 
            // comboBoxInputs
            // 
            this.comboBoxInputs.FormattingEnabled = true;
            this.comboBoxInputs.Location = new System.Drawing.Point(434, 11);
            this.comboBoxInputs.Name = "comboBoxInputs";
            this.comboBoxInputs.Size = new System.Drawing.Size(151, 21);
            this.comboBoxInputs.TabIndex = 17;
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInput.Location = new System.Drawing.Point(384, 15);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(42, 13);
            this.labelInput.TabIndex = 18;
            this.labelInput.Text = "Inputs";
            // 
            // labelCalidad
            // 
            this.labelCalidad.AutoSize = true;
            this.labelCalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCalidad.Location = new System.Drawing.Point(598, 15);
            this.labelCalidad.Name = "labelCalidad";
            this.labelCalidad.Size = new System.Drawing.Size(52, 13);
            this.labelCalidad.TabIndex = 19;
            this.labelCalidad.Text = "Formato";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.listViewIamgenesVideos);
            this.panel1.Controls.Add(this.listViewImages);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 143);
            this.panel1.MinimumSize = new System.Drawing.Size(600, 314);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1174, 638);
            this.panel1.TabIndex = 20;
            // 
            // listViewIamgenesVideos
            // 
            this.listViewIamgenesVideos.ContextMenuStrip = this.contextMenuStripEliminar;
            this.listViewIamgenesVideos.HideSelection = false;
            this.listViewIamgenesVideos.LargeImageList = this.imageListVideos;
            this.listViewIamgenesVideos.Location = new System.Drawing.Point(12, 501);
            this.listViewIamgenesVideos.Name = "listViewIamgenesVideos";
            this.listViewIamgenesVideos.Size = new System.Drawing.Size(640, 121);
            this.listViewIamgenesVideos.SmallImageList = this.imageListVideos;
            this.listViewIamgenesVideos.TabIndex = 18;
            this.listViewIamgenesVideos.UseCompatibleStateImageBehavior = false;
            this.listViewIamgenesVideos.DoubleClick += new System.EventHandler(this.listViewIamgenesVideos_DoubleClick);
            // 
            // listViewImages
            // 
            this.listViewImages.ContextMenuStrip = this.contextMenuStripEliminar;
            this.listViewImages.HideSelection = false;
            this.listViewImages.LargeImageList = this.imageListCaptured;
            this.listViewImages.Location = new System.Drawing.Point(672, 3);
            this.listViewImages.Name = "listViewImages";
            this.listViewImages.Size = new System.Drawing.Size(463, 619);
            this.listViewImages.SmallImageList = this.imageListCaptured;
            this.listViewImages.TabIndex = 17;
            this.listViewImages.UseCompatibleStateImageBehavior = false;
            this.listViewImages.DoubleClick += new System.EventHandler(this.listViewImages_DoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // UserControlVideoCapturer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelCalidad);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.comboBoxInputs);
            this.Controls.Add(this.comboBoxCapabilitis);
            this.Controls.Add(this.buttonGrabar);
            this.Controls.Add(this.buttonCapturarImg);
            this.Controls.Add(this.buttonObtenerVideo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDispositivos);
            this.Controls.Add(this.panel1);
            this.Name = "UserControlVideoCapturer";
            this.Size = new System.Drawing.Size(1192, 784);
            this.Load += new System.EventHandler(this.UserControlVideoCapturer_Load);
            this.contextMenuStripEliminar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonGrabar;
        private System.Windows.Forms.Button buttonCapturarImg;
        private System.Windows.Forms.Button buttonObtenerVideo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDispositivos;
        private System.Windows.Forms.ImageList imageListCaptured;
        private System.Windows.Forms.ImageList imageListIconos;
        private System.Windows.Forms.Timer timerRecording;
        private System.Windows.Forms.ImageList imageListVideos;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEliminar;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxCapabilitis;
        private System.Windows.Forms.ComboBox comboBoxInputs;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Label labelCalidad;
        private System.Windows.Forms.ListView listViewIamgenesVideos;
        private System.Windows.Forms.ListView listViewImages;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Panel panel1;
    }
}
