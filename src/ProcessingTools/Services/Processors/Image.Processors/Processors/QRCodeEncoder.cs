namespace ProcessingTools.Imaging.Processors
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Processors.Contracts.Imaging;
    using QRCoder;

    public class QRCodeEncoder : IQRCodeEncoder
    {
        public Task<byte[]> Encode(string content, int pixelPerModule)
        {
            return Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new BitmapByteQRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule);
            });
        }

        public Task<string> EncodeBase64(string content, int pixelPerModule)
        {
            return Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new Base64QRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule);
            });
        }

        public Task<Image> EncodeImage(string content, int pixelPerModule)
        {
            return Task.Run(() =>
            {
                pixelPerModule = Math.Max(pixelPerModule, ImagingConstants.MinimalQRCodePixelsPerModule);

                var qrcodeData = this.GetQRCodeData(content);
                var qrcode = new QRCode(qrcodeData);
                return qrcode.GetGraphic(pixelPerModule) as Image;
            });
        }

        public Task<string> EncodeSvg(string content, int pixelPerModule)
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
