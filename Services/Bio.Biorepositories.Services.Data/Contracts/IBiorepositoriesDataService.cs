namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Linq;

    using Models;

    public interface IBiorepositoriesDataService
    {
        IQueryable<BiorepositoryInstitutionServiceModel> GetBiorepositoryInstitutions(int skip, int take);

        IQueryable<BiorepositoryInstitutionalCodeServiceModel> GetBiorepositoryInstitutionalCodes(int skip, int take);
    }
}
