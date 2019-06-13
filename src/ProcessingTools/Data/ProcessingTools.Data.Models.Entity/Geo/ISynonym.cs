namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Contracts.Models;

    public interface ISynonym : INamedIntegerIdentified
    {
        int? LanguageCode { get; }
    }
}
