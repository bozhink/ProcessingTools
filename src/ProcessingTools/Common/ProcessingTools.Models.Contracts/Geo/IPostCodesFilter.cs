namespace ProcessingTools.Contracts.Filters.Geo
{
    using ProcessingTools.Contracts.Models;

    public interface IPostCodesFilter : IGenericIdentifiable<int?>, IFilter
    {
        string Country { get; }

        string State { get; }

        string Province { get; }

        string Region { get; }

        string District { get; }

        string Municipality { get; }

        string County { get; }

        string City { get; }
    }
}
