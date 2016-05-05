namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IBiorepositoriesInstitutionalCollectionsDataService
    {
        Task<IQueryable<BiorepositoriesCollectionServiceModel>> GetCollections(int skip, int take);
    }
}
