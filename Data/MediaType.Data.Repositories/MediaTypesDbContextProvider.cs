namespace ProcessingTools.MediaType.Data.Repositories
{
    using Contracts;

    public class MediaTypesDbContextProvider : IMediaTypesDbContextProvider
    {
        public MediaTypesDbContext Create()
        {
            return MediaTypesDbContext.Create();
        }
    }
}