namespace ProcessingTools.Geo.Data.Entity.Models
{
    using ProcessingTools.Contracts.Models;

    public interface ISynonym : INameableIntegerIdentifiable
    {
        int? LanguageCode { get; }
    }
}
