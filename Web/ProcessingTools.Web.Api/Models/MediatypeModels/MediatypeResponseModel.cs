namespace ProcessingTools.Web.Api.Models.MediaTypeModels
{
    using Contracts.Mapping;
    using MediaType.Services.Data.Models;

    public class MediaTypeResponseModel : IMapFrom<MediaTypeDataServiceResponseModel>
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}