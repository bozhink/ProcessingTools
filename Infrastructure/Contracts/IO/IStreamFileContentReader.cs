namespace ProcessingTools.Contracts.IO
{
    using System.IO;

    public interface IStreamFileContentReader
    {
        StreamReader GetReader(object id);

        Stream ReadToStream(object id);
    }
}
