namespace ProcessingTools.Documents.Data.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Common.Contracts.Models;
    using ProcessingTools.Documents.Data.Common.Contracts.Repositories;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityGenericRepository<IPublisherEntity>
    {
    }
}
