namespace ProcessingTools.Contracts.Files.IO
{
    using System.IO;

    public interface IFileReader
    {
        StreamReader GetReader(string fullName);

        Stream ReadToStream(string fullName);
    }
}
