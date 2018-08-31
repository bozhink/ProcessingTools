// <copyright file="ImagesHelper.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Code.Images
{
    using System.Linq;
    using System.Text;
    using ProcessingTools.Enumerations;

    /// <summary>
    /// Images helper.
    /// </summary>
    public static class ImagesHelper
    {
        /// <summary>
        /// Gets image format.
        /// </summary>
        /// <param name="bytes">Image as byte array.</param>
        /// <returns>Image format of the image.</returns>
        /// <remarks>
        /// See https://www.codeproject.com/Articles/1256591/Upload-Image-to-NET-Core-2-1-API
        /// </remarks>
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            // BMP
            var bmp = Encoding.ASCII.GetBytes("BM");
            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
            {
                return ImageFormat.Bmp;
            }

            // GIF
            var gif = Encoding.ASCII.GetBytes("GIF");
            if (gif.SequenceEqual(bytes.Take(gif.Length)))
            {
                return ImageFormat.Gif;
            }

            // PNG
            var png = new byte[] { 137, 80, 78, 71 };
            if (png.SequenceEqual(bytes.Take(png.Length)))
            {
                return ImageFormat.Png;
            }

            // TIFF
            var tiff = new byte[] { 73, 73, 42 };
            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
            {
                return ImageFormat.Tiff;
            }

            // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };
            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
            {
                return ImageFormat.Tiff;
            }

            // JPEG
            var jpeg = new byte[] { 255, 216, 255, 224 };
            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
            {
                return ImageFormat.Jpeg;
            }

            // JPEG CANON
            var jpeg2 = new byte[] { 255, 216, 255, 225 };
            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
            {
                return ImageFormat.Jpeg;
            }

            return ImageFormat.Unknown;
        }
    }
}
