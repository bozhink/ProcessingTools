namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface IDeletableRepository<T> : IRepository<T>
    {
        Task<object> Delete(object id);
    }
}
