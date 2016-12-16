namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataService<TServiceModel>
    {
        Task<IQueryable<TServiceModel>> All();

        Task<object> Add(TServiceModel model);

        Task<object> Delete(object id);

        Task<TServiceModel> Get(object id);

        Task<object> Update(TServiceModel model);
    }
}
