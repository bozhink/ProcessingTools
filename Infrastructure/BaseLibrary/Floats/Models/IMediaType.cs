namespace ProcessingTools.BaseLibrary.Floats.Models
{
    internal interface IMediaType
    {
        string FileExtension { get; }

        string MimeSubtype { get; }

        string MimeType { get; }
    }
}
