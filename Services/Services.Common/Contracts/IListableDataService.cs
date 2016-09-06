namespace ProcessingTools.Services.Common.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Contracts;

    public interface IListableDataService<T>
        where T : IListableServiceModel
    {
        Task<IEnumerable<T>> All();
    }
}
