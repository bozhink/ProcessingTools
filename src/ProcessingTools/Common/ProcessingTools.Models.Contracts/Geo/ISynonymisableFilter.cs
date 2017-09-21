namespace ProcessingTools.Contracts.Filters.Geo
{
    public interface ISynonymisableFilter : IGeoFilter
    {
        string Synonym { get; }
    }
}
