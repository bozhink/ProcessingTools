namespace ProcessingTools.Imaging.Contracts.Processors
{
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Services;

    public interface IQRCodeEncoderService : IProcessor
    {
        Task<byte[]> Encode(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule);

        Task<Image> EncodeImage(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule);

        Task<string> EncodeSvg(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule);

        Task<string> EncodeBase64(string content, int pixelPerModule = ImagingConstants.DefaultQRCodePixelsPerModule);
    }
}