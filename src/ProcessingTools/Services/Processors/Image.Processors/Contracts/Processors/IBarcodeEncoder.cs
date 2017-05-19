namespace ProcessingTools.Imaging.Contracts.Processors
{
    using System.Drawing;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public interface IBarcodeEncoder : IProcessor
    {
        Task<byte[]> Encode(BarcodeType type, string content, int width, int height);

        Task<Image> EncodeImage(BarcodeType type, string content, int width, int height);

        Task<string> EncodeBase64(BarcodeType type, string content, int width, int height);
    }
}