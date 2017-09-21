namespace ProcessingTools.Contracts.Filters.Geo
{
    public interface IContinentsFilter : ISynonymisableFilter
    {
        string Country { get; }
    }
}
