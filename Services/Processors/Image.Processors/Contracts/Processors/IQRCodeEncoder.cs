namespace ProcessingTools.Image.Processors.Contracts.Processors
{
    using System.Drawing;
    using System.Threading.Tasks;

    public interface IQRCodeEncoder
    {
        Task<byte[]> Encode(string content, int pixelPerModule = 20);

        Task<Bitmap> EncodeBitmap(string content, int pixelPerModule = 20);

        Task<string> EncodeSvg(string content, int pixelPerModule = 20);

        Task<string> EncodeBase64(string content, int pixelPerModule = 20);

    }
}
