namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;


    using Models.Publishers;
    using ProcessingTools.Services.Common.Contracts;

    public interface IPublishersDataService : IMvcDataService<PublisherMinimalServiceModel, PublisherServiceModel, PublisherDetailsServiceModel>
    {
        Task<IEnumerable<PublisherSimpleServiceModel>> All();
    }
}
