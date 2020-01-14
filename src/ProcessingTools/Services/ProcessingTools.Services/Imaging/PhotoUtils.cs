// <copyright file="PhotoUtils.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Imaging
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// Photo utilities.
    /// </summary>
    public static class PhotoUtils
    {
        /// <summary>
        /// Inscribe image to square.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be inscribed.</param>
        /// <param name="size">The size of the square.</param>
        /// <returns>Inscribed image.</returns>
        public static Image Inscribe(Image image, int size)
        {
            return Inscribe(image, size, size);
        }

        /// <summary>
        /// Inscribe image to rectangle.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be inscribed.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <returns>Inscribed image.</returns>
        public static Image Inscribe(Image image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(result))
            {
                double factor = 1.0 * width / image.Width;
                if (image.Height * factor < height)
                {
                    factor = 1.0 * height / image.Height;
                }

                Size size = new Size((int)(width / factor), (int)(height / factor));
                Point sourceLocation = new Point((image.Width - size.Width) / 2, (image.Height - size.Height) / 2);

                SmoothGraphics(graphics);
                graphics.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(sourceLocation, size), GraphicsUnit.Pixel);
            }

            return result;
        }

        /// <summary>
        /// Get reduced image.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be reduced.</param>
        /// <param name="extremeWidth">The extreme width.</param>
        /// <param name="extremeHeight">The extreme height.</param>
        /// <returns>The reduced image.</returns>
        public static Image GetReducedImage(Image image, int extremeWidth, int extremeHeight)
        {
            double ratio = (double)image.Width / (double)image.Height;
            Size size = new Size(extremeWidth, (int)(extremeWidth / ratio));
            if (size.Height > extremeHeight)
            {
                size = new Size((int)(extremeHeight * ratio), extremeHeight);
            }

            return new Bitmap(image, size);
        }

        /// <summary>
        /// Draw watermark text.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be updated.</param>
        /// <param name="text">The watermark text.</param>
        public static void DrawWatermarkText(Image image, string text)
        {
            DrawWatermarkText(image, text, "Arial");
        }

        /// <summary>
        /// Draw watermark text.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be updated.</param>
        /// <param name="text">The watermark text.</param>
        /// <param name="fontName">The name of the font.</param>
        public static void DrawWatermarkText(Image image, string text, string fontName)
        {
            using (Graphics graphics = CreateGraphics(image))
            {
                DrawWatermarkText(graphics, text, fontName);
            }
        }

        /// <summary>
        /// Draw watermark text.
        /// </summary>
        /// <param name="graphics"><see cref="Graphics"/> object to be updated.</param>
        /// <param name="text">The watermark text.</param>
        /// <param name="fontName">The name of the font.</param>
        public static void DrawWatermarkText(Graphics graphics, string text, string fontName)
        {
            int imageHeight = (int)graphics.VisibleClipBounds.Height;
            int imageWidth = (int)graphics.VisibleClipBounds.Width;
            int maxTextWidth = (int)(imageHeight * 0.4);
            int[] fontSizes = new int[] { 72, 48, 36, 24, 18, 18, 14, 12, 10 };
            Font font = null;
            foreach (int fontSize in fontSizes)
            {
                font = new Font(fontName, fontSize, GraphicsUnit.Pixel);
                if (graphics.MeasureString(text, font).Width <= maxTextWidth)
                {
                    break;
                }
            }

            GraphicsState state = graphics.Save();
            SmoothGraphics(graphics);
            graphics.RotateTransform(-90);

            if (font != null)
            {
                float padding = font.Size / 2;
                graphics.TranslateTransform(-imageHeight + padding, imageWidth - font.GetHeight() - padding);
                graphics.TextContrast = 12;
                graphics.PageUnit = font.Unit;

                using (var blackBrush = new SolidBrush(Color.FromArgb(120, Color.Black)))
                {
                    using (var whiteBrush = new SolidBrush(Color.FromArgb(120, Color.White)))
                    {
                        graphics.DrawString(text, font, blackBrush, 1, 1);
                        graphics.DrawString(text, font, whiteBrush, 0, 0);
                    }
                }
            }

            graphics.Restore(state);
        }



        /// <summary>
        /// Save image to JPEG format.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be exported.</param>
        /// <param name="output">The output stream.</param>
        public static void SaveToJpeg(Image image, Stream output)
        {
            image.Save(output, ImageFormat.Jpeg);
        }

        /// <summary>
        /// Save image to JPEG format.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be exported.</param>
        /// <param name="fileName">The name of the output file.</param>
        public static void SaveToJpeg(Image image, string fileName)
        {
            image.Save(fileName, ImageFormat.Jpeg);
        }

        private static void SmoothGraphics(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        private static Graphics CreateGraphics(Image image)
        {
            image = IsIndexedPaxelFormat(image.PixelFormat) ? ConvertIndexedBitmapToARGB(image) : image;
            Graphics graphics = Graphics.FromImage(image);
            SmoothGraphics(graphics);
            return graphics;
        }

        private static bool IsIndexedPaxelFormat(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Format1bppIndexed:
                    return true;

                case PixelFormat.Format4bppIndexed:
                    return true;

                case PixelFormat.Format8bppIndexed:
                    return true;

                case PixelFormat.Indexed:
                    return true;

                default:
                    return false;
            }
        }

        private static Bitmap ConvertIndexedBitmapToARGB(Image image)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(image, new Rectangle(0, 0, result.Width, result.Height), 0, 0, result.Width, result.Height, GraphicsUnit.Pixel);
                return result;
            }
        }
    }
}
