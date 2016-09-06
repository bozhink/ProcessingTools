namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMvcDataService<TMinimalServiceModel, TServiceModel, TDetailsServiceModel>
        where TMinimalServiceModel : class
        where TServiceModel : class
        where TDetailsServiceModel : class
    {
        Task<object> Add(object userId, TMinimalServiceModel model);

        Task<IQueryable<TServiceModel>> All(int pageNumber, int itemsPerPage);

        Task<long> Count();

        Task<object> Delete(object id);

        Task<TServiceModel> Get(object id);

        Task<TDetailsServiceModel> GetDetails(object id);

        Task<object> Update(object userId, TMinimalServiceModel model);
    }
}
