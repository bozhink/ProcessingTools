namespace ProcessingTools.Geo.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using Models;
    using ProcessingTools.Services.Common.Contracts;

    public interface IContinentsDataService : IDataService<ContinentServiceModel>
    {
        Task<object> AddSynonym(int continentId, ContinentSynonymServiceModel synonym);

        Task<object> RemoveSynonym(int continentId, ContinentSynonymServiceModel synonym);
    }
}
