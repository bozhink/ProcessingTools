// <copyright file="QRCodeEncoder.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Imaging
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts.Services.Imaging;
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
        public async Task<string> EncodeBase64Async(string content, int pixelPerModule)
        {
#if NETFRAMEWORK || NETSTANDARD2_0 || NET5_0 || NET6_0_WINDOWS

            return await Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new Base64QRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule);
            }).ConfigureAwait(false);

#else

            byte[] result = await this.EncodeAsync(content: content, pixelPerModule: pixelPerModule).ConfigureAwait(false);
            return Convert.ToBase64String(result);

#endif
        }

        /// <summary>
        /// Encodes string content to qr-code image with specified dimensions as <see cref="Image"/> object.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="pixelPerModule">Size of the image square.</param>
        /// <returns>QR-code image as <see cref="Image"/> object.</returns>
        public async Task<Image> EncodeImageAsync(string content, int pixelPerModule)
        {
#if NETFRAMEWORK || NETSTANDARD2_0 || NET5_0 || NET6_0_WINDOWS

            return await Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new QRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule) as Image;
            }).ConfigureAwait(false);
            
#else

            byte[] result = await this.EncodeAsync(content: content, pixelPerModule: pixelPerModule).ConfigureAwait(false);
            return Image.FromStream(new MemoryStream(result));

#endif
        }

        /// <summary>
        /// Encodes string content to qr-code image with specified dimensions as SVG.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        /// <param name="pixelPerModule">Size of the image square.</param>
        /// <returns>QR-code image as SVG.</returns>
        public Task<string> EncodeSvgAsync(string content, int pixelPerModule)
        {
#if NETFRAMEWORK || NETSTANDARD2_0 || NET5_0 || NET6_0_WINDOWS

            return Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new SvgQRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule);
            });

#else

            throw new NotSupportedException("QR code as SVG is supported by the library only for Windows");

#endif
        }

        private QRCodeData GetQRCodeData(string content)
        {
            QRCodeData qrcodeData;

            using (var qrcodeGenerator = new QRCodeGenerator())
            {
                qrcodeData = qrcodeGenerator.CreateQrCode(content ?? string.Empty, QRCodeGenerator.ECCLevel.Q, forceUtf8: true);
            }

            return qrcodeData;
        }
    }
}
