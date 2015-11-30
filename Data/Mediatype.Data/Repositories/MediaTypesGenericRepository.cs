namespace ProcessingTools.MediaType.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Repositories;

    public class MediaTypesGenericRepository<T> : EfGenericRepository<IMediaTypesDbContext, T>, IMediaTypesRepository<T>
        where T : class
    {
        public MediaTypesGenericRepository(IMediaTypesDbContext context)
            : base(context)
        {
        }
    }
}