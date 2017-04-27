namespace ProcessingTools.Contracts.Services.Data.Geo.Filters
{
    public interface IRegionsFilter : ISynonymisableFilter
    {
        string Country { get; }

        string State { get; }

        string Province { get; }

        string District { get; }

        string Municipality { get; }

        string County { get; }

        string City { get; }
    }
}
