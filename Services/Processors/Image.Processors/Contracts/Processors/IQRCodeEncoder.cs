namespace ProcessingTools.Imaging.Contracts.Processors
{
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Imaging.Constants;

    public interface IQRCodeEncoder
    {
        Task<byte[]> Encode(string content, int pixelPerModule = DefaultConstants.PixelPerModule);

        Task<Image> EncodeImage(string content, int pixelPerModule = DefaultConstants.PixelPerModule);

        Task<string> EncodeSvg(string content, int pixelPerModule = DefaultConstants.PixelPerModule);

        Task<string> EncodeBase64(string content, int pixelPerModule = DefaultConstants.PixelPerModule);
    }
}
