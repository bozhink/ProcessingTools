namespace ProcessingTools.Documents.Data.Common.Repositories.Contracts
{
    using Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IPublishersRepository : IAddressableRepository, ICountableIterableCrudRepository<IPublisherEntity>
    {
    }
}
