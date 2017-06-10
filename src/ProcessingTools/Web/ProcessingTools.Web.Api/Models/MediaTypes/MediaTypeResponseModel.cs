namespace ProcessingTools.Web.Api.Models.MediaTypes
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Mediatypes;

    public class MediaTypeResponseModel : IMapFrom<IMediatype>
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}
