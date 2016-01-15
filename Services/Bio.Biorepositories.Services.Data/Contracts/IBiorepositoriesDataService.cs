namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Linq;

    using Models.Contracts;

    public interface IBiorepositoriesDataService
    {
        IQueryable<IBiorepositoryInstitution> GetBiorepositoryInstitutions(int skip, int take);

        IQueryable<IBiorepositoryInstitutionalCode> GetBiorepositoryInstitutionalCodes(int skip, int take);
    }
}
