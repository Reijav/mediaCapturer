namespace MediaCampturerControlerLib
{
    partial class VideoPlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayer));
            this.pictureBoxPlayer = new System.Windows.Forms.PictureBox();
            this.panelControles = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnRetroceder = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnAdelantar = new System.Windows.Forms.Button();
            this.timerPlayer = new System.Windows.Forms.Timer(this.components);
            this.listViewCapturas = new System.Windows.Forms.ListView();
            this.imageListCapturas = new System.Windows.Forms.ImageList(this.components);
            this.buttonCapturarImg = new System.Windows.Forms.Button();
            this.buttonAdjuntarFotosParaInforme = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).BeginInit();
            this.panelControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPlayer
            // 
            this.pictureBoxPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPlayer.Location = new System.Drawing.Point(3, 1);
            this.pictureBoxPlayer.Name = "pictureBoxPlayer";
            this.pictureBoxPlayer.Size = new System.Drawing.Size(562, 455);
            this.pictureBoxPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlayer.TabIndex = 4;
            this.pictureBoxPlayer.TabStop = false;
            // 
            // panelControles
            // 
            this.panelControles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControles.Controls.Add(this.trackBar1);
            this.panelControles.Controls.Add(this.btnRetroceder);
            this.panelControles.Controls.Add(this.btnPlay);
            this.panelControles.Controls.Add(this.btnAdelantar);
            this.panelControles.Location = new System.Drawing.Point(2, 460);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(563, 100);
            this.panelControles.TabIndex = 5;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(4, 4);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(556, 45);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar1_MouseUp);
            // 
            // btnRetroceder
            // 
            this.btnRetroceder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRetroceder.Location = new System.Drawing.Point(259, 55);
            this.btnRetroceder.Name = "btnRetroceder";
            this.btnRetroceder.Size = new System.Drawing.Size(75, 23);
            this.btnRetroceder.TabIndex = 4;
            this.btnRetroceder.Text = "Retroceder";
            this.btnRetroceder.UseVisualStyleBackColor = true;
            this.btnRetroceder.Click += new System.EventHandler(this.btnRetroceder_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(340, 55);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(122, 23);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnAdelantar
            // 
            this.btnAdelantar.Location = new System.Drawing.Point(468, 55);
            this.btnAdelantar.Name = "btnAdelantar";
            this.btnAdelantar.Size = new System.Drawing.Size(75, 23);
            this.btnAdelantar.TabIndex = 7;
            this.btnAdelantar.Text = "Adelantar";
            this.btnAdelantar.UseVisualStyleBackColor = true;
            this.btnAdelantar.Click += new System.EventHandler(this.btnAdelantar_Click);
            // 
            // timerPlayer
            // 
            this.timerPlayer.Tick += new System.EventHandler(this.timerPlayer_Tick);
            // 
            // listViewCapturas
            // 
            this.listViewCapturas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCapturas.HideSelection = false;
            this.listViewCapturas.LargeImageList = this.imageListCapturas;
            this.listViewCapturas.Location = new System.Drawing.Point(572, 83);
            this.listViewCapturas.Name = "listViewCapturas";
            this.listViewCapturas.Size = new System.Drawing.Size(212, 373);
            this.listViewCapturas.SmallImageList = this.imageListCapturas;
            this.listViewCapturas.TabIndex = 6;
            this.listViewCapturas.UseCompatibleStateImageBehavior = false;
            // 
            // imageListCapturas
            // 
            this.imageListCapturas.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListCapturas.ImageSize = new System.Drawing.Size(128, 128);
            this.imageListCapturas.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonCapturarImg
            // 
            this.buttonCapturarImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCapturarImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCapturarImg.Image = ((System.Drawing.Image)(resources.GetObject("buttonCapturarImg.Image")));
            this.buttonCapturarImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCapturarImg.Location = new System.Drawing.Point(572, 1);
            this.buttonCapturarImg.Name = "buttonCapturarImg";
            this.buttonCapturarImg.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.buttonCapturarImg.Size = new System.Drawing.Size(212, 79);
            this.buttonCapturarImg.TabIndex = 13;
            this.buttonCapturarImg.Text = "Capturar Imagen";
            this.buttonCapturarImg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCapturarImg.UseVisualStyleBackColor = true;
            this.buttonCapturarImg.Click += new System.EventHandler(this.buttonCapturarImg_Click);
            // 
            // buttonAdjuntarFotosParaInforme
            // 
            this.buttonAdjuntarFotosParaInforme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdjuntarFotosParaInforme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdjuntarFotosParaInforme.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdjuntarFotosParaInforme.Image")));
            this.buttonAdjuntarFotosParaInforme.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdjuntarFotosParaInforme.Location = new System.Drawing.Point(571, 462);
            this.buttonAdjuntarFotosParaInforme.Name = "buttonAdjuntarFotosParaInforme";
            this.buttonAdjuntarFotosParaInforme.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.buttonAdjuntarFotosParaInforme.Size = new System.Drawing.Size(212, 98);
            this.buttonAdjuntarFotosParaInforme.TabIndex = 14;
            this.buttonAdjuntarFotosParaInforme.Text = "Adjuntar Fotos";
            this.buttonAdjuntarFotosParaInforme.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAdjuntarFotosParaInforme.UseVisualStyleBackColor = true;
            this.buttonAdjuntarFotosParaInforme.Click += new System.EventHandler(this.buttonAdjuntarFotosParaInforme_Click);
            // 
            // VideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.buttonAdjuntarFotosParaInforme);
            this.Controls.Add(this.buttonCapturarImg);
            this.Controls.Add(this.listViewCapturas);
            this.Controls.Add(this.pictureBoxPlayer);
            this.Controls.Add(this.panelControles);
            this.Name = "VideoPlayer";
            this.Text = "VideoPlayer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.VideoPlayer_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).EndInit();
            this.panelControles.ResumeLayout(false);
            this.panelControles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxPlayer;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnAdelantar;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnRetroceder;
        private System.Windows.Forms.Timer timerPlayer;
        private System.Windows.Forms.ListView listViewCapturas;
        private System.Windows.Forms.ImageList imageListCapturas;
        private System.Windows.Forms.Button buttonCapturarImg;
        private System.Windows.Forms.Button buttonAdjuntarFotosParaInforme;
    }
}