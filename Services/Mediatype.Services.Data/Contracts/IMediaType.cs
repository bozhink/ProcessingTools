namespace ProcessingTools.MediaType.Services.Data.Contracts
{
    public interface IMediaType
    {
        string FileExtension { get; set; }

        string MimeType { get; set; }

        string MimeSubtype { get; set; }
    }
}