namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface ISavabaleRepository
    {
        Task<long> SaveChanges();
    }
}
