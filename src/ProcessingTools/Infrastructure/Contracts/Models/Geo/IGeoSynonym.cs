namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using ProcessingTools.Contracts.Models;

    public interface IGeoSynonym : INameableIntegerIdentifiable
    {
        int ParentId { get; }

        int? LanguageCode { get; }
    }
}
