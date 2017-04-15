namespace ProcessingTools.Contracts.Services.Data.Geo.Filters
{
    public interface IProvincesFilter : ISynonymisableFilter
    {
        string Country { get; }

        string State { get; }

        string Region { get; }

        string District { get; }

        string Municipality { get; }

        string County { get; }

        string City { get; }
    }
}
