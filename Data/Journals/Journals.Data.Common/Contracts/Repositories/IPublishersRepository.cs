namespace ProcessingTools.Journals.Data.Common.Contracts.Repositories
{
    using Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IPublishersRepository : IAddressableRepository, ISearchableCountableCrudRepository<IPublisher>
    {
    }
}
