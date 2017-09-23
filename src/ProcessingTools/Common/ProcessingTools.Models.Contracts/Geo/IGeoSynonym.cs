namespace ProcessingTools.Models.Contracts.Geo
{
    using ProcessingTools.Models.Contracts;

    public interface IGeoSynonym : INameableIntegerIdentifiable
    {
        int ParentId { get; }

        int? LanguageCode { get; }
    }
}
