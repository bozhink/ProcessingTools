namespace ProcessingTools.MediaType.Services.Data.Models
{
    using Contracts;

    internal class MediaTypeDataServiceResponseModel : IMediaType
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}