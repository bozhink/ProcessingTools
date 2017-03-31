namespace ProcessingTools.Geo.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Geo.Services.Data.Models;

    public interface IContinentsDataService : IDataService<ContinentServiceModel>
    {
        Task<object> AddSynonym(int continentId, ContinentSynonymServiceModel synonym);

        Task<object> RemoveSynonym(int continentId, ContinentSynonymServiceModel synonym);
    }
}
