namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using System.Threading.Tasks;
    using Models;

    public interface IAddressableDataService<T>
        where T : IAddressable
    {
        Task<object> AddAddress(object modelId, IAddress address);

        Task<object> RemoveAddress(object modelId, object addressId);
    }
}
