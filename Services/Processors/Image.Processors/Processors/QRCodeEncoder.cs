namespace ProcessingTools.Image.Processors.Processors
{
    using System;
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Image.Processors.Contracts.Processors;
    using QRCoder;

    public class QRCodeEncoder : IQRCodeEncoder
    {
        public Task<byte[]> Encode(string content, int pixelPerModule = 20)
        {
            var qrcodeData = this.GetQRCodeData(content);
            var qrcode = new BitmapByteQRCode(qrcodeData);
            byte[] qrcodeImage = qrcode.GetGraphic(pixelPerModule);

            return Task.FromResult(qrcodeImage);
        }

        public Task<string> EncodeBase64(string content, int pixelPerModule = 20)
        {
            var qrcodeData = this.GetQRCodeData(content);
            var qrcode = new Base64QRCode(qrcodeData);
            string qrcodeImage = qrcode.GetGraphic(pixelPerModule);

            return Task.FromResult(qrcodeImage);
        }

        public Task<Bitmap> EncodeBitmap(string content, int pixelPerModule = 20)
        {
            var qrcodeData = this.GetQRCodeData(content);
            var qrcode = new QRCode(qrcodeData);
            Bitmap qrcodeImage = qrcode.GetGraphic(pixelPerModule);

            return Task.FromResult(qrcodeImage);
        }

        public Task<string> EncodeSvg(string content, int pixelPerModule = 20)
        {
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
