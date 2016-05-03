namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IBiorepositoriesDataService
    {
        Task<IQueryable<BiorepositoryInstitutionServiceModel>> GetInstitutions(int skip, int take);
    }
}
