namespace ProcessingTools.Web.Api.Models.MediaTypes
{
    using Mappings.Contracts;
    using Mediatypes.Services.Data.Models;

    public class MediaTypeResponseModel : IMapFrom<MediaTypeServiceModel>
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}