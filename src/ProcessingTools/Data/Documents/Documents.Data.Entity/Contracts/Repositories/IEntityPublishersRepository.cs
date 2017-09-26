namespace ProcessingTools.Documents.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Documents.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;
    using ProcessingTools.Models.Contracts.Documents;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityGenericRepository<IPublisher>
    {
    }
}
