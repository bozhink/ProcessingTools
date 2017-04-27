namespace ProcessingTools.Contracts.Services.Data.Geo.Filters
{
    public interface ICitiesFilter : ISynonymisableFilter
    {
        string Country { get; }

        string State { get; }

        string Province { get; }

        string Region { get; }

        string District { get; }

        string Municipality { get; }

        string County { get; }

        string PostCode { get; }
    }
}
