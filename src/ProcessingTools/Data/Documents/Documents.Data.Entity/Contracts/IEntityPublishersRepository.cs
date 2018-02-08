namespace ProcessingTools.Documents.Data.Entity.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Models.Contracts.Documents;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityGenericRepository<IPublisher>
    {
    }
}
