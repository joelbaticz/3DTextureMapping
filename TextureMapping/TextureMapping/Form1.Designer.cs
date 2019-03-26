namespace TextureMapping
{
    partial class Form1
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.pbFrameBuffer = new System.Windows.Forms.PictureBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblAngleX = new System.Windows.Forms.Label();
            this.lblAngleY = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.lblAngleZ = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbFrameBuffer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // pbFrameBuffer
            // 
            this.pbFrameBuffer.Location = new System.Drawing.Point(213, 12);
            this.pbFrameBuffer.Name = "pbFrameBuffer";
            this.pbFrameBuffer.Size = new System.Drawing.Size(1024, 768);
            this.pbFrameBuffer.TabIndex = 1;
            this.pbFrameBuffer.TabStop = false;
            // 
            // btnDraw
            // 
            this.btnDraw.Enabled = false;
            this.btnDraw.Location = new System.Drawing.Point(12, 41);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 2;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 93);
            this.trackBar1.Maximum = 180;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(195, 45);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblAngleX
            // 
            this.lblAngleX.AutoSize = true;
            this.lblAngleX.Location = new System.Drawing.Point(12, 77);
            this.lblAngleX.Name = "lblAngleX";
            this.lblAngleX.Size = new System.Drawing.Size(62, 13);
            this.lblAngleX.TabIndex = 4;
            this.lblAngleX.Text = "Angle X (0):";
            // 
            // lblAngleY
            // 
            this.lblAngleY.AutoSize = true;
            this.lblAngleY.Location = new System.Drawing.Point(12, 125);
            this.lblAngleY.Name = "lblAngleY";
            this.lblAngleY.Size = new System.Drawing.Size(62, 13);
            this.lblAngleY.TabIndex = 6;
            this.lblAngleY.Text = "Angle Y (0):";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(12, 144);
            this.trackBar2.Maximum = 180;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(195, 45);
            this.trackBar2.TabIndex = 5;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // lblAngleZ
            // 
            this.lblAngleZ.AutoSize = true;
            this.lblAngleZ.Location = new System.Drawing.Point(12, 176);
            this.lblAngleZ.Name = "lblAngleZ";
            this.lblAngleZ.Size = new System.Drawing.Size(62, 13);
            this.lblAngleZ.TabIndex = 8;
            this.lblAngleZ.Text = "Angle Z (0):";
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(12, 195);
            this.trackBar3.Maximum = 180;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(195, 45);
            this.trackBar3.TabIndex = 7;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 786);
            this.Controls.Add(this.lblAngleZ);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.lblAngleY);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.lblAngleX);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.pbFrameBuffer);
            this.Controls.Add(this.btnOpen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbFrameBuffer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.PictureBox pbFrameBuffer;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblAngleX;
        private System.Windows.Forms.Label lblAngleY;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label lblAngleZ;
        private System.Windows.Forms.TrackBar trackBar3;
    }
}

