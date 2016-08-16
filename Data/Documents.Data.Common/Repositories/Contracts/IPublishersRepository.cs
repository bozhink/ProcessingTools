namespace ProcessingTools.Documents.Data.Common.Repositories.Contracts
{
    using System.Threading.Tasks;

    using Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IPublishersRepository : ICountableRepository<IPublisherEntity>, ICrudRepository<IPublisherEntity>, IIterableRepository<IPublisherEntity>
    {
        Task<object> AddAddress(IAddressEntity address);

        Task<object> RemoveAddress(object addressId);
    }
}
