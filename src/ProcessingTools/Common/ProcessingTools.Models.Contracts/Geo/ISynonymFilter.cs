namespace ProcessingTools.Contracts.Filters.Geo
{
    public interface ISynonymFilter : IGeoFilter
    {
        int? LanguageCode { get; }
    }
}
