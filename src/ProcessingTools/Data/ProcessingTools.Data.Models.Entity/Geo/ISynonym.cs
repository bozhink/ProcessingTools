namespace ProcessingTools.Data.Models.Entity.Geo
{
    using ProcessingTools.Models.Contracts;

    public interface ISynonym : INameableIntegerIdentifiable
    {
        int? LanguageCode { get; }
    }
}
