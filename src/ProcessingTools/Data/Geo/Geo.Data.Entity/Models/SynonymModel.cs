namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Contracts.Models.Geo;

    internal abstract class SynonymModel : IGeoSynonym
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public int? LanguageCode { get; set; }

        public string Name { get; set; }
    }
}
