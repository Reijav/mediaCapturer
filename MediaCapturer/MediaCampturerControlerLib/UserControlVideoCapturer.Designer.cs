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
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
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
            this.obtenerFotoDeVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListCaptured = new System.Windows.Forms.ImageList(this.components);
            this.imageListIconos = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDispositivos = new System.Windows.Forms.ComboBox();
            this.timerRecording = new System.Windows.Forms.Timer(this.components);
            this.imageListVideos = new System.Windows.Forms.ImageList(this.components);
            this.comboBoxCapabilitis = new System.Windows.Forms.ComboBox();
            this.comboBoxInputs = new System.Windows.Forms.ComboBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelCalidad = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAdelantar = new System.Windows.Forms.Button();
            this.buttonRetroceder = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonMaximizar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listViewIamgenesVideos = new System.Windows.Forms.ListView();
            this.listViewImages = new System.Windows.Forms.ListView();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.labelTiempoGrabacion = new System.Windows.Forms.Label();
            this.openFileDialogImagen = new System.Windows.Forms.OpenFileDialog();
            this.timerPlaying = new System.Windows.Forms.Timer(this.components);
            this.buttonMinimizar = new System.Windows.Forms.Button();
            this.pictureBoxMaximizado = new System.Windows.Forms.PictureBox();
            this.buttonAgregarDesdeArchivo = new System.Windows.Forms.Button();
            this.buttonGrabar = new System.Windows.Forms.Button();
            this.buttonCapturarImg = new System.Windows.Forms.Button();
            this.buttonObtenerVideo = new System.Windows.Forms.Button();
            this.contextMenuStripEliminar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMaximizado)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripEliminar
            // 
            this.contextMenuStripEliminar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem,
            this.obtenerFotoDeVideoToolStripMenuItem});
            this.contextMenuStripEliminar.Name = "contextMenuStripEliminar";
            this.contextMenuStripEliminar.Size = new System.Drawing.Size(194, 48);
            this.contextMenuStripEliminar.Text = "Eliminar";
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // obtenerFotoDeVideoToolStripMenuItem
            // 
            this.obtenerFotoDeVideoToolStripMenuItem.Name = "obtenerFotoDeVideoToolStripMenuItem";
            this.obtenerFotoDeVideoToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.obtenerFotoDeVideoToolStripMenuItem.Text = "Obtener Foto de Video";
            this.obtenerFotoDeVideoToolStripMenuItem.Click += new System.EventHandler(this.obtenerFotoDeVideoToolStripMenuItem_Click);
            // 
            // imageListCaptured
            // 
            this.imageListCaptured.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListCaptured.ImageSize = new System.Drawing.Size(140, 110);
            this.imageListCaptured.TransparentColor = System.Drawing.Color.Transparent;
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
            this.panel1.Controls.Add(this.buttonAdelantar);
            this.panel1.Controls.Add(this.buttonRetroceder);
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.buttonPlay);
            this.panel1.Controls.Add(this.buttonMaximizar);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.listViewIamgenesVideos);
            this.panel1.Controls.Add(this.listViewImages);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Location = new System.Drawing.Point(0, 143);
            this.panel1.MinimumSize = new System.Drawing.Size(600, 314);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 638);
            this.panel1.TabIndex = 20;
            // 
            // buttonAdelantar
            // 
            this.buttonAdelantar.Image = global::MediaCampturerControlerLib.Properties.Resources.iconfinder_fullscreen_expand_maximize_full_screen_3994367;
            this.buttonAdelantar.Location = new System.Drawing.Point(343, 443);
            this.buttonAdelantar.Name = "buttonAdelantar";
            this.buttonAdelantar.Size = new System.Drawing.Size(31, 27);
            this.buttonAdelantar.TabIndex = 23;
            this.buttonAdelantar.UseVisualStyleBackColor = true;
            this.buttonAdelantar.Visible = false;
            this.buttonAdelantar.Click += new System.EventHandler(this.buttonAdelantar_Click);
            // 
            // buttonRetroceder
            // 
            this.buttonRetroceder.Image = global::MediaCampturerControlerLib.Properties.Resources.iconfinder_fullscreen_expand_maximize_full_screen_3994367;
            this.buttonRetroceder.Location = new System.Drawing.Point(202, 443);
            this.buttonRetroceder.Name = "buttonRetroceder";
            this.buttonRetroceder.Size = new System.Drawing.Size(31, 27);
            this.buttonRetroceder.TabIndex = 22;
            this.buttonRetroceder.UseVisualStyleBackColor = true;
            this.buttonRetroceder.Visible = false;
            this.buttonRetroceder.Click += new System.EventHandler(this.buttonRetroceder_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Image = global::MediaCampturerControlerLib.Properties.Resources.iconfinder_ic_pause_48px_3669310;
            this.buttonStop.Location = new System.Drawing.Point(294, 443);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(31, 27);
            this.buttonStop.TabIndex = 21;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Visible = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonPlay.Image = global::MediaCampturerControlerLib.Properties.Resources.iconfinder_play_music_triangle_media_2203515;
            this.buttonPlay.Location = new System.Drawing.Point(248, 443);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(40, 27);
            this.buttonPlay.TabIndex = 20;
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Visible = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonMaximizar
            // 
            this.buttonMaximizar.Image = global::MediaCampturerControlerLib.Properties.Resources.iconfinder_fullscreen_expand_maximize_full_screen_3994367;
            this.buttonMaximizar.Location = new System.Drawing.Point(744, 4);
            this.buttonMaximizar.Name = "buttonMaximizar";
            this.buttonMaximizar.Size = new System.Drawing.Size(31, 27);
            this.buttonMaximizar.TabIndex = 19;
            this.buttonMaximizar.UseVisualStyleBackColor = true;
            this.buttonMaximizar.Click += new System.EventHandler(this.buttonMaximizar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBox1.Location = new System.Drawing.Point(12, 3);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(1280, 720);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(320, 240);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(764, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // listViewIamgenesVideos
            // 
            this.listViewIamgenesVideos.ContextMenuStrip = this.contextMenuStripEliminar;
            this.listViewIamgenesVideos.HideSelection = false;
            this.listViewIamgenesVideos.LargeImageList = this.imageListVideos;
            this.listViewIamgenesVideos.Location = new System.Drawing.Point(12, 501);
            this.listViewIamgenesVideos.MaximumSize = new System.Drawing.Size(1280, 200);
            this.listViewIamgenesVideos.MinimumSize = new System.Drawing.Size(320, 121);
            this.listViewIamgenesVideos.Name = "listViewIamgenesVideos";
            this.listViewIamgenesVideos.Size = new System.Drawing.Size(764, 121);
            this.listViewIamgenesVideos.SmallImageList = this.imageListVideos;
            this.listViewIamgenesVideos.TabIndex = 18;
            this.listViewIamgenesVideos.UseCompatibleStateImageBehavior = false;
            this.listViewIamgenesVideos.Click += new System.EventHandler(this.listViewIamgenesVideos_SelectedIndexChanged);
            this.listViewIamgenesVideos.DoubleClick += new System.EventHandler(this.listViewIamgenesVideos_DoubleClick);
            this.listViewIamgenesVideos.MouseEnter += new System.EventHandler(this.listViewIamgenesVideos_MouseEnter);
            // 
            // listViewImages
            // 
            this.listViewImages.ContextMenuStrip = this.contextMenuStripEliminar;
            this.listViewImages.HideSelection = false;
            this.listViewImages.LargeImageList = this.imageListCaptured;
            this.listViewImages.Location = new System.Drawing.Point(782, 3);
            this.listViewImages.MaximumSize = new System.Drawing.Size(1280, 920);
            this.listViewImages.MinimumSize = new System.Drawing.Size(100, 360);
            this.listViewImages.Name = "listViewImages";
            this.listViewImages.Size = new System.Drawing.Size(314, 619);
            this.listViewImages.SmallImageList = this.imageListCaptured;
            this.listViewImages.TabIndex = 17;
            this.listViewImages.UseCompatibleStateImageBehavior = false;
            this.listViewImages.Click += new System.EventHandler(this.listViewImages_Click);
            this.listViewImages.DoubleClick += new System.EventHandler(this.listViewImages_DoubleClick);
            this.listViewImages.MouseEnter += new System.EventHandler(this.listViewImages_MouseEnter);
            // 
            // trackBar1
            // 
            this.trackBar1.CausesValidation = false;
            this.trackBar1.Location = new System.Drawing.Point(16, 489);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(743, 45);
            this.trackBar1.TabIndex = 24;
            this.trackBar1.Visible = false;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            this.trackBar1.MouseLeave += new System.EventHandler(this.trackBar1_MouseLeave);
            this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseUp);
            // 
            // labelTiempoGrabacion
            // 
            this.labelTiempoGrabacion.AutoSize = true;
            this.labelTiempoGrabacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTiempoGrabacion.Location = new System.Drawing.Point(312, 77);
            this.labelTiempoGrabacion.Name = "labelTiempoGrabacion";
            this.labelTiempoGrabacion.Size = new System.Drawing.Size(96, 26);
            this.labelTiempoGrabacion.TabIndex = 21;
            this.labelTiempoGrabacion.Text = "00:00:00";
            // 
            // openFileDialogImagen
            // 
            this.openFileDialogImagen.FileName = "openFileDialog1";
            // 
            // timerPlaying
            // 
            this.timerPlaying.Interval = 30;
            this.timerPlaying.Tick += new System.EventHandler(this.timerPlaying_Tick);
            // 
            // buttonMinimizar
            // 
            this.buttonMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMinimizar.Image = global::MediaCampturerControlerLib.Properties.Resources.iconfinder_fullscreen_exit_326649;
            this.buttonMinimizar.Location = new System.Drawing.Point(1150, 12);
            this.buttonMinimizar.Name = "buttonMinimizar";
            this.buttonMinimizar.Size = new System.Drawing.Size(31, 27);
            this.buttonMinimizar.TabIndex = 20;
            this.buttonMinimizar.UseVisualStyleBackColor = true;
            this.buttonMinimizar.Click += new System.EventHandler(this.buttonMinimizar_Click);
            // 
            // pictureBoxMaximizado
            // 
            this.pictureBoxMaximizado.Location = new System.Drawing.Point(1102, 0);
            this.pictureBoxMaximizado.MinimumSize = new System.Drawing.Size(320, 240);
            this.pictureBoxMaximizado.Name = "pictureBoxMaximizado";
            this.pictureBoxMaximizado.Size = new System.Drawing.Size(320, 240);
            this.pictureBoxMaximizado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMaximizado.TabIndex = 23;
            this.pictureBoxMaximizado.TabStop = false;
            this.pictureBoxMaximizado.Visible = false;
            // 
            // buttonAgregarDesdeArchivo
            // 
            this.buttonAgregarDesdeArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgregarDesdeArchivo.Image = ((System.Drawing.Image)(resources.GetObject("buttonAgregarDesdeArchivo.Image")));
            this.buttonAgregarDesdeArchivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAgregarDesdeArchivo.Location = new System.Drawing.Point(899, 40);
            this.buttonAgregarDesdeArchivo.Name = "buttonAgregarDesdeArchivo";
            this.buttonAgregarDesdeArchivo.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.buttonAgregarDesdeArchivo.Size = new System.Drawing.Size(197, 79);
            this.buttonAgregarDesdeArchivo.TabIndex = 22;
            this.buttonAgregarDesdeArchivo.Text = "Agregar Foto";
            this.buttonAgregarDesdeArchivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAgregarDesdeArchivo.UseVisualStyleBackColor = true;
            this.buttonAgregarDesdeArchivo.Click += new System.EventHandler(this.buttonAgregarDesdeArchivo_Click);
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
            // UserControlVideoCapturer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonMinimizar);
            this.Controls.Add(this.pictureBoxMaximizado);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonAgregarDesdeArchivo);
            this.Controls.Add(this.labelTiempoGrabacion);
            this.Controls.Add(this.labelCalidad);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.comboBoxInputs);
            this.Controls.Add(this.comboBoxCapabilitis);
            this.Controls.Add(this.buttonGrabar);
            this.Controls.Add(this.buttonCapturarImg);
            this.Controls.Add(this.buttonObtenerVideo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxDispositivos);
            this.Name = "UserControlVideoCapturer";
            this.Size = new System.Drawing.Size(1194, 784);
            this.Load += new System.EventHandler(this.UserControlVideoCapturer_Load);
            this.SizeChanged += new System.EventHandler(this.UserControlVideoCapturer_SizeChanged);
            this.contextMenuStripEliminar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMaximizado)).EndInit();
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
        private System.Windows.Forms.Label labelTiempoGrabacion;
        private System.Windows.Forms.Button buttonAgregarDesdeArchivo;
        private System.Windows.Forms.OpenFileDialog openFileDialogImagen;
        private System.Windows.Forms.Button buttonMaximizar;
        private System.Windows.Forms.PictureBox pictureBoxMaximizado;
        private System.Windows.Forms.Button buttonMinimizar;
        private System.Windows.Forms.Timer timerPlaying;
        private System.Windows.Forms.Button buttonAdelantar;
        private System.Windows.Forms.Button buttonRetroceder;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ToolStripMenuItem obtenerFotoDeVideoToolStripMenuItem;
    }
}
