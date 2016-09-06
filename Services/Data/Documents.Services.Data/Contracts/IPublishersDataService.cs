namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Models.Publishers.Contracts;

    public interface IPublishersDataService
    {
        Task<object> Add(object userId, IPublisherAddableServiceModel model);

        Task<IEnumerable<IPublisherListableServiceModel>> All();

        Task<IQueryable<IPublisherSimpleServiceModel>> All(int pageNumber, int itemsPerPage);

        Task<long> Count();

        Task<object> Delete(object id);

        Task<IPublisherSimpleServiceModel> Get(object id);

        Task<IPublisherDetailedServiceModel> GetDetails(object id);

        Task<object> Update(object userId, IPublisherUpdatableServiceModel model);
    }
}
