namespace ProcessingTools.Web.Api.Models.MediatypeModels
{
    using Contracts.Mapping;
    using Mediatype.Services.Data.Models;

    public class MediatypeResponseModel : IMapFrom<MediatypeDataServiceResponseModel>
    {
        public string FileExtension { get; set; }

        public string MimeType { get; set; }

        public string MimeSubtype { get; set; }
    }
}