namespace ProcessingTools.MediaType.Data.Seed.Contracts
{
    using ProcessingTools.Data.Common.Entity.Contracts;

    public interface IMediaTypeDataInitializer : IDbContextInitializer<MediaTypesDbContext>
    {
    }
}