using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Image rescaling
    /// </summary>
    public class RescaleImageTest : ITest
    {
        public void Run()
        {
            // TODO:
            // Grab an image from a public URL and write a function thats rescale the image to a desired format
            // The use of 3rd party plugins is permitted
            // For example: 100x80 (thumbnail) and 1200x1600 (preview)


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Rescale Image Test");
            Console.WriteLine();


            byte[] byteImage = download();

            if (byteImage==null || byteImage.Length==0)
            {
                Console.WriteLine(@"No picture available..");
                return;
            }


            var bitmap = GetBitmapFromBytes(byteImage);

            if (bitmap == null)
            {
                Console.WriteLine(@"No picture available");
                return;
            }


            Console.WriteLine(@"Image correctly downloaded and converted..");

            Console.WriteLine(@"Original Image - Height:{0}  Width: {1}", bitmap.Height, bitmap.Width);

            Console.WriteLine(@"Resizing Image..");

            var resizedImage = ResizeImage(bitmap, 30, 50);
            
            if (resizedImage == null)
            {
                Console.WriteLine(@"No picture available");
                return;
            }

            Console.WriteLine(@"Resized Image - Height:{0}  Width: {1}", resizedImage.Height, resizedImage.Width);

            Console.WriteLine();
            Console.WriteLine("------------------------------------");
        }


        private byte[] download()
        {
            try
            {
                WebClient client = new WebClient();

                byte[] arr = client.DownloadData("http://i64.tinypic.com/29kopx4.jpg");

                return arr;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private Bitmap GetBitmapFromBytes(byte[] bytes)
        {
            if (bytes ==null || !bytes.Any())
                return null;

            Bitmap bitmap;

            using (var ms = new MemoryStream(bytes))
            {
                bitmap = new Bitmap(ms);
            }

            return bitmap;
        }

        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            if (image == null)
            {
                return null;
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
