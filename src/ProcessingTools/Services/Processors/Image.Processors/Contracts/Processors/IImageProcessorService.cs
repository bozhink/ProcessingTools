namespace ProcessingTools.Imaging.Contracts.Processors
{
    using System.IO;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services;

    public interface IImageProcessorService : IProcessor
    {
        Task<byte[]> Resize(byte[] originalImage, int width);

        Stream Resize(Stream originalImage, int width);
    }
}
