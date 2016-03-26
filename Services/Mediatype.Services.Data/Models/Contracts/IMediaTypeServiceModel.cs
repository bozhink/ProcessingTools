namespace ProcessingTools.MediaType.Services.Data.Models.Contracts
{
    public interface IMediaTypeServiceModel
    {
        string FileExtension { get; set; }

        string MimeType { get; set; }

        string MimeSubtype { get; set; }
    }
}