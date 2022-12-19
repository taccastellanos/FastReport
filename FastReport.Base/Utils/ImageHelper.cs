using System;


using System.IO;
using System.Net;

namespace FastReport.Utils
{
    internal static class ImageHelper
    {
        public static SkiaSharp.SKBitmap CloneBitmap(SkiaSharp.SKImage source)
        {
            if (source == null)
                return null;

            SkiaSharp.SKBitmap image = new SkiaSharp.SKBitmap(source.Width, source.Height);
            //image.SetResolution(source.HorizontalResolution, source.VerticalResolution);
            using (var g = new SkiaSharp.SKCanvas(image))
            {
                g.DrawImage(source, 0, 0);
            }
            return image;

            // this can throw OutOfMemory when creating a grayscale image from a cloned bitmap
            //      return source.Clone() as Bitmap;
        }

        public static void Save(SkiaSharp.SKBitmap image, Stream stream)
        {
            Save(image, stream, SkiaSharp.SKEncodedImageFormat.Png);
        }

        public static void Save(SkiaSharp.SKBitmap image, string fileName, SkiaSharp.SKEncodedImageFormat format)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                Save(image, stream, format);
            }
        }

        public static void Save(SkiaSharp.SKBitmap image, Stream stream, SkiaSharp.SKEncodedImageFormat format)
        {
            
            if (image == null)
                return;
            
                if (format == SkiaSharp.SKEncodedImageFormat.Ico)
                {
                    SaveAsIcon(image, stream, true);
                }
                else
                {
                    image.Encode(stream, format, 95);
                }
            
        }

        public static bool SaveAndConvert(SkiaSharp.SKBitmap image, Stream stream, SkiaSharp.SKEncodedImageFormat format)
        {
            

            if (image == null)
                return false;
            if (format == SkiaSharp.SKEncodedImageFormat.Jpeg || format == SkiaSharp.SKEncodedImageFormat.Gif
                 || format == SkiaSharp.SKEncodedImageFormat.Bmp
                || format == SkiaSharp.SKEncodedImageFormat.Png
                )
            {
                if (image is SkiaSharp.SKBitmap)
                {
                    if (format == SkiaSharp.SKEncodedImageFormat.Bmp)
                        throw new Exception(Res.Get("Export,Image,ImageParceFormatException"));
                    image.Encode(stream, format, 95);
                    return true;
                }
                //from mf to bitmap
                //using (Metafile metafile = image as Metafile)
                
                    //TODOimage.SetPixel(96F, 96F, SkiaSharp.SKColors.Transparent);
                    using (var g = new SkiaSharp.SKCanvas(image))
                    {
                        //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.DrawBitmap(image, 0, 0);
                        g.Dispose();
                    }
                    
                    image.Encode(stream, format, 95);


                
                return true;

            }
            else if (format == SkiaSharp.SKEncodedImageFormat.Ico)
            {
                return SaveAsIcon(image, stream, true);
            }
            /*
            else if (format == SkiaSharp.SKEncodedImageFormat.Wmf || format == SkiaSharp.SKEncodedImageFormat.Emf)
            {
                if (image is Metafile)
                {
                    Metafile emf = null;
                    using (SkiaSharp.SKBitmap bmp = new Bitmap(1, 1))
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        IntPtr hdc = g.GetHdc();
                        emf = new Metafile(stream, hdc);
                        g.ReleaseHdc(hdc);
                    }
                    using (Graphics g = Graphics.FromImage(emf))
                    {
                        g.DrawImage(image, 0, 0);
                    }
                    return true;
                }
            }*/
            //throw new Exception(Res.Get("Export,Image,ImageParceFormatException")); // we cant convert image to exif or from bitmap to mf 
            return false;
        }

        public static byte[] Load(string fileName)
        {
            if (!String.IsNullOrEmpty(fileName))
                return File.ReadAllBytes(fileName);
            return null;
        }

        public static SkiaSharp.SKBitmap  Load(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                try
                {

                    // TODO memory leaks image converter
                    return SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromEncodedData(SkiaSharp.SKData.Create( new MemoryStream(bytes))));


                }
                catch
                {
                    SkiaSharp.SKBitmap errorBmp = new SkiaSharp.SKBitmap(10, 10);
                    
                    using (var g = new SkiaSharp.SKCanvas(errorBmp))
                    {
                        SkiaSharp.SKPaint p = new SkiaSharp.SKPaint();
                        p.Color = SkiaSharp.SKColors.Red;
                        p.Style= SkiaSharp.SKPaintStyle.Stroke; 
                        
                        g.DrawLine( 0, 0, 10, 10, p);
                        g.DrawLine( 0, 10, 10, 0, p);
                    }
                    return errorBmp;
                }
            }
            return null;
        }

        public static byte[] LoadURL(string url)
        {
            if (!String.IsNullOrEmpty(url))
            {
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
                using (WebClient web = new WebClient())
                {
                    return web.DownloadData(url);
                }
            }
            return null;
        }

        public static SkiaSharp.SKBitmap GetTransparentBitmap(SkiaSharp.SKImage source, float transparency)
        {
            if (source == null)
                return null;
/*TODO
            ColorMatrix colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = 1 - transparency;
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(
               colorMatrix,
               ColorMatrixFlag.Default,
               ColorAdjustType.Bitmap);
*/
            int width = source.Width;
            int height = source.Height;
            SkiaSharp.SKBitmap image = new SkiaSharp.SKBitmap(width, height);
            /*TODO image.SetResolution(source.HorizontalResolution, source.VerticalResolution);

            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.Transparent);
                g.DrawImage(
                  source,
                  new Rectangle(0, 0, width, height),
                  0, 0, width, height,
                  GraphicsUnit.Pixel,
                  imageAttributes);
            }*/
            return image;
        }

        public static SkiaSharp.SKBitmap GetGrayscaleBitmap(SkiaSharp.SKBitmap source)
        {
            //SkiaSharp.SKBitmap grayscaleBitmap = new SkiaSharp.SKBitmap(source.Info);

            // Red should be converted to (R*.299)+(G*.587)+(B*.114)
            // Green should be converted to (R*.299)+(G*.587)+(B*.114)
            // Blue should be converted to (R*.299)+(G*.587)+(B*.114)
            // Alpha should stay the same.
            /*TODO
            ColorMatrix grayscaleMatrix = new ColorMatrix(new float[][]{
                                                          new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                                                          new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                                                          new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                                                          new float[] {     0,      0,      0, 1, 0},
                                                          new float[] {     0,      0,      0, 0, 1}});

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(grayscaleMatrix);

            // Use a SkiaSharp.SKGraphics object from the new image
            using (Graphics graphics = Graphics.FromImage(grayscaleBitmap))
            {
                // Draw the original image using the ImageAttributes we created
                graphics.DrawImage(source,
                    new Rectangle(0, 0, grayscaleBitmap.Width, grayscaleBitmap.Height),
                    0, 0, grayscaleBitmap.Width, grayscaleBitmap.Height,
                    GraphicsUnit.Pixel, attributes);
            }*/

            return source;
        }

        /// <summary>
        /// Converts a PNG image to a icon (ico)
        /// </summary>
        /// <param name="image">The input image</param>
        /// <param name="output">The output stream</param>
        /// <param name="preserveAspectRatio">Preserve the aspect ratio</param>
        /// <returns>Wether or not the icon was succesfully generated</returns>
        public static bool SaveAsIcon(SkiaSharp.SKBitmap image, Stream output, bool preserveAspectRatio = false)
        {
            int size = 256;
            int width = size, height = size;
            if (preserveAspectRatio)
            {
                if (image.Width > image.Height)
                    height = (image.Height / image.Width) * size;
                else
                    width = (image.Width / image.Height) * size;
            }

            var newBitmap = image;
            newBitmap.Resize(new SkiaSharp.SKSizeI(width,height), SkiaSharp.SKFilterQuality.High);
            if (newBitmap == null)
                return false;

            // save the resized png into a memory stream for future use
            using (MemoryStream memoryStream = new MemoryStream())
            {
                newBitmap.Encode(memoryStream, SkiaSharp.SKEncodedImageFormat.Png, 100);
                
                var iconWriter = new BinaryWriter(output);
                if (output == null || iconWriter == null)
                    return false;

                // 0-1 reserved, 0
                iconWriter.Write((byte)0);
                iconWriter.Write((byte)0);

                // 2-3 image type, 1 = icon, 2 = cursor
                iconWriter.Write((short)1);

                // 4-5 number of images
                iconWriter.Write((short)1);

                // image entry 1
                // 0 image width
                iconWriter.Write((byte)width);
                // 1 image height
                iconWriter.Write((byte)height);

                // 2 number of colors
                iconWriter.Write((byte)0);

                // 3 reserved
                iconWriter.Write((byte)0);

                // 4-5 color planes
                iconWriter.Write((short)0);

                // 6-7 bits per pixel
                iconWriter.Write((short)32);

                // 8-11 size of image data
                iconWriter.Write((int)memoryStream.Length);

                // 12-15 offset of image data
                iconWriter.Write((int)(6 + 16));

                // write image data
                // png data must contain the whole png data file
                iconWriter.Write(memoryStream.ToArray());

                iconWriter.Flush();
            }

            return true;
        }
    }

    public static class ImageExtension
    {
        /// <summary>
        /// Returns an SkiaSharp.SKImage  format.
        /// </summary>
        public static SkiaSharp.SKEncodedImageFormat GetImageFormat(this SkiaSharp.SKImage  bitmap)
        {
            /*TODO
            if (bitmap == null || bitmap.EncodedData == null)
                return null;
            SkiaSharp.SKEncodedImageFormat format = null;
            if (ImageFormat.Jpeg.Equals(bitmap.RawFormat))
            {
                format = SkiaSharp.SKEncodedImageFormat.Jpeg;
            }
            else if (ImageFormat.Gif.Equals(bitmap.RawFormat))
            {
                format = SkiaSharp.SKEncodedImageFormat.Gif;
            }
            else if (ImageFormat.Png.Equals(bitmap.RawFormat))
            {
                format = SkiaSharp.SKEncodedImageFormat.Png;
            }
            else if (ImageFormat.Emf.Equals(bitmap.RawFormat))
            {
                format = SkiaSharp.SKEncodedImageFormat.Emf;
            }
            else if (ImageFormat.Icon.Equals(bitmap.RawFormat))
            {
                format = SkiaSharp.SKEncodedImageFormat.Icon;
            }
            else if (ImageFormat.Tiff.Equals(bitmap.RawFormat))
            {
                format = SkiaSharp.SKEncodedImageFormat.Tiff;
            }
            else if (ImageFormat.Bmp.Equals(bitmap.RawFormat) || SkiaSharp.SKEncodedImageFormat.MemoryBmp.Equals(bitmap.RawFormat)) // MemoryBmp format raises a GDI exception
            {
                format = SkiaSharp.SKEncodedImageFormat.Bmp;
            }
            else if (ImageFormat.Wmf.Equals(bitmap.RawFormat)) 
            {
                format = SkiaSharp.SKEncodedImageFormat.Wmf;
            }
            if (format != null)
                return format;*/
            return SkiaSharp.SKEncodedImageFormat.Png;
        }
    }
}
