using System;
using System.IO;

namespace MediaCampturerControlerLib
{
    partial class VideoPlayerVlc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayerVlc));
            this.button1 = new System.Windows.Forms.Button();
            this.buttonPlayPause = new System.Windows.Forms.Button();
            this.buttonAdelantar = new System.Windows.Forms.Button();
            this.listViewCapturas = new System.Windows.Forms.ListView();
            this.imageListCapturas = new System.Windows.Forms.ImageList(this.components);
            this.trackBarVideo = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonSnapShot = new System.Windows.Forms.Button();
            this.openFileVideoDialog = new System.Windows.Forms.OpenFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(127, 464);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "<< Retroceder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonPlayPause
            // 
            this.buttonPlayPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPlayPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlayPause.Location = new System.Drawing.Point(288, 464);
            this.buttonPlayPause.Name = "buttonPlayPause";
            this.buttonPlayPause.Size = new System.Drawing.Size(132, 38);
            this.buttonPlayPause.TabIndex = 2;
            this.buttonPlayPause.Text = "Reproducir";
            this.buttonPlayPause.UseVisualStyleBackColor = true;
            this.buttonPlayPause.Click += new System.EventHandler(this.buttonPlayPause_Click);
            // 
            // buttonAdelantar
            // 
            this.buttonAdelantar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdelantar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdelantar.Location = new System.Drawing.Point(426, 464);
            this.buttonAdelantar.Name = "buttonAdelantar";
            this.buttonAdelantar.Size = new System.Drawing.Size(132, 38);
            this.buttonAdelantar.TabIndex = 3;
            this.buttonAdelantar.Text = "Adelantar >>";
            this.buttonAdelantar.UseVisualStyleBackColor = true;
            this.buttonAdelantar.Click += new System.EventHandler(this.buttonAdelantar_Click);
            // 
            // listViewCapturas
            // 
            this.listViewCapturas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewCapturas.HideSelection = false;
            this.listViewCapturas.LargeImageList = this.imageListCapturas;
            this.listViewCapturas.Location = new System.Drawing.Point(683, 71);
            this.listViewCapturas.Name = "listViewCapturas";
            this.listViewCapturas.Size = new System.Drawing.Size(199, 366);
            this.listViewCapturas.SmallImageList = this.imageListCapturas;
            this.listViewCapturas.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listViewCapturas.TabIndex = 5;
            this.listViewCapturas.UseCompatibleStateImageBehavior = false;
            // 
            // imageListCapturas
            // 
            this.imageListCapturas.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListCapturas.ImageSize = new System.Drawing.Size(100, 80);
            this.imageListCapturas.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // trackBarVideo
            // 
            this.trackBarVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarVideo.Location = new System.Drawing.Point(2, 406);
            this.trackBarVideo.Name = "trackBarVideo";
            this.trackBarVideo.Size = new System.Drawing.Size(675, 45);
            this.trackBarVideo.TabIndex = 6;
            this.trackBarVideo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBarVideo_MouseDown);
            this.trackBarVideo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarVideo_MouseUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 471);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(13, 13);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(0, 13);
            this.labelInfo.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::MediaCampturerControlerLib.Properties.Resources.iconfinder_199_CircledPlus_183316;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(683, 443);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 82);
            this.button2.TabIndex = 8;
            this.button2.Text = "           Adjuntar Fotos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonSnapShot
            // 
            this.buttonSnapShot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSnapShot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSnapShot.Image = ((System.Drawing.Image)(resources.GetObject("buttonSnapShot.Image")));
            this.buttonSnapShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSnapShot.Location = new System.Drawing.Point(683, 2);
            this.buttonSnapShot.Name = "buttonSnapShot";
            this.buttonSnapShot.Size = new System.Drawing.Size(199, 63);
            this.buttonSnapShot.TabIndex = 4;
            this.buttonSnapShot.Text = "       Capturar Foto";
            this.buttonSnapShot.UseVisualStyleBackColor = true;
            this.buttonSnapShot.Click += new System.EventHandler(this.buttonSnapShot_Click);
            // 
            // openFileVideoDialog
            // 
            this.openFileVideoDialog.FileName = "openFileDialog1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(16, 494);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // VideoPlayerVlc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 529);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarVideo);
            this.Controls.Add(this.listViewCapturas);
            this.Controls.Add(this.buttonSnapShot);
            this.Controls.Add(this.buttonAdelantar);
            this.Controls.Add(this.buttonPlayPause);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelInfo);
            this.Name = "VideoPlayerVlc";
            this.Text = "Obtener Fotos de Video";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoPlayerVlc_FormClosing);
            this.Load += new System.EventHandler(this.VideoPlayerVlc_Load);
            this.Resize += new System.EventHandler(this.VideoPlayerVlc_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        //private Vlc.DotNet.Forms.VlcControl vlcControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonPlayPause;
        private System.Windows.Forms.Button buttonAdelantar;
        private System.Windows.Forms.Button buttonSnapShot;
        private System.Windows.Forms.ListView listViewCapturas;
        private System.Windows.Forms.ImageList imageListCapturas;
        private System.Windows.Forms.TrackBar trackBarVideo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.OpenFileDialog openFileVideoDialog;
        private System.Windows.Forms.Button button3;
    }
}