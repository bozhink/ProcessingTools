namespace ProcessingTools.Data.Common.File.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IFileDbContext<T>
    {
        IQueryable<T> DataSet { get; }

        Task<object> AddAsync(T entity);

        Task<object> DeleteAsync(object id);

        Task<T> GetAsync(object id);

        Task<long> LoadFromFileAsync(string fileName);

        Task<object> UpdateAsync(T entity);

        Task<long> WriteToFileAsync(string fileName);
    }
}
