namespace ImageEncryptCompress
{
    partial class MainForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.original_image = new System.Windows.Forms.Label();
            this.smoothed_image = new System.Windows.Forms.Label();
            this.btnGaussSmooth = new System.Windows.Forms.Button();
            this.mask_size = new System.Windows.Forms.Label();
            this.gauss_sigma = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.TextBox();
            this.nudMaskSize = new System.Windows.Forms.NumericUpDown();
            this.Width = new System.Windows.Forms.TextBox();
            this.width_label = new System.Windows.Forms.Label();
            this.height_label = new System.Windows.Forms.Label();
            this.txtGaussSigma = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.encryption = new System.Windows.Forms.Button();
            this.compression = new System.Windows.Forms.Button();
            this.decompression = new System.Windows.Forms.Button();
            this.Initial_Seed = new System.Windows.Forms.TextBox();
            this.Tap_Position = new System.Windows.Forms.TextBox();
            this.inital_seed_label = new System.Windows.Forms.Label();
            this.tap_position_label = new System.Windows.Forms.Label();
            this.decryption = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaskSize)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(835, 704);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(8, 8);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(805, 704);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.White;
            this.btnOpen.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.Location = new System.Drawing.Point(329, 513);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(125, 89);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open Image";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // original_image
            // 
            this.original_image.AutoSize = true;
            this.original_image.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.original_image.Location = new System.Drawing.Point(217, 484);
            this.original_image.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.original_image.Name = "original_image";
            this.original_image.Size = new System.Drawing.Size(162, 24);
            this.original_image.TabIndex = 3;
            this.original_image.Text = "Original Image";
            // 
            // smoothed_image
            // 
            this.smoothed_image.AutoSize = true;
            this.smoothed_image.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.smoothed_image.Location = new System.Drawing.Point(843, 481);
            this.smoothed_image.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.smoothed_image.Name = "smoothed_image";
            this.smoothed_image.Size = new System.Drawing.Size(184, 24);
            this.smoothed_image.TabIndex = 4;
            this.smoothed_image.Text = "Smoothed Image";
            // 
            // btnGaussSmooth
            // 
            this.btnGaussSmooth.BackColor = System.Drawing.Color.White;
            this.btnGaussSmooth.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGaussSmooth.Location = new System.Drawing.Point(1023, 550);
            this.btnGaussSmooth.Margin = new System.Windows.Forms.Padding(4);
            this.btnGaussSmooth.Name = "btnGaussSmooth";
            this.btnGaussSmooth.Size = new System.Drawing.Size(125, 89);
            this.btnGaussSmooth.TabIndex = 5;
            this.btnGaussSmooth.Text = "Gaussian Filter";
            this.btnGaussSmooth.UseVisualStyleBackColor = false;
            this.btnGaussSmooth.Click += new System.EventHandler(this.btnGaussSmooth_Click);
            // 
            // mask_size
            // 
            this.mask_size.AutoSize = true;
            this.mask_size.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mask_size.Location = new System.Drawing.Point(803, 604);
            this.mask_size.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mask_size.Name = "mask_size";
            this.mask_size.Size = new System.Drawing.Size(94, 21);
            this.mask_size.TabIndex = 7;
            this.mask_size.Text = "Mask Size";
            // 
            // gauss_sigma
            // 
            this.gauss_sigma.AutoSize = true;
            this.gauss_sigma.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gauss_sigma.Location = new System.Drawing.Point(803, 562);
            this.gauss_sigma.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gauss_sigma.Name = "gauss_sigma";
            this.gauss_sigma.Size = new System.Drawing.Size(120, 21);
            this.gauss_sigma.TabIndex = 9;
            this.gauss_sigma.Text = "Gauss Sigma";
            // 
            // Height
            // 
            this.Height.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Height.Location = new System.Drawing.Point(231, 562);
            this.Height.Margin = new System.Windows.Forms.Padding(4);
            this.Height.Name = "Height";
            this.Height.ReadOnly = true;
            this.Height.Size = new System.Drawing.Size(75, 27);
            this.Height.TabIndex = 8;
            // 
            // nudMaskSize
            // 
            this.nudMaskSize.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMaskSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudMaskSize.Location = new System.Drawing.Point(927, 602);
            this.nudMaskSize.Margin = new System.Windows.Forms.Padding(4);
            this.nudMaskSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nudMaskSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudMaskSize.Name = "nudMaskSize";
            this.nudMaskSize.Size = new System.Drawing.Size(76, 27);
            this.nudMaskSize.TabIndex = 10;
            this.nudMaskSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // Width
            // 
            this.Width.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Width.Location = new System.Drawing.Point(231, 527);
            this.Width.Margin = new System.Windows.Forms.Padding(4);
            this.Width.Name = "Width";
            this.Width.ReadOnly = true;
            this.Width.Size = new System.Drawing.Size(75, 27);
            this.Width.TabIndex = 11;
            // 
            // width_label
            // 
            this.width_label.AutoSize = true;
            this.width_label.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.width_label.Location = new System.Drawing.Point(157, 530);
            this.width_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.width_label.Name = "width_label";
            this.width_label.Size = new System.Drawing.Size(61, 21);
            this.width_label.TabIndex = 12;
            this.width_label.Text = "Width";
            // 
            // height_label
            // 
            this.height_label.AutoSize = true;
            this.height_label.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.height_label.Location = new System.Drawing.Point(157, 566);
            this.height_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.height_label.Name = "height_label";
            this.height_label.Size = new System.Drawing.Size(67, 21);
            this.height_label.TabIndex = 13;
            this.height_label.Text = "Height";
            // 
            // txtGaussSigma
            // 
            this.txtGaussSigma.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGaussSigma.Location = new System.Drawing.Point(927, 562);
            this.txtGaussSigma.Margin = new System.Windows.Forms.Padding(4);
            this.txtGaussSigma.Name = "txtGaussSigma";
            this.txtGaussSigma.Size = new System.Drawing.Size(75, 27);
            this.txtGaussSigma.TabIndex = 14;
            this.txtGaussSigma.Text = "1";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 456);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(628, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 456);
            this.panel2.TabIndex = 16;
            // 
            // encryption
            // 
            this.encryption.BackColor = System.Drawing.Color.White;
            this.encryption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.encryption.Location = new System.Drawing.Point(527, 486);
            this.encryption.Margin = new System.Windows.Forms.Padding(4);
            this.encryption.Name = "encryption";
            this.encryption.Size = new System.Drawing.Size(189, 42);
            this.encryption.TabIndex = 17;
            this.encryption.Text = "Encryption";
            this.encryption.UseVisualStyleBackColor = false;
            this.encryption.Click += new System.EventHandler(this.Encryption_Click);
            // 
            // compression
            // 
            this.compression.BackColor = System.Drawing.Color.White;
            this.compression.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compression.Location = new System.Drawing.Point(527, 536);
            this.compression.Margin = new System.Windows.Forms.Padding(4);
            this.compression.Name = "compression";
            this.compression.Size = new System.Drawing.Size(189, 42);
            this.compression.TabIndex = 18;
            this.compression.Text = "Compression";
            this.compression.UseVisualStyleBackColor = false;
            this.compression.Click += new System.EventHandler(this.Compression_Click);
            // 
            // decompression
            // 
            this.decompression.BackColor = System.Drawing.Color.White;
            this.decompression.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decompression.Location = new System.Drawing.Point(527, 585);
            this.decompression.Margin = new System.Windows.Forms.Padding(4);
            this.decompression.Name = "decompression";
            this.decompression.Size = new System.Drawing.Size(189, 42);
            this.decompression.TabIndex = 19;
            this.decompression.Text = "Decompression";
            this.decompression.UseVisualStyleBackColor = false;
            this.decompression.Click += new System.EventHandler(this.Decompression_Click);
            // 
            // Initial_Seed
            // 
            this.Initial_Seed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Initial_Seed.Location = new System.Drawing.Point(231, 609);
            this.Initial_Seed.Margin = new System.Windows.Forms.Padding(4);
            this.Initial_Seed.Name = "Initial_Seed";
            this.Initial_Seed.Size = new System.Drawing.Size(223, 27);
            this.Initial_Seed.TabIndex = 20;
            // 
            // Tap_Position
            // 
            this.Tap_Position.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tap_Position.Location = new System.Drawing.Point(231, 645);
            this.Tap_Position.Margin = new System.Windows.Forms.Padding(4);
            this.Tap_Position.Name = "Tap_Position";
            this.Tap_Position.Size = new System.Drawing.Size(223, 27);
            this.Tap_Position.TabIndex = 23;
            // 
            // inital_seed_label
            // 
            this.inital_seed_label.AutoSize = true;
            this.inital_seed_label.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inital_seed_label.Location = new System.Drawing.Point(112, 618);
            this.inital_seed_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.inital_seed_label.Name = "inital_seed_label";
            this.inital_seed_label.Size = new System.Drawing.Size(108, 21);
            this.inital_seed_label.TabIndex = 24;
            this.inital_seed_label.Text = "Initial Seed";
            // 
            // tap_position_label
            // 
            this.tap_position_label.AutoSize = true;
            this.tap_position_label.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tap_position_label.Location = new System.Drawing.Point(109, 645);
            this.tap_position_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tap_position_label.Name = "tap_position_label";
            this.tap_position_label.Size = new System.Drawing.Size(116, 21);
            this.tap_position_label.TabIndex = 25;
            this.tap_position_label.Text = "Tap Position";
            // 
            // decryption
            // 
            this.decryption.BackColor = System.Drawing.Color.White;
            this.decryption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decryption.Location = new System.Drawing.Point(527, 635);
            this.decryption.Margin = new System.Windows.Forms.Padding(4);
            this.decryption.Name = "decryption";
            this.decryption.Size = new System.Drawing.Size(189, 42);
            this.decryption.TabIndex = 26;
            this.decryption.Text = "Decryption";
            this.decryption.UseVisualStyleBackColor = false;
            this.decryption.Click += new System.EventHandler(this.Decryption_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1217, 691);
            this.Controls.Add(this.decryption);
            this.Controls.Add(this.tap_position_label);
            this.Controls.Add(this.inital_seed_label);
            this.Controls.Add(this.Tap_Position);
            this.Controls.Add(this.Initial_Seed);
            this.Controls.Add(this.decompression);
            this.Controls.Add(this.compression);
            this.Controls.Add(this.encryption);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtGaussSigma);
            this.Controls.Add(this.height_label);
            this.Controls.Add(this.width_label);
            this.Controls.Add(this.Width);
            this.Controls.Add(this.nudMaskSize);
            this.Controls.Add(this.gauss_sigma);
            this.Controls.Add(this.Height);
            this.Controls.Add(this.mask_size);
            this.Controls.Add(this.btnGaussSmooth);
            this.Controls.Add(this.smoothed_image);
            this.Controls.Add(this.original_image);
            this.Controls.Add(this.btnOpen);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Image Enctryption and Compression...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaskSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label original_image;
        private System.Windows.Forms.Label smoothed_image;
        private System.Windows.Forms.Label mask_size;
        private System.Windows.Forms.Label gauss_sigma;
        private System.Windows.Forms.Label width_label;
        private System.Windows.Forms.Label height_label;
        private System.Windows.Forms.Label inital_seed_label;
        private System.Windows.Forms.Label tap_position_label;
        private System.Windows.Forms.Button btnGaussSmooth;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button encryption;
        private System.Windows.Forms.Button compression;
        private System.Windows.Forms.Button decompression;
        private System.Windows.Forms.Button decryption;
        private System.Windows.Forms.TextBox Height;
        private System.Windows.Forms.TextBox Width;
        private System.Windows.Forms.TextBox Initial_Seed;
        private System.Windows.Forms.TextBox Tap_Position;
        private System.Windows.Forms.TextBox txtGaussSigma;
        private System.Windows.Forms.NumericUpDown nudMaskSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

