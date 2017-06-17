namespace ProcessingTools.Web.Areas.Data.Models.Shared
{
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public class SynonymResponseModel : ISynonym
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? LanguageCode { get; set; }

        public int ParentId { get; set; }
    }
}
