namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Publishers;

    public interface IPublishersDataService
    {
        Task<object> Add(object userId, PublisherMinimalServiceModel model);

        Task<IQueryable<PublisherServiceModel>> All(int pageNumber, int itemsPerPage);

        Task<object> Delete(object id);

        Task<PublisherServiceModel> Get(object id);

        Task<PublisherDetailsServiceModel> GetDetails(object id);

        Task<object> Update(object userId, PublisherMinimalServiceModel model);
    }
}
