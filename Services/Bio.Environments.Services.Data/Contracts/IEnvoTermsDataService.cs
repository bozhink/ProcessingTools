namespace ProcessingTools.Bio.Environments.Services.Data.Contracts
{
    using System.Linq;

    using Models.Contracts;

    public interface IEnvoTermsDataService
    {
        IQueryable<IEnvoTerm> All();

        IQueryable<IEnvoTerm> Get(int skip, int take);
    }
}
