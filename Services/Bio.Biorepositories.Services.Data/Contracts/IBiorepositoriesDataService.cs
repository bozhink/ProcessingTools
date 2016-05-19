namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IBiorepositoriesDataService<T>
        where T : class
    {
        Task<IQueryable<T>> Get(int skip, int take);
    }
}
