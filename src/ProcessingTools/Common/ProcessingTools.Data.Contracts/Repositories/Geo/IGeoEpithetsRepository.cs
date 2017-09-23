namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IGeoEpithetsRepository : IRepositoryAsync<IGeoEpithet, ITextFilter>
    {
    }
}
