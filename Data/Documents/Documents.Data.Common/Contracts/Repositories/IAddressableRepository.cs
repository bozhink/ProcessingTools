namespace ProcessingTools.Documents.Data.Common.Contracts.Repositories
{
    using System.Threading.Tasks;
    using Models;

    public interface IAddressableRepository
    {
        Task<object> AddAddress(object entityId, IAddressEntity address);

        Task<object> RemoveAddress(object entityId, object addressId);
    }
}
