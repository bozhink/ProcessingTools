namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Journals.Services.Data.Contracts.Models;

    public interface IAddressableDataService<T>
        where T : IAddressable
    {
        Task<object> AddAddressAsync(object userId, object modelId, IAddress address);

        Task<object> UpdateAddressAsync(object userId, object modelId, IAddress address);

        Task<object> RemoveAddressAsync(object userId, object modelId, object addressId);
    }
}
