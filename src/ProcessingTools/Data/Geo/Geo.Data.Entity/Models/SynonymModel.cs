namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Models.Contracts.Geo;

    internal abstract class SynonymModel : IGeoSynonym
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public int? LanguageCode { get; set; }

        public string Name { get; set; }
    }
}
