namespace ProcessingTools.Contracts.Data.Journals.Repositories
{
    using ProcessingTools.Contracts.Data.Journals.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IPublishersRepository : IAddressableRepository, ICrudRepository<IPublisher>
    {
    }
}
