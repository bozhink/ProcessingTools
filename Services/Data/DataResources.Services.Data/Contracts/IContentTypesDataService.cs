namespace ProcessingTools.DataResources.Services.Data.Contracts
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Contracts;

    public interface IContentTypesDataService
    {
        Task<IEnumerable<IContentTypeServiceModel>> All(int pageNumber, int numberOfItemsPerPage);

        Task<long> Count();
    }
}
