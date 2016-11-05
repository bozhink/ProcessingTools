namespace ProcessingTools.Documents.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Common.Contracts.Models;
    using ProcessingTools.Documents.Data.Common.Contracts.Repositories;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityGenericRepository<IPublisherEntity>
    {
    }
}
