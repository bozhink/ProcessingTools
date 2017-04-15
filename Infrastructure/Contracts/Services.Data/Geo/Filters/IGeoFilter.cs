namespace ProcessingTools.Contracts.Services.Data.Geo.Filters
{
    using ProcessingTools.Contracts.Models;

    public interface IGeoFilter : INameable, IGenericIdentifiable<int?>, IFilter
    {
    }
}
