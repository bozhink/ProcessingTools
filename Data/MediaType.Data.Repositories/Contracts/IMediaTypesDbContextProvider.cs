namespace ProcessingTools.MediaType.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IMediaTypesDbContextProvider : IDbContextProvider<MediaTypesDbContext>
    {
    }
}