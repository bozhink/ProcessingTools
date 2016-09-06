namespace ProcessingTools.Services.Common.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIterableDataService<T>
    {
        Task<IEnumerable<T>> All();
    }
}
