namespace ProcessingTools.Web.Areas.Data.Models.Shared
{
    using ProcessingTools.Contracts.Models.Geo;

    public class SynonymResponseModel : IGeoSynonym
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? LanguageCode { get; set; }

        public int ParentId { get; set; }
    }
}
