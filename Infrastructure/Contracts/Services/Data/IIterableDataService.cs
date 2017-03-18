namespace ProcessingTools.Contracts.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIterableDataService<T>
    {
        Task<IEnumerable<T>> All();
    }
}
