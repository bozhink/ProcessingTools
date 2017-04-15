namespace ProcessingTools.Contracts.Services.Data.Geo.Services
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Data.Geo.Filters;
    using ProcessingTools.Contracts.Services.Data.Geo.Models;

    public interface ICitiesDataService : IDataServiceAsync<ICity, ICitiesFilter>, ISynonymisableDataService<ICitySynonym>
    {
        Task<object> AddPostCodeAsync(int modelId, IPostCode postCode);

        Task<object> RemovePostCodeAsync(int modelId, int postCodeId);

        Task<object> UpdatePostCodeAsync(int modelId, IPostCode postCode);
    }
}
