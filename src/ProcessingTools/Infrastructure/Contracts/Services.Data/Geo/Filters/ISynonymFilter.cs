namespace ProcessingTools.Contracts.Services.Data.Geo.Filters
{
    public interface ISynonymFilter : IGeoFilter
    {
        int? LanguageCode { get; }
    }
}
