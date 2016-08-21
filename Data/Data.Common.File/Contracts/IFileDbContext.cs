namespace ProcessingTools.Data.Common.File.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IFileDbContext<T>
    {
        Task<object> Add(T entity);

        IQueryable<T> DataSet { get; }

        Task<object> Delete(object id);

        Task<T> Get(object id);

        Task<long> LoadFromFile(string fileName);

        Task<object> Update(T entity);

        Task<long> WriteToFile(string fileName);
    }
}
