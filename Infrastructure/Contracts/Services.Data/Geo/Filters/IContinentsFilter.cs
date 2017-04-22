namespace ProcessingTools.Contracts.Services.Data.Geo.Filters
{
    public interface IContinentsFilter : ISynonymisableFilter
    {
        string Country { get; }
    }
}
