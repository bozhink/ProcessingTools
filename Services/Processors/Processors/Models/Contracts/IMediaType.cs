namespace ProcessingTools.Processors.Models.Contracts
{
    internal interface IMediaType
    {
        string FileExtension { get; }

        string MimeSubtype { get; }

        string MimeType { get; }
    }
}
