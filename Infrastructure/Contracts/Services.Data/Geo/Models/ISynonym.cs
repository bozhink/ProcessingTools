namespace ProcessingTools.Contracts.Services.Data.Geo.Models
{
    using ProcessingTools.Contracts.Models;

    public interface ISynonym : INameableIntegerIdentifiable
    {
        int ParentId { get; }

        int? LanguageCode { get; }
    }
}
