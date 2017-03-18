namespace ProcessingTools.Contracts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataService<TModel>
    {
        Task<IQueryable<TModel>> All();

        Task<object> Add(TModel model);

        Task<object> Delete(object id);

        Task<TModel> Get(object id);

        Task<object> Update(TModel model);
    }
}
