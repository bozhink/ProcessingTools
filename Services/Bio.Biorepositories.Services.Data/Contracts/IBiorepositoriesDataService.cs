namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Linq;

    using Models.Contracts;

    public interface IBiorepositoriesDataService
    {
        IQueryable<IBiorepositoryInstitutionServiceModel> GetBiorepositoryInstitutions(int skip, int take);

        IQueryable<IBiorepositoryInstitutionalCodeServiceModel> GetBiorepositoryInstitutionalCodes(int skip, int take);
    }
}
