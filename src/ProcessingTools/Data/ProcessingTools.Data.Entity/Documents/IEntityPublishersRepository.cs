namespace ProcessingTools.Data.Entity.Documents
{
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Data.Entity.Abstractions;
    using ProcessingTools.Models.Contracts.Documents;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityGenericRepository<IPublisher>
    {
    }
}
