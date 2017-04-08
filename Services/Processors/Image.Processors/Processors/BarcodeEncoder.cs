namespace ProcessingTools.Imaging.Processors
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Imaging.Contracts.Processors;

    public class BarcodeEncoder : IBarcodeEncoder
    {
        public async Task<byte[]> Encode(BarcodeType type, string content, int width, int height)
        {
            var image = await this.EncodeImage(type, content, width, height);

            var result = image.ToByteArray(ImageFormat.Bmp);

            return result;
        }

        public async Task<string> EncodeBase64(BarcodeType type, string content, int width, int height)
        {
            var image = await this.EncodeImage(type, content, width, height);

            var result = image.ToBase64String(ImageFormat.Bmp);

            return result;
        }

        public Task<Image> EncodeImage(BarcodeType type, string content, int width, int height)
        {
            var barcode = new BarcodeLib.Barcode();

            Image image;
            if (width < 1 || height < 1)
            {
                image = barcode.Encode((BarcodeLib.TYPE)((int)type), content, Color.Black, Color.White);
            }
            else
            {
                image = barcode.Encode((BarcodeLib.TYPE)((int)type), content, Color.Black, Color.White, width, height);
            }

            return Task.FromResult(image);
        }
    }
}
