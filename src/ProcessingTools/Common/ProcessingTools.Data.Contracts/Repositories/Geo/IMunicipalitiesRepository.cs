namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    public interface IMunicipalitiesRepository : IRepositoryAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableRepository<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
