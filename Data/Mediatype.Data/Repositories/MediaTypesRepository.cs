namespace ProcessingTools.MediaType.Data.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories;
    using ProcessingTools.MediaType.Data.Contracts;
    using ProcessingTools.MediaType.Data.Repositories.Contracts;

    public class MediaTypesRepository<T> : EntityGenericRepository<MediaTypesDbContext, T>, IMediaTypesRepository<T>
        where T : class
    {
        public MediaTypesRepository(IMediaTypesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
