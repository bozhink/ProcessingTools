namespace ProcessingTools.Web.Api.Models.MediaTypes
{
    using Contracts.Mapping;
    using MediaType.Services.Data.Models.Contracts;

    public class MediaTypeResponseModel : IMapFrom<IMediaTypeServiceModel>
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}