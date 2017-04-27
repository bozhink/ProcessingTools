namespace ProcessingTools.Geo.Services.Data.Entity.Models
{
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    internal abstract class Synonym : ISynonym
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public int? LanguageCode { get; set; }

        public string Name { get; set; }
    }
}
