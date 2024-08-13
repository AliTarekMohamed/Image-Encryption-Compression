using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ImageEncryptCompress;
using static ImageEncryptCompress.ImageOperations;
namespace ImageEncryptCompress
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"D:\FCIS - ASU\Y3S2\Alogrithm Analysis & Design\Project\[1] Image Encryption and Compression\Complete Test";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            Width.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            Height.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }
        private void Encryption_Click(object sender, EventArgs e)
        {
            ImageMatrix = ImageOperations.Encryption(ImageMatrix, Initial_Seed.Text, Tap_Position.Text);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
            MessageBox.Show("Encryption Done", "Encrypting...");

            //save in bitmap
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = @"D:\FCIS - ASU\Y3S2\Alogrithm Analysis & Design\Project\[1] Image Encryption and Compression\Output";
            saveFileDialog1.FileName = "Encrypted Image";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
        }

        private void Compression_Click(object sender, EventArgs e)
        {
            // Compression
            Tuple<List<Dictionary<byte, string>>, List<string>> compressed_image = ImageOperations.Compression(ImageMatrix);
            MessageBox.Show("Compression Done", "Compressing...");

            // Saving into Binary File
            ImageOperations.SaveFile(Initial_Seed.Text, Tap_Position.Text, compressed_image.Item1, compressed_image.Item2);
            Initial_Seed.Text = "";
            Tap_Position.Text = "";
            MessageBox.Show("Saved", "Saving...");
        }

        private void Decompression_Click(object sender, EventArgs e)
        {
            ImageMatrix = ImageOperations.Decompression(Width.Text, Height.Text);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
            MessageBox.Show("Decompression Done", "Decompressing...");
        }

        private void Decryption_Click(object sender, EventArgs e)
        {
            ImageMatrix = ImageOperations.Decryption(ImageMatrix);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
            MessageBox.Show("Decryption Done", "Decrypting...");

            //save in bitmap
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = @"D:\FCIS - ASU\Y3S2\Alogrithm Analysis & Design\Project\[1] Image Encryption and Compression\Output";
            saveFileDialog1.FileName = "Restored Image";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
        }
    }
}