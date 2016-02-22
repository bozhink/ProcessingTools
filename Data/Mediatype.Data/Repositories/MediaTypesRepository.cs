namespace ProcessingTools.MediaType.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories.Factories;
    using ProcessingTools.MediaType.Data.Contracts;
    using ProcessingTools.MediaType.Data.Repositories.Contracts;

    public class MediaTypesRepository<T> : EfGenericRepository<IMediaTypesDbContext, T>, IMediaTypesRepository<T>
        where T : class
    {
        public MediaTypesRepository(IMediaTypesDbContext context)
            : base(context)
        {
        }
    }
}
