namespace ProcessingTools.Bio.Environments.Services.Data.Contracts
{
    using System.Linq;

    using Models.Contracts;

    public interface IEnvoTermsDataService
    {
        IQueryable<IEnvoTermServiceModel> All();

        IQueryable<IEnvoTermServiceModel> Get(int skip, int take);
    }
}
