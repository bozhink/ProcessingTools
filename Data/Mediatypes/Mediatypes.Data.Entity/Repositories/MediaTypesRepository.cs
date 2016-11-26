namespace ProcessingTools.MediaType.Data.Entity.Repositories
{
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class MediaTypesRepository<T> : EntityGenericRepository<MediaTypesDbContext, T>, IMediaTypesRepository<T>
        where T : class
    {
        public MediaTypesRepository(IMediaTypesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
