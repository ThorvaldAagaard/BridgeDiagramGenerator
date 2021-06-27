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

namespace BridgeDiagramGenerator
{
    public partial class Diagram : Form
    {
        public BridgeDiagram bd;
        public Diagram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = bd.FileName;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bd.FileName = saveFileDialog1.FileName;
                ExportToBmp(bd.FileName);
            }
        }

        public  Image Render()
        {

           var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format32bppArgb);
            Image image = new Bitmap(bitmap);


            // create memory buffer from desktop handle that supports alpha channel
            IntPtr dib;
        var memoryHdc = ImageHandler.CreateMemoryHdc(IntPtr.Zero, image.Width, image.Height, out dib);
        try
        {
            // create memory buffer graphics to use for HTML rendering
            using (var memoryGraphics = Graphics.FromHdc(memoryHdc))
            {
                // must not be transparent background 
                memoryGraphics.Clear(Color.White);

                    // execute GDI text rendering
                    var suitFont = new Font("GillSans Bridge", 28);
                    var cardFont = new Font("Lucida Sans", 28);
                    var suits = new String[] { "[","]","{","}" };
                TextRenderer.DrawText(memoryGraphics, bd.Name, new Font("Cambria", 32, FontStyle.Bold), new Point(15, 25), Color.Black);
                    for (int i =0; i <4;i++)
                    {
                        TextRenderer.DrawText(memoryGraphics, suits[i], suitFont, new Point(310, 90 + (50 * i)), Color.Black);
                        TextRenderer.DrawText(memoryGraphics, bd.North[i], cardFont, new Point(342, 90 + (50 * i)), Color.Black);
                        TextRenderer.DrawText(memoryGraphics, suits[i], suitFont, new Point(310, 460 + (50 * i)), Color.Black);
                        TextRenderer.DrawText(memoryGraphics, bd.South[i], cardFont, new Point(342, 460 + (50 * i)), Color.Black);
                        TextRenderer.DrawText(memoryGraphics, suits[i], suitFont, new Point(70, 270 + (50 * i)), Color.Black);
                        TextRenderer.DrawText(memoryGraphics, bd.West[i], cardFont, new Point(102, 270 + (50 * i)), Color.Black);
                        TextRenderer.DrawText(memoryGraphics, suits[i], suitFont, new Point(550, 270 + (50 * i)), Color.Black);
                        TextRenderer.DrawText(memoryGraphics, bd.East[i], cardFont, new Point(582, 270 + (50 * i)), Color.Black);
                    }
                }

                // copy from memory buffer to image
                using (var imageGraphics = Graphics.FromImage(image))
            {
                var imgHdc = imageGraphics.GetHdc();
                    ImageHandler.BitBlt(imgHdc, 0, 0, image.Width, image.Height, memoryHdc, 0, 0, 0x00CC0020);
    imageGraphics.ReleaseHdc(imgHdc);
            }
        }
        finally
        {
                // release memory buffer
                ImageHandler.DeleteObject(dib);
                ImageHandler.DeleteDC(memoryHdc);
        }

        return image;
    }


        public void ExportToBmp(string path)
        {
            pictureBox1.BorderStyle = BorderStyle.None;
            using (var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                pictureBox1.DrawToBitmap(bitmap, pictureBox1.ClientRectangle);
                ImageFormat imageFormat = null;
                bitmap.SetResolution(300, 300);
                var extension = Path.GetExtension(path);
                switch (extension)
                {
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".jpeg":
                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case ".gif":
                        imageFormat = ImageFormat.Gif;
                        break;
                    case ".tif":
                        imageFormat = ImageFormat.Tiff;
                        break;
                    case ".tiff":
                        imageFormat = ImageFormat.Tiff;
                        break;
                    default:
                        throw new NotSupportedException("File extension is not supported");
                }

                bitmap.Save(path, imageFormat);
            }
        }

        private void Diagram_Load(object sender, EventArgs e)
        {
            Image img = Render();
            pictureBox1.Image = img;

        }
    }
}
