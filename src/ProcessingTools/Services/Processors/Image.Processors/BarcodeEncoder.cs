namespace ProcessingTools.Imaging.Processors
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Processors.Imaging.Contracts;

    /// <summary>
    /// Default <see cref="IBarcodeEncoder"/> implementation.
    /// </summary>
    public class BarcodeEncoder : IBarcodeEncoder
    {
        public Task<byte[]> EncodeAsync(BarcodeType type, string content, int width, int height)
        {
            return Task.Run(() =>
            {
                var image = this.EncodeImage(type, content, width, height);
                return this.ImageToByteArray(image, System.Drawing.Imaging.ImageFormat.Bmp);
            });
        }

        public Task<string> EncodeBase64Async(BarcodeType type, string content, int width, int height)
        {
            return Task.Run(() =>
            {
                var image = this.EncodeImage(type, content, width, height);
                return this.ImageToBase64String(image, System.Drawing.Imaging.ImageFormat.Bmp);
            });
        }

        private Image EncodeImage(BarcodeType type, string content, int width, int height)
        {
            var barcode = new BarcodeLib.Barcode
            {
                IncludeLabel = true,
                Alignment = BarcodeLib.AlignmentPositions.CENTER,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
                LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER,
                AlternateLabel = content
            };

            Image image;
            if (width < ImagingConstants.MinimalBarcodeWidth || height < ImagingConstants.MinimalBarcodeHeight)
            {
                image = barcode.Encode((BarcodeLib.TYPE)((int)type), content);
            }
            else
            {
                image = barcode.Encode((BarcodeLib.TYPE)((int)type), content, width, height);
            }

            return image;
        }

        /// <summary>
        /// Converts <see cref="Image"/> to Base64 encoded string.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be converted.</param>
        /// <param name="format"><see cref="ImageFormat"/> of the image.</param>
        /// <returns>Base64 encoded string as image presentation.</returns>
        /// <remarks>
        /// See http://stackoverflow.com/questions/7350679/convert-a-bitmap-into-a-byte-array
        /// </remarks>
        private string ImageToBase64String(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        /// <summary>
        /// Converts <see cref="Image"/> to byte array.
        /// </summary>
        /// <param name="image"><see cref="Image"/> object to be converted.</param>
        /// <param name="format"><see cref="ImageFormat"/> of the image.</param>
        /// <returns>Byte array of the image.</returns>
        /// <remarks>
        /// See http://stackoverflow.com/questions/10889764/how-to-convert-bitmap-to-a-base64-string
        /// </remarks>
        private byte[] ImageToByteArray(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, format);
                return stream.ToArray();
            }
        }
    }
}
