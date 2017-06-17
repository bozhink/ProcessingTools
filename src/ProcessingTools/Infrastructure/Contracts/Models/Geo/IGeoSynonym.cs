namespace ProcessingTools.Contracts.Models.Geo
{
    using ProcessingTools.Contracts.Models;

    public interface IGeoSynonym : INameableIntegerIdentifiable
    {
        int ParentId { get; }

        int? LanguageCode { get; }
    }
}
