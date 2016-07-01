namespace ProcessingTools.Documents.Services.Data.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Journals;

    public interface IJournalsDataService
    {
        Task<object> Add(object userId, JournalMinimalServiceModel model);

        Task<IQueryable<JournalServiceModel>> All(int pageNumber, int itemsPerPage);

        Task<long> Count();

        Task<object> Delete(object id);

        Task<JournalServiceModel> Get(object id);

        Task<JournalDetailsServiceModel> GetDetails(object id);

        Task<object> Update(object userId, JournalMinimalServiceModel model);
    }
}
