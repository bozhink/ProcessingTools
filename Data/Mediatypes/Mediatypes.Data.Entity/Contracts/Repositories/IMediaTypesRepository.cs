namespace ProcessingTools.Mediatypes.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IMediatypesRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
