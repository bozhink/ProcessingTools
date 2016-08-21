namespace ProcessingTools.Documents.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Common.Models.Contracts;
    using ProcessingTools.Documents.Data.Common.Repositories.Contracts;

    public interface IEntityPublishersRepository : IPublishersRepository, IEntityCrudRepository<IPublisherEntity>
    {
    }
}
