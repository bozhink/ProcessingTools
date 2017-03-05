namespace ProcessingTools.Contracts.Data.Repositories
{
    using System.Threading.Tasks;

    public interface ISelectableByIdRepository<T> : IRepository<T>
    {
        Task<T> GetById(object id);
    }
}
