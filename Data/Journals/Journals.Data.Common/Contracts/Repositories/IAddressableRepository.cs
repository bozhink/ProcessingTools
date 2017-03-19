namespace ProcessingTools.Journals.Data.Common.Contracts.Repositories
{
    using System.Threading.Tasks;
    using Models;

    public interface IAddressableRepository
    {
        Task<object> AddAddress(object entityId, IAddress address);

        Task<object> UpdateAddress(object entityId, IAddress address);

        Task<object> RemoveAddress(object entityId, object addressId);
    }
}
