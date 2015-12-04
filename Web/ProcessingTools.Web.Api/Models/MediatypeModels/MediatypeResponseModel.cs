namespace ProcessingTools.Web.Api.Models.MediaTypeModels
{
    using Contracts.Mapping;
    using MediaType.Services.Data.Contracts;

    public class MediaTypeResponseModel : IMapFrom<IMediaType>
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}