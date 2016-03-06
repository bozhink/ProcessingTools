namespace ProcessingTools.MediaType.Data
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