namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Contracts.Models.Geo;

    public interface IMunicipalitiesRepository : IRepositoryAsync<IMunicipality, IMunicipalitiesFilter>, IGeoSynonymisableRepository<IMunicipality, IMunicipalitySynonym, IMunicipalitySynonymsFilter>
    {
    }
}
