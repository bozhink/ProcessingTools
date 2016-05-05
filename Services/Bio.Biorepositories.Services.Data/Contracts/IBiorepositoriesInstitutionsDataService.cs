namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IBiorepositoriesInstitutionsDataService
    {
        Task<IQueryable<BiorepositoriesInstitutionServiceModel>> GetInstitutions(int skip, int take);
    }
}
