namespace ProcessingTools.Web.Areas.Data.Filters.Continents
{
    using ProcessingTools.Contracts.Filters.Geo;

    public class ContinentSynonymsFilter : IContinentSynonymsFilter
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? LanguageCode { get; set; }

        public int? ParentId { get; set; }
    }
}
