namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    public interface ISynonym : INameableIntegerIdentifiable
    {
        int? LanguageCode { get; }
    }
}
