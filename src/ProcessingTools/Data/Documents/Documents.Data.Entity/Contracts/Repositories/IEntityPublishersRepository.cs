namespace ProcessingTools.Documents.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Contracts.Repositories.Documents;
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;
    using ProcessingTools.Models.Contracts.Documents;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityGenericRepository<IPublisher>
    {
    }
}
