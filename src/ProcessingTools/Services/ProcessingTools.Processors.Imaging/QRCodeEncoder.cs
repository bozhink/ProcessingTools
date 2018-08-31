// <copyright file="QRCodeEncoder.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Imaging
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Processors.Imaging.Contracts;
    using QRCoder;

    /// <summary>
    /// QR Code Encoder.
    /// </summary>
    public class QRCodeEncoder : IQRCodeEncoder
    {
        /// <summary>
        /// Encodes string content to qr-code image with specified dimensions as byte array.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="pixelPerModule">Size of the image square.</param>
        /// <returns>QR-code image as byte array.</returns>
        public Task<byte[]> EncodeAsync(string content, int pixelPerModule)
        {
            return Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new BitmapByteQRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule);
            });
        }

        /// <summary>
        /// Encodes string content to qr-code image with specified dimensions as Base64 encoded string.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="pixelPerModule">Size of the image square.</param>
        /// <returns>QR-code image as Base64 string.</returns>
        public Task<string> EncodeBase64Async(string content, int pixelPerModule)
        {
            return Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new Base64QRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule);
            });
        }

        /// <summary>
        /// Encodes string content to qr-code image with specified dimensions as <see cref="Image"/> object.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="pixelPerModule">Size of the image square.</param>
        /// <returns>QR-code image as <see cref="Image"/> object.</returns>
        public Task<Image> EncodeImageAsync(string content, int pixelPerModule)
        {
            return Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new QRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule) as Image;
            });
        }

        /// <summary>
        /// Encodes string content to qr-code image with specified dimensions as SVG.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="pixelPerModule">Size of the image square.</param>
        /// <returns>QR-code image as SVG.</returns>
        public Task<string> EncodeSvgAsync(string content, int pixelPerModule)
        {
            return Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new SvgQRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule);
            });
        }

        private QRCodeData GetQRCodeData(string content)
        {
            var qrcodeGenerator = new QRCodeGenerator();
            var qrcodeData = qrcodeGenerator.CreateQrCode(content ?? string.Empty, QRCodeGenerator.ECCLevel.Q, forceUtf8: true);
            return qrcodeData;
        }
    }
}
