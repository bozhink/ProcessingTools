namespace ProcessingTools.Imaging.Processors
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Imaging.Contracts.Processors;

    public class BarcodeEncoderService : IBarcodeEncoderService
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

            return Task.FromResult(image);
        }
    }
}
