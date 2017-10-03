namespace ProcessingTools.Processors.Contracts.Models.Floats
{
    public interface IMediaType
    {
        string FileExtension { get; }

        string MimeSubtype { get; }

        string MimeType { get; }
    }
}
