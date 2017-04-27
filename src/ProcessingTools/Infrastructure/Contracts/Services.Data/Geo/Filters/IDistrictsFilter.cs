namespace ProcessingTools.Contracts.Services.Data.Geo.Filters
{
    public interface IDistrictsFilter : ISynonymisableFilter
    {
        string Country { get; }

        string State { get; }

        string Province { get; }

        string Region { get; }

        string Municipality { get; }

        string County { get; }

        string City { get; }
    }
}
