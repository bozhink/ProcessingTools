namespace ProcessingTools.Documents.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Contracts.Data.Documents.Models;
    using ProcessingTools.Contracts.Data.Documents.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityGenericRepository<IPublisher>
    {
    }
}
