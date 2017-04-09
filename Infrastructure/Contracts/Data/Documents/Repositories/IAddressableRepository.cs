namespace ProcessingTools.Contracts.Data.Documents.Repositories
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Documents.Models;

    public interface IAddressableRepository
    {
        Task<object> AddAddress(object entityId, IAddressEntity address);

        Task<object> RemoveAddress(object entityId, object addressId);
    }
}
