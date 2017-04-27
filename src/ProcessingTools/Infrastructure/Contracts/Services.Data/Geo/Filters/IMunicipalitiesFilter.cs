namespace ProcessingTools.Contracts.Services.Data.Geo.Filters
{
    public interface IMunicipalitiesFilter : ISynonymisableFilter
    {
        string Country { get; }

        string State { get; }

        string Province { get; }

        string Region { get; }

        string District { get; }

        string County { get; }

        string City { get; }
    }
}
