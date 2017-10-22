namespace ProcessingTools.Bio.Biorepositories.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IBiorepositoriesDataService<T>
        where T : class
    {
        Task<T[]> GetAsync(int skip, int take);
    }
}
