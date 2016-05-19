namespace ProcessingTools.Web.Api.Models.MediaTypes
{
    using Mappings.Contracts;
    using MediaType.Services.Data.Models;

    public class MediaTypeResponseModel : IMapFrom<MediaTypeServiceModel>
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}