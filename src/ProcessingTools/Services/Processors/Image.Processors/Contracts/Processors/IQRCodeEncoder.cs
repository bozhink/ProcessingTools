namespace ProcessingTools.Imaging.Contracts.Processors
{
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;

    public interface IQRCodeEncoder
    {
        Task<byte[]> Encode(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelPerModule);

        Task<Image> EncodeImage(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelPerModule);

        Task<string> EncodeSvg(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelPerModule);

        Task<string> EncodeBase64(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelPerModule);
    }
}
