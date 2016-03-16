using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Pastaci
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImageProcesses
    {
        /// <summary>
        /// Optimizes and minimizes all Image files that placed under a directory and its sub directories.
        /// </summary>
        /// <param name="path">Server.MapPath("~/images");</param>
        /// <param name="extention">".jpg"</param>
        public static void DirectoryAllFilesEncode(string path, string extention)
        {
            //try
            //{
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.jpg", SearchOption.AllDirectories); 
            foreach (FileInfo file in Files)
            {
                //var strm = file.OpenRead();
                //System.Drawing.Image image = System.Drawing.Image.FromStream(strm);
                //strm.Dispose();
                //System.Drawing.Image image = System.Drawing.Image.FromFile(file.DirectoryName + "\\" + file.Name);
                Image image = Bitmap.FromFile(file.DirectoryName + "\\" + file.Name);
                Bitmap bmp = new Bitmap(image);
                image.Dispose();
                System.IO.File.Delete(file.DirectoryName + "\\" + file.Name);
                saveTo(bmp, file.DirectoryName + "\\" + file.Name, ImageFormat.Jpeg, 99);
            }

            //}
            //catch (Exception ex)
            //{
            //return ex.Message;
            //}
            //return Completed.
        }


        #region JPG
        public static void saveAsJpg(this Image img, string savePath)
        {
            saveTo(img, savePath, ImageFormat.Jpeg, 99);
            img.Dispose();
        }
        public static void saveAsJpg(this System.IO.Stream st, string savePath, Size s)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st), s);
            saveTo(image, savePath, ImageFormat.Jpeg, 99);
            image.Dispose();
        }
        public static void saveAsJpg(this System.IO.Stream st, string savePath)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st));
            saveTo(image, savePath, ImageFormat.Jpeg, 99);
            image.Dispose();
        }
        public static void saveAsJpg(this System.IO.Stream st, string savePath, int quality)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st));
            saveTo(image, savePath, ImageFormat.Jpeg, quality);
            image.Dispose();
        }
        public static void saveAsJpg(this System.IO.Stream st, string savePath, Size s, int quality)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st), s);
            saveTo(image, savePath, ImageFormat.Jpeg, quality);
            image.Dispose();
        }
        #endregion

        #region PNG
        public static void saveAsPNG(this Image img, string savePath)
        {
            saveTo(img, savePath, ImageFormat.Png, 99);
            img.Dispose();
        }
        public static void saveAsPNG(this System.IO.Stream st, string savePath, Size s)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st), s);
            saveTo(image, savePath, ImageFormat.Png, 99);
            image.Dispose();
        }
        public static void saveAsPNG(this System.IO.Stream st, string savePath)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st));
            saveTo(image, savePath, ImageFormat.Png, 99);
            image.Dispose();
        }
        public static void saveAsPNG(this System.IO.Stream st, string savePath, int quality)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st));
            saveTo(image, savePath, ImageFormat.Png, quality);
            image.Dispose();
        }
        public static void saveAsPNG(this System.IO.Stream st, string savePath, Size s, int quality)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st), s);
            saveTo(image, savePath, ImageFormat.Png, quality);
            image.Dispose();
        }
        #endregion

        #region GIF
        public static void saveAsGIF(this Image img, string savePath)
        {
            saveTo(img, savePath, ImageFormat.Gif, 99);
            img.Dispose();
        }
        public static void saveAsGIF(this System.IO.Stream st, string savePath, Size s)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st), s);
            saveTo(image, savePath, ImageFormat.Gif, 99);
            image.Dispose();
        }
        public static void saveAsGIF(this System.IO.Stream st, string savePath)
        {
            System.Drawing.Image image = (System.Drawing.Image)new Bitmap(System.Drawing.Image.FromStream(st));
            saveTo(image, savePath, ImageFormat.Gif, 99);
            image.Dispose();
        }
        #endregion


        //From Anwar Javed's answer
        //http://stackoverflow.com/users/1005359/anwar-javed
        /// <summary>
        /// Saves the image to specified stream and format.
        /// </summary>
        /// <param name="image">The image to save.</param>
        /// <param name="outputStream">The stream to used.</param>
        /// <param name="format">The format of new image.</param>
        /// <param name="quality">The quality of the image in percent.</param>
        public static void saveTo(System.Drawing.Image image, string outputStream, ImageFormat format, int quality)
        {
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            if (format == ImageFormat.Gif)
            {
                image.Save(outputStream, ImageFormat.Gif);
            }
            else if (format == ImageFormat.Jpeg)
            {
                image.Save(outputStream, encoders[1], encoderParameters);
            }
            else if (format == ImageFormat.Png)
            {
                image.Save(outputStream, encoders[4], encoderParameters);
            }
            else if (format == ImageFormat.Bmp)
            {
                image.Save(outputStream, encoders[0], encoderParameters);
            }
            else
            {
                image.Save(outputStream, format);
            }
        }
    }
}
