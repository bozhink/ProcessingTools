namespace ProcessingTools.MediaType.Services.Data.Models
{
    using Contracts;

    internal class MediaTypeServiceModel : IMediaTypeServiceModel
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}