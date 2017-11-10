namespace ProcessingTools.Journals.Services.Data.Contracts.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Data.Journals;

    public interface IAddressableDataService<T>
        where T : IAddressable
    {
        Task<object> AddAddressAsync(object userId, object modelId, IAddress address);

        Task<object> UpdateAddressAsync(object userId, object modelId, IAddress address);

        Task<object> RemoveAddressAsync(object userId, object modelId, object addressId);
    }
}
