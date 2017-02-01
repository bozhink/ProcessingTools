namespace ProcessingTools.Processors.Contracts.Models.Floats
{
    internal interface IMediaType
    {
        string FileExtension { get; }

        string MimeSubtype { get; }

        string MimeType { get; }
    }
}
