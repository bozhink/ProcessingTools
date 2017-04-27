namespace ProcessingTools.Contracts.Data.Geo.Models
{
    using ProcessingTools.Contracts.Models;

    public interface ISynonym : INameableIntegerIdentifiable
    {
        int? LanguageCode { get; }
    }
}
