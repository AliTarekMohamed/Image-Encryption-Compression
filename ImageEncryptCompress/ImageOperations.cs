using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;
using static ImageEncryptCompress.ImageOperations;
using System.Collections;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace ImageEncryptCompress
{
    /// <summary>
    /// Holds the pixel color in 3 byte values: red, green and blue
    /// </summary>
    public struct RGBPixel
    {
        public byte red, green, blue;
    }
    public struct RGBPixelD
    {
        public double red, green, blue;
    }


    /// <summary>
    /// Library of static functions that deal with images
    /// </summary>
    public class ImageOperations
    {
        /// <summary>
        /// Open an image and load it into 2D array of colors (size: Height x Width)
        /// </summary>
        /// <param name="ImagePath">Image file path</param>
        /// <returns>2D array of colors</returns>
        public static RGBPixel[,] OpenImage(string ImagePath)
        {
            Bitmap original_bm = new Bitmap(ImagePath);
            int Height = original_bm.Height;
            int Width = original_bm.Width;

            RGBPixel[,] Buffer = new RGBPixel[Height, Width];

            unsafe
            {
                BitmapData bmd = original_bm.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, original_bm.PixelFormat);
                int x, y;
                int nWidth = 0;
                bool Format32 = false;
                bool Format24 = false;
                bool Format8 = false;

                if (original_bm.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    Format24 = true;
                    nWidth = Width * 3;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format32bppArgb || original_bm.PixelFormat == PixelFormat.Format32bppRgb || original_bm.PixelFormat == PixelFormat.Format32bppPArgb)
                {
                    Format32 = true;
                    nWidth = Width * 4;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    Format8 = true;
                    nWidth = Width;
                }
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (y = 0; y < Height; y++)
                {
                    for (x = 0; x < Width; x++)
                    {
                        if (Format8)
                        {
                            Buffer[y, x].red = Buffer[y, x].green = Buffer[y, x].blue = p[0];
                            p++;
                        }
                        else
                        {
                            Buffer[y, x].red = p[2];
                            Buffer[y, x].green = p[1];
                            Buffer[y, x].blue = p[0];
                            if (Format24) p += 3;
                            else if (Format32) p += 4;
                        }
                    }
                    p += nOffset;
                }
                original_bm.UnlockBits(bmd);
            }

            return Buffer;
        }

        /// <summary>
        /// Get the height of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Height</returns>
        public static int GetHeight(RGBPixel[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(0);
        }

        /// <summary>
        /// Get the width of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Width</returns>
        public static int GetWidth(RGBPixel[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(1);
        }

        /// <summary>
        /// Display the given image on the given PictureBox object
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <param name="PicBox">PictureBox object to display the image on it</param>
        public static void DisplayImage(RGBPixel[,] ImageMatrix, PictureBox PicBox)
        {
            // Create Image:
            //==============
            int Height = ImageMatrix.GetLength(0);
            int Width = ImageMatrix.GetLength(1);

            Bitmap ImageBMP = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = Width * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        p[2] = ImageMatrix[i, j].red;
                        p[1] = ImageMatrix[i, j].green;
                        p[0] = ImageMatrix[i, j].blue;
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd);
            }
            PicBox.Image = ImageBMP;
        }

        /// <summary>
        /// Apply Gaussian smoothing filter to enhance the edge detection 
        /// </summary>
        /// <param name="ImageMatrix">Colored image matrix</param>
        /// <param name="filterSize">Gaussian mask size</param>
        /// <param name="sigma">Gaussian sigma</param>
        /// <returns>smoothed color image</returns>
        public static RGBPixel[,] GaussianFilter1D(RGBPixel[,] ImageMatrix, int filterSize, double sigma)
        {
            int Height = GetHeight(ImageMatrix);
            int Width = GetWidth(ImageMatrix);

            RGBPixelD[,] VerFiltered = new RGBPixelD[Height, Width];
            RGBPixel[,] Filtered = new RGBPixel[Height, Width];


            // Create Filter in Spatial Domain:
            //=================================
            //make the filter ODD size
            if (filterSize % 2 == 0) filterSize++;

            double[] Filter = new double[filterSize];

            //Compute Filter in Spatial Domain :
            //==================================
            double Sum1 = 0;
            int HalfSize = filterSize / 2;
            for (int y = -HalfSize; y <= HalfSize; y++)
            {
                //Filter[y+HalfSize] = (1.0 / (Math.Sqrt(2 * 22.0/7.0) * Segma)) * Math.Exp(-(double)(y*y) / (double)(2 * Segma * Segma)) ;
                Filter[y + HalfSize] = Math.Exp(-(double)(y * y) / (double)(2 * sigma * sigma));
                Sum1 += Filter[y + HalfSize];
            }
            for (int y = -HalfSize; y <= HalfSize; y++)
            {
                Filter[y + HalfSize] /= Sum1;
            }

            //Filter Original Image Vertically:
            //=================================
            int ii, jj;
            RGBPixelD Sum;
            RGBPixel Item1;
            RGBPixelD Item2;

            for (int j = 0; j < Width; j++)
                for (int i = 0; i < Height; i++)
                {
                    Sum.red = 0;
                    Sum.green = 0;
                    Sum.blue = 0;
                    for (int y = -HalfSize; y <= HalfSize; y++)
                    {
                        ii = i + y;
                        if (ii >= 0 && ii < Height)
                        {
                            Item1 = ImageMatrix[ii, j];
                            Sum.red += Filter[y + HalfSize] * Item1.red;
                            Sum.green += Filter[y + HalfSize] * Item1.green;
                            Sum.blue += Filter[y + HalfSize] * Item1.blue;
                        }
                    }
                    VerFiltered[i, j] = Sum;
                }

            //Filter Resulting Image Horizontally:
            //===================================
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                {
                    Sum.red = 0;
                    Sum.green = 0;
                    Sum.blue = 0;
                    for (int x = -HalfSize; x <= HalfSize; x++)
                    {
                        jj = j + x;
                        if (jj >= 0 && jj < Width)
                        {
                            Item2 = VerFiltered[i, jj];
                            Sum.red += Filter[x + HalfSize] * Item2.red;
                            Sum.green += Filter[x + HalfSize] * Item2.green;
                            Sum.blue += Filter[x + HalfSize] * Item2.blue;
                        }
                    }
                    Filtered[i, j].red = (byte)Sum.red;
                    Filtered[i, j].green = (byte)Sum.green;
                    Filtered[i, j].blue = (byte)Sum.blue;
                }

            return Filtered;
        }


        // Timers
        private static double enc_comp = 0;
        private static double bin_orig = 0;
        // Encryption
        public static byte[,] l = new byte[1000,3];

        public static RGBPixel[,] Encryption(RGBPixel[,] ImageMatrix, string initial_Seed, string Tap_Position)
        {
            enc_comp = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // handel of Intial seed zeros
            string Intial_all_zeros = "";
            Intial_all_zeros = Intial_all_zeros.PadLeft(initial_Seed.Length, '0');
            if (Intial_all_zeros != initial_Seed)
            {
                StringBuilder temp = new StringBuilder();
                for (int i = 0; i < initial_Seed.Length; i++)
                {
                    if (initial_Seed[i] == '0')
                        temp.Append('0');
                    else if (initial_Seed[i] == '1')
                        temp.Append('1');
                    else
                    {
                        int AsciiOfChar = initial_Seed[i] - '0' + 46;
                        temp.Append(Convert.ToString(AsciiOfChar, 2).PadLeft(8, '0'));
                    }
                }
                StringBuilder reg_val = new StringBuilder(temp.ToString());

                int register_size = initial_Seed.Length;
                int tap_position = Convert.ToInt32(Tap_Position);
                int shifted_bit;
                bool f = false;
                int mod = 1;
                int m = 0;
                int height = ImageOperations.GetHeight(ImageMatrix);
                int width = ImageOperations.GetWidth(ImageMatrix);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        temp.Length = 0;
                        if (f)
                            m %= mod;
                        if (!f)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                shifted_bit = reg_val[0] - '0';
                                shifted_bit ^= (reg_val[register_size - tap_position - 1] - '0');
                                reg_val = reg_val.Remove(0, 1);
                                reg_val.Append(shifted_bit.ToString());

                                temp.Append(shifted_bit.ToString());
                            }
                            if (j != 0)
                            {
                                if (!f && l[0, 0] == Convert.ToByte(temp.ToString(), 2))
                                {
                                    f = true;
                                    mod = j;
                                }
                            }
                            l[m, 0] = Convert.ToByte(temp.ToString(), 2);
                        }
                        ImageMatrix[i, j].red ^= l[m, 0];
                        temp.Length = 0;
                        if (!f)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                shifted_bit = reg_val[0] - '0';
                                shifted_bit ^= (reg_val[register_size - tap_position - 1] - '0');
                                reg_val = reg_val.Remove(0, 1);
                                reg_val.Append(shifted_bit.ToString());
                                temp.Append(shifted_bit.ToString());
                            }
                            l[m, 1] = Convert.ToByte(temp.ToString(), 2);
                        }
                        ImageMatrix[i, j].green ^= l[m, 1];
                        temp.Length = 0;
                        if (!f)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                shifted_bit = reg_val[0] - '0';
                                shifted_bit ^= (reg_val[register_size - tap_position - 1] - '0');
                                reg_val = reg_val.Remove(0, 1);
                                reg_val.Append(shifted_bit.ToString());
                                temp.Append(shifted_bit.ToString());
                            }

                            l[m, 2] = Convert.ToByte(temp.ToString(), 2);
                        }
                        ImageMatrix[i, j].blue ^= l[m, 2];
                        m++;
                    }
                }
            }
            stopwatch.Stop();
            enc_comp += stopwatch.ElapsedMilliseconds / (double)1000;
            return ImageMatrix;
        }

        // Decryption
        public static RGBPixel[,] Decryption(RGBPixel[,] ImageMatrix)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int height = ImageOperations.GetHeight(ImageMatrix);
            int width = ImageOperations.GetWidth(ImageMatrix);

            List<string> texts = ImageOperations.LoadFile(false);
            string initial_Seed = texts[1];
            string Tap_Position = texts[2];

            l = new byte[1000, 3];
            // handel of Intial seed zeros
            string Intial_all_zeros = "";
            Intial_all_zeros = Intial_all_zeros.PadLeft(initial_Seed.Length, '0');
            if (Intial_all_zeros != initial_Seed)
            {
                StringBuilder temp = new StringBuilder();
                for (int i = 0; i < initial_Seed.Length; i++)
                {
                    if (initial_Seed[i] == '0')
                        temp.Append('0');
                    else if (initial_Seed[i] == '1')
                        temp.Append('1');
                    else
                    {
                        int AsciiOfChar = initial_Seed[i] - '0' + 46;
                        temp.Append(Convert.ToString(AsciiOfChar, 2).PadLeft(8, '0'));
                    }
                }
                    StringBuilder reg_val = new StringBuilder(temp.ToString());

                    int register_size = initial_Seed.Length;
                    int tap_position = Convert.ToInt32(Tap_Position);
                    int shifted_bit;
                    bool f = false;
                    int mod = 1;
                    int m = 0;
                    for (int i = 0; i < height; i++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            temp.Length = 0;
                            if (f)
                                m %= mod;
                            if (!f)
                            {
                                for (int k = 0; k < 8; k++)
                                {
                                    shifted_bit = reg_val[0] - '0';
                                    shifted_bit ^= (reg_val[register_size - tap_position - 1] - '0');
                                    reg_val = reg_val.Remove(0, 1);
                                    reg_val.Append(shifted_bit.ToString());

                                    temp.Append(shifted_bit.ToString());
                                }
                                if (j != 0)
                                {
                                    if (!f && l[0, 0] == Convert.ToByte(temp.ToString(), 2))
                                    {
                                        f = true;
                                        mod = j;
                                    }
                                }
                                l[m, 0] = Convert.ToByte(temp.ToString(), 2);
                            }
                            ImageMatrix[i, j].red ^= l[m, 0];
                            temp.Length = 0;
                            if (!f)
                            {
                                for (int k = 0; k < 8; k++)
                                {
                                    shifted_bit = reg_val[0] - '0';
                                    shifted_bit ^= (reg_val[register_size - tap_position - 1] - '0');
                                    reg_val = reg_val.Remove(0, 1);
                                    reg_val.Append(shifted_bit.ToString());
                                    temp.Append(shifted_bit.ToString());
                                }
                                l[m, 1] = Convert.ToByte(temp.ToString(), 2);
                            }
                            ImageMatrix[i, j].green ^= l[m, 1];
                            temp.Length = 0;
                            if (!f)
                            {
                                for (int k = 0; k < 8; k++)
                                {
                                    shifted_bit = reg_val[0] - '0';
                                    shifted_bit ^= (reg_val[register_size - tap_position - 1] - '0');
                                    reg_val = reg_val.Remove(0, 1);
                                    reg_val.Append(shifted_bit.ToString());
                                    temp.Append(shifted_bit.ToString());
                                }

                                l[m, 2] = Convert.ToByte(temp.ToString(), 2);
                            }
                            ImageMatrix[i, j].blue ^= l[m, 2];
                            m++;
                        }
                    }
            }
            stopwatch.Stop();
            bin_orig += stopwatch.ElapsedMilliseconds / (double)1000;
            MessageBox.Show($"(BinFile + OrigImg) Time: {bin_orig} seconds", "Time for Decompression & Decryption");
            return ImageMatrix;
        }
         

        // Node & Priority Queue
        private class Huffman_node : IComparable<Huffman_node>
        {
            public byte C_value { get; set; }
            public int Freq { get; set; }
            public Huffman_node L { get; set; }
            public Huffman_node R { get; set; }

            public int CompareTo(Huffman_node n)
            {
                return this.Freq.CompareTo(n.Freq);
            }
        }
        private class PriorityQueue<T> where T : IComparable<T>
        {
            private List<T> node;
            public PriorityQueue()
            {
                node = new List<T>();
            }
            public int Count()
            {
                return node.Count;
            }
            public void Enqueue(T item)
            {
                node.Add(item);
                int c_inx = node.Count - 1;
                while (c_inx > 0)
                {
                    int p_inx = (c_inx - 1) / 2;
                    if (node[c_inx].CompareTo(node[p_inx]) >= 0) //< -1,> 1,== 0 less swap
                        break;
                    T temp = node[c_inx];
                    node[c_inx] = node[p_inx];
                    node[p_inx] = temp;
                    c_inx = p_inx;
                }
            }
            public T Dequeue()
            {
                int l_inx = node.Count - 1;
                T top_item = node[0];
                node[0] = node[l_inx];
                node.RemoveAt(l_inx);
                --l_inx;
                int p_inx = 0;
                while (true)
                {
                    int c_inx = p_inx * 2 + 1;
                    if (c_inx > l_inx)
                        break;
                    int right_c = c_inx + 1;
                    if (right_c <= l_inx && node[right_c].CompareTo(node[c_inx]) < 0)
                        c_inx = right_c;
                    if (node[p_inx].CompareTo(node[c_inx]) <= 0) //< -1,> 1,== 0 greter swap
                        break;
                    T temp = node[p_inx];
                    node[p_inx] = node[c_inx];
                    node[c_inx] = temp;
                    p_inx = c_inx;
                }
                return top_item;
            }
            public T Peek()
            {
                T top_item = node[0];
                return top_item;
            }
        }

        // Construct Huffman Tree
        
        private static List<string> binary_stream = new List<string>(3);
        private static List<Dictionary<byte, string>> huffman_tree = new List<Dictionary<byte, string>>(3);
        private static Dictionary<byte, string> Huffman_tree(Dictionary<byte, int> freq, int color)
        {
            PriorityQueue<Huffman_node> priorityQueue = new PriorityQueue<Huffman_node>();
            huffman_tree.Add(new Dictionary<byte, string>());

            foreach (var kv in freq)
            {
                Huffman_node node = new Huffman_node { C_value = kv.Key, Freq = kv.Value };
                priorityQueue.Enqueue(node);
            }

            while (priorityQueue.Count() > 1)
            {
                Huffman_node x = priorityQueue.Dequeue();
                Huffman_node y = priorityQueue.Dequeue();
                Huffman_node z = new Huffman_node
                {
                    Freq = x.Freq + y.Freq,
                    R = x,
                    L = y
                };
                priorityQueue.Enqueue(z);
            }
            Construct_tree(priorityQueue.Peek(), "", color);
            return huffman_tree[color];
        }
        private static void Construct_tree(Huffman_node node, string binary_code, int color)
        {
            if (node.L == null && node.R == null)
            {
                huffman_tree[color][node.C_value] = binary_code;
                return;
            }
            Construct_tree(node.L, binary_code + "0", color);
            Construct_tree(node.R, binary_code + "1", color);
        }
        private static int Compressed_size(Dictionary<byte, int> freq_p, Dictionary<byte, string> huffman_rgb)
        {
            int total_bit = 0;
            foreach (var fp in freq_p)
            {
                total_bit += fp.Value * huffman_rgb[fp.Key].Length;
            }
            return total_bit;
        }

        // Compression
        public static Tuple<List<Dictionary<byte, string>>, List<string>> Compression(RGBPixel[,] ImageMatrix)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Dictionary<byte, int> freq_r = new Dictionary<byte, int>();
            Dictionary<byte, int> freq_g = new Dictionary<byte, int>();
            Dictionary<byte, int> freq_b = new Dictionary<byte, int>();

            int Height = GetHeight(ImageMatrix);
            int Width = GetWidth(ImageMatrix);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    byte red_value = ImageMatrix[i, j].red;
                    byte green_value = ImageMatrix[i, j].green;
                    byte blue_value = ImageMatrix[i, j].blue;

                    if (!freq_r.ContainsKey(red_value))
                        freq_r[red_value] = 1;
                    else
                        freq_r[red_value]++;

                    if (!freq_g.ContainsKey(green_value))
                        freq_g[green_value] = 1;
                    else
                        freq_g[green_value]++;

                    if (!freq_b.ContainsKey(blue_value))
                        freq_b[blue_value] = 1;
                    else
                        freq_b[blue_value]++;
                }
            }

            huffman_tree.Clear();
            Dictionary<byte, string> huffman_r = new Dictionary<byte, string>();
            Dictionary<byte, string> huffman_g = new Dictionary<byte, string>();
            Dictionary<byte, string> huffman_b = new Dictionary<byte, string>();
            Parallel.Invoke(
                () => huffman_r = Huffman_tree(freq_r, 0),
                () => huffman_g = Huffman_tree(freq_g, 1),
                () => huffman_b = Huffman_tree(freq_b, 2)
            );


            binary_stream = new List<string>
            {
                "",
                "",
                ""
            };

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    sb1.Append(huffman_tree[0][ImageMatrix[i, j].red]);
                    sb2.Append(huffman_tree[1][ImageMatrix[i, j].green]);
                    sb3.Append(huffman_tree[2][ImageMatrix[i, j].blue]);
                }
            }
            binary_stream[0] = sb1.ToString();
            binary_stream[1] = sb2.ToString();
            binary_stream[2] = sb3.ToString();

            int originalSize = Height * Width * 8;
            int Compressed_size_r = Compressed_size(freq_r, huffman_r);
            int Compressed_size_g = Compressed_size(freq_g, huffman_g);
            int Compressed_size_b = Compressed_size(freq_b, huffman_b);

            double compression_ratio_r = ((double)Compressed_size_r / originalSize * 100);
            double compression_ratio_g = ((double)Compressed_size_g / originalSize * 100);
            double compression_ratio_b = ((double)Compressed_size_b / originalSize * 100);
            double avg_ratio = (compression_ratio_r + compression_ratio_g + compression_ratio_b) / 3;

            int total_compressed_bits = (Compressed_size_r + Compressed_size_g + Compressed_size_b);
            double total_compressed_byte = total_compressed_bits / 8.0;

            Tuple<List<Dictionary<byte, string>>, List<string>> compressed_image = new Tuple<List<Dictionary<byte, string>>, List<string>>(huffman_tree, binary_stream);

            stopwatch.Stop();
            enc_comp += stopwatch.ElapsedMilliseconds / (double)1000;

            return compressed_image;
        }

        // Decompression
        private static List<Dictionary<string, byte>> reverse_huffman_tree;
        public static RGBPixel[,] Decompression(string w, string h)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int Width = Convert.ToUInt16(w), Height = Convert.ToUInt16(h);

            binary_stream = new List<string>
            {
                "",
                "",
                ""
            };

            reverse_huffman_tree = new List<Dictionary<string, byte>>
            {
                new Dictionary<string, byte>(),
                new Dictionary<string, byte>(),
                new Dictionary<string, byte>()
            };

            LoadFile(true);

            RGBPixel[,] decompressed_image = new RGBPixel[Height, Width];

            StringBuilder sb = new StringBuilder();
            int iterator1 = 0, iterator2 = 0, iterator3 = 0;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    // Red
                    while (iterator1 < binary_stream[0].Length && !reverse_huffman_tree[0].ContainsKey(sb.ToString()))
                    {
                        sb.Append(binary_stream[0][iterator1]);
                        iterator1++;
                    }
                    decompressed_image[i, j].red = reverse_huffman_tree[0][sb.ToString()];
                    sb.Clear();

                    // Green
                    while (iterator2 < binary_stream[1].Length && !reverse_huffman_tree[1].ContainsKey(sb.ToString()))
                    {
                        sb.Append(binary_stream[1][iterator2]);
                        iterator2++;
                    }
                    decompressed_image[i, j].green = reverse_huffman_tree[1][sb.ToString()];
                    sb.Clear();

                    // Blue
                    while (iterator3 < binary_stream[2].Length && !reverse_huffman_tree[2].ContainsKey(sb.ToString()))
                    {
                        sb.Append(binary_stream[2][iterator3]);
                        iterator3++;
                    }
                    decompressed_image[i, j].blue = reverse_huffman_tree[2][sb.ToString()];
                    sb.Clear();
                }
            }
            stopwatch.Stop();
            bin_orig += stopwatch.ElapsedMilliseconds / (double)1000;
            return decompressed_image;
        }


        // Load & Save
        public static List<string> LoadFile(bool flag)
        {
            bin_orig = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FileStream file = new FileStream("Compressed Image.bin", FileMode.Open);
            BinaryReader br = new BinaryReader(file);
            List<string> list = new List<string>();

            if (flag == true)
            {
                long Initial_Seed_Length = br.ReadInt64();

                if (Initial_Seed_Length == 0)
                {
                    br.ReadString();
                }
                else
                {
                    br.ReadInt64();
                }

                br.ReadInt64();

                /* ##################################################################### */

                // Load Huffman Tree and Binary Stream
                Queue<int> pathLen = new Queue<int>();
                Queue<byte> colors = new Queue<byte>();
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < 3; i++)
                {
                    // Load Huffman Tree
                    int start = 0, end;

                    long dictLen = br.ReadInt64();
                    for (long j = 0; j < dictLen; j++)
                    {
                        pathLen.Enqueue(Convert.ToInt32(br.ReadByte()));
                        colors.Enqueue(br.ReadByte());
                    }

                    sb.Clear();
                    long numOfBytes = br.ReadInt64();

                    for (long j = 0; j < numOfBytes; j++)
                    {
                        sb.Append(Convert.ToString(br.ReadByte(), 2).PadLeft(8, '0'));
                    }
                    string paths = sb.ToString();

                    for (int j = 0; j < dictLen; j++)
                    {
                        end = pathLen.Dequeue();
                        reverse_huffman_tree[i][paths.Substring(start, end)] = colors.Dequeue();
                        start += end;
                    }

                    /* ##################################################################### */

                    // Load Binary Stream
                    sb.Clear();
                    numOfBytes = br.ReadInt64();

                    for (int k = 0; k < numOfBytes; k++)
                    {
                        sb.Append(Convert.ToString(br.ReadByte(), 2).PadLeft(8, '0'));
                    }
                    binary_stream[i] = sb.ToString();
                }
            }
            else    // Load Initial Seed & Tap Position
            {
                long Initial_Seed_Length = br.ReadInt64(); // Initial seed length
                list.Add(Initial_Seed_Length.ToString());

                if (Initial_Seed_Length == 0) // There is chars
                {
                    list.Add(br.ReadString());  // Initial Seed with chars
                }
                else    // There is no chars
                {
                    list.Add(br.ReadInt64().ToString().PadLeft((int)Initial_Seed_Length, '0'));    // Initial Seed without chars
                }

                long Tap_Position = br.ReadInt64();
                list.Add(Tap_Position.ToString());
            }

            file.Close();

            stopwatch.Stop();
            bin_orig += stopwatch.ElapsedMilliseconds / (double)1000;

            return list;
        }
        public static void SaveFile(string Initial_Seed, string Tap_Position, List<Dictionary<byte, string>> huffman_tree, List<string> binary_stream)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            FileStream file = new FileStream("Compressed Image.bin", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(file);

            //Saving Initial Seed & Tap Position
            try
            {
                Convert.ToInt64(Initial_Seed);
                bw.Write(Convert.ToInt64(Initial_Seed.Length));
                bw.Write(Convert.ToInt64(Initial_Seed));
            }
            catch
            {
                bw.Write(Convert.ToInt64(0));
                bw.Write(Initial_Seed);
            }

            bw.Write(Convert.ToInt64(Tap_Position));

            /* ##################################################################### */

            // Saving Huffman Tree and Binay Stream
            for (int i = 0; i < 3; i++)
            {
                // Saving Huffman Tree
                StringBuilder sb = new StringBuilder();

                bw.Write(Convert.ToInt64(huffman_tree[i].Count()));
                foreach (byte h in huffman_tree[i].Keys)
                {
                    sb.Append(huffman_tree[i][h]);
                    bw.Write(Convert.ToByte(huffman_tree[i][h].Length));
                    bw.Write(h);
                }

                string paths = sb.ToString();
                long numOfBytes = (long)Math.Ceiling((double)paths.Length / (double)8);

                bw.Write(numOfBytes);
                for (int j = 0; j < paths.Length; j += 8)
                {
                    try
                    {
                        string s = paths.Substring(j, 8);
                        byte b = Convert.ToByte(s, 2);
                        bw.Write(b);
                    }
                    catch
                    {
                        string s = paths.Substring(j).PadRight(8, '0');
                        byte b = Convert.ToByte(s, 2);
                        bw.Write(b);
                    }
                }

                /* ##################################################################### */

                // Saving Binary Stream
                numOfBytes = (long)Math.Ceiling((double)binary_stream[i].Length / (double)8);
                bw.Write(numOfBytes);

                for (int j = 0; j < binary_stream[i].Length; j += 8)
                {
                    try
                    {
                        string s = binary_stream[i].Substring(j, 8);
                        byte b = Convert.ToByte(s, 2);
                        bw.Write(b);
                    }
                    catch
                    {
                        string s = binary_stream[i].Substring(j).PadRight(8, '0');
                        byte b = Convert.ToByte(s, 2);
                        bw.Write(b);
                    }
                }
            }

            bw.Close();
            file.Close();

            stopwatch.Stop();
            enc_comp += stopwatch.ElapsedMilliseconds / (double)1000;
            MessageBox.Show($"(Enc + Comp) Time: {enc_comp} seconds", "Time for Encryption & Compression");
        }

        public static RGBPixel[,] break_encryption(RGBPixel[,] ImageMatrix)
        {
            int mx = 0;
            RGBPixel[,] res = ImageMatrix;
            int size = (int)Math.Pow(2, 8);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    string reg = Convert.ToString(i, 2);
                    int x = 0;
                    RGBPixel[,] temp = ImageMatrix;
                    HashSet<RGBPixel> Color = new HashSet<RGBPixel>();
                    while (reg.Length < 8)
                    {
                        reg = "0" + reg;
                    }
                    for (int k = 0; k < ImageMatrix.GetLength(0); k++)
                    {
                        for (int l = 0; l < ImageMatrix.GetLength(1); l++)
                        {
                            temp[k, l].red ^= Convert.ToByte(reg, 2);
                            for (int m = 0; m < 8; m++)
                            {
                                int shifted_bit = reg[0] - '0';
                                shifted_bit ^= (reg[8 - j - 1] - '0');
                                reg = reg.Remove(0, 1);
                                reg += shifted_bit.ToString();
                            }
                            temp[k, l].green ^= Convert.ToByte(reg, 2);
                            for (int m = 0; m < 8; m++)
                            {
                                int shifted_bit = reg[0] - '0';
                                shifted_bit ^= (reg[8 - j - 1] - '0');
                                reg = reg.Remove(0, 1);
                                reg += shifted_bit.ToString();
                            }
                            temp[k, l].blue ^= Convert.ToByte(reg, 2);
                            Color.Add(temp[k, l]);
                        }
                    }
                    if (Color.Count > mx)
                    {
                        mx = Color.Count;
                        res = temp;
                    }
                }
            }
            return res;
        }
    }
}