namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IBiorepositoriesDataService
    {
        Task<IQueryable<BiorepositoryInstitutionServiceModel>> GetBiorepositoryInstitutions(int skip, int take);

        Task<IQueryable<BiorepositoryInstitutionalCodeServiceModel>> GetBiorepositoryInstitutionalCodes(int skip, int take);
    }
}
