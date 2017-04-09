namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface IRepository
    {
        object SaveChanges();

        Task<object> SaveChangesAsync();
    }
}
