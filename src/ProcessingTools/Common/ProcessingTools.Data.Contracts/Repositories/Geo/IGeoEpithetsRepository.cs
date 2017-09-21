namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models.Geo;

    public interface IGeoEpithetsRepository : IRepositoryAsync<IGeoEpithet, ITextFilter>
    {
    }
}
