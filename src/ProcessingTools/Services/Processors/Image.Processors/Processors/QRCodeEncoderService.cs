namespace ProcessingTools.Imaging.Processors
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Imaging.Contracts.Processors;
    using QRCoder;

    public class QRCodeEncoderService : IQRCodeEncoderService
    {
        public Task<byte[]> Encode(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule)
        {
            if (pixelPerModule < ImagingConstants.MinimalQRCodePixelsPerModule)
            {
                pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule;
            }

            var qrcodeData = this.GetQRCodeData(content);
            var qrcode = new BitmapByteQRCode(qrcodeData);
            byte[] qrcodeImage = qrcode.GetGraphic(pixelPerModule);

            return Task.FromResult(qrcodeImage);
        }

        public Task<string> EncodeBase64(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule)
        {
            if (pixelPerModule < ImagingConstants.MinimalQRCodePixelsPerModule)
            {
                pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule;
            }

            var qrcodeData = this.GetQRCodeData(content);
            var qrcode = new Base64QRCode(qrcodeData);
            string qrcodeImage = qrcode.GetGraphic(pixelPerModule);

            return Task.FromResult(qrcodeImage);
        }

        public Task<Image> EncodeImage(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule)
        {
            if (pixelPerModule < ImagingConstants.MinimalQRCodePixelsPerModule)
            {
                pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule;
            }

            var qrcodeData = this.GetQRCodeData(content);
            var qrcode = new QRCode(qrcodeData);
            Bitmap qrcodeImage = qrcode.GetGraphic(pixelPerModule);

            return Task.FromResult<Image>(qrcodeImage);
        }

        public Task<string> EncodeSvg(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule)
        {
            if (pixelPerModule < ImagingConstants.MinimalQRCodePixelsPerModule)
            {
                pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule;
            }

            var qrcodeData = this.GetQRCodeData(content);
            var qrcode = new SvgQRCode(qrcodeData);
            string qrcodeImage = qrcode.GetGraphic(pixelPerModule);

            return Task.FromResult(qrcodeImage);
        }

        private QRCodeData GetQRCodeData(string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var qrcodeGenerator = new QRCodeGenerator();
            var qrcodeData = qrcodeGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q, forceUtf8: true);
            return qrcodeData;
        }
    }
}
