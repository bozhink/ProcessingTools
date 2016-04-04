namespace ProcessingTools.Bio.Environments.Services.Data.Contracts
{
    using System.Linq;

    using Models;

    public interface IEnvoTermsDataService
    {
        IQueryable<EnvoTermServiceModel> All();

        IQueryable<EnvoTermServiceModel> Get(int skip, int take);
    }
}
