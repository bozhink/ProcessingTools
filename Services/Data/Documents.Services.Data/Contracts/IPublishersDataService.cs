namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models.Publishers;
    using Models.Publishers.Contracts;

    using System.Collections.Generic;

    public interface IPublishersDataService
    {
        Task<object> Add(object userId, PublisherUpdateServiceModel model);

        Task<IEnumerable<IPublisherListableServiceModel>> All();

        Task<IQueryable<PublisherServiceModel>> All(int pageNumber, int itemsPerPage);

        Task<long> Count();

        Task<object> Delete(object id);

        Task<PublisherServiceModel> Get(object id);

        Task<PublisherDetailsServiceModel> GetDetails(object id);

        Task<object> Update(object userId, IPublisherUpdatableServiceModel model);
    }
}
