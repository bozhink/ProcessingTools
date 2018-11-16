namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    public abstract class SynonymModel : IGeoSynonym
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public int? LanguageCode { get; set; }

        public string Name { get; set; }
    }
}
