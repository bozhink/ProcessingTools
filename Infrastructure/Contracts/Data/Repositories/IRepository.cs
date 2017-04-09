namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface IRepository
    {
        Task<long> SaveChangesAsync();
    }
}
