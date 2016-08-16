namespace ProcessingTools.Documents.Data.Common.Repositories.Contracts
{
    using System.Threading.Tasks;
    using Models.Contracts;

    public interface IAddressableRepository
    {
        Task<object> AddAddress(IAddressEntity address);

        Task<object> RemoveAddress(object addressId);
    }
}
