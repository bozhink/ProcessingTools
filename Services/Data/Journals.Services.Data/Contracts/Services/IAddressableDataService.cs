namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using System.Threading.Tasks;
    using Models;

    public interface IAddressableDataService<T>
        where T : IAddressable
    {
        Task<object> AddAddress(object userId, object modelId, IAddress address);

        Task<object> RemoveAddress(object userId, object modelId, object addressId);
    }
}
