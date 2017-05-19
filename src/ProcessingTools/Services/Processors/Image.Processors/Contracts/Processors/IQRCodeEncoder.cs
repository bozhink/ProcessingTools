namespace ProcessingTools.Imaging.Contracts.Processors
{
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Services;

    public interface IQRCodeEncoder : IProcessor
    {
        Task<byte[]> Encode(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelPerModule);

        Task<Image> EncodeImage(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelPerModule);

        Task<string> EncodeSvg(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelPerModule);

        Task<string> EncodeBase64(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelPerModule);
    }
}