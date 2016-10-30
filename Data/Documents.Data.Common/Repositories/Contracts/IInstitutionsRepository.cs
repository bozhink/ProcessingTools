namespace ProcessingTools.Documents.Data.Common.Repositories.Contracts
{
    using Models.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IInstitutionsRepository : IAddressableRepository, ICrudRepository<IInstitutionEntity>
    {
    }
}
