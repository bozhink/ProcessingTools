namespace ProcessingTools.Mediatypes.Data.Entity.Repositories
{
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class MediatypesRepository<T> : EntityGenericRepository<MediatypesDbContext, T>, IMediatypesRepository<T>
        where T : class
    {
        public MediatypesRepository(IMediatypesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
