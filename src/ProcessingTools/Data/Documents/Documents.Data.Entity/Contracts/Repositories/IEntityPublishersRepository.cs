namespace ProcessingTools.Documents.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories.Documents;
    using ProcessingTools.Models.Contracts.Documents;
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityGenericRepository<IPublisher>
    {
    }
}
