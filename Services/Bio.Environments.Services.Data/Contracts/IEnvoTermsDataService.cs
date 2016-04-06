namespace ProcessingTools.Bio.Environments.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IEnvoTermsDataService
    {
        Task<IQueryable<EnvoTermServiceModel>> All();

        Task<IQueryable<EnvoTermServiceModel>> Get(int skip, int take);
    }
}
