namespace ProcessingTools.DataResources.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Contracts;

    public interface IContentTypesDataService
    {
        Task<object> Add(IContentTypeCreateServiceModel model);

        Task<IEnumerable<IContentTypeServiceModel>> All();

        Task<IEnumerable<IContentTypeServiceModel>> All(int pageNumber, int numberOfItemsPerPage);

        Task<long> Count();

        Task<object> Delete(object id);

        Task<IContentTypeDetailsServiceModel> GetDetails(object id);

        Task<object> Update(IContentTypeUpdateServiceModel model);
    }
}
