namespace ProcessingTools.Geo.Services.Data.Entity.Services
{
    using ProcessingTools.Data.Contracts.Geo;
    using ProcessingTools.Geo.Services.Data.Abstractions;
    using ProcessingTools.Models.Contracts.Geo;
    using ProcessingTools.Services.Contracts.Geo;

    public class PostCodesDataService : AbstractGeoDataService<IPostCodesRepository, IPostCode, IPostCodesFilter>, IPostCodesDataService
    {
        public PostCodesDataService(IPostCodesRepository repository)
            : base(repository)
        {
        }
    }
}
