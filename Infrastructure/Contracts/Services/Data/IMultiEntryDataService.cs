namespace ProcessingTools.Contracts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMultiEntryDataService<TModel>
    {
        Task<object> DeleteAsync(params TModel[] models);

        Task<object> DeleteAsync(params object[] ids);

        Task<TModel> GetByIdAsync(object id);

        Task<object> InsertAsync(params TModel[] models);

        Task<TModel[]> SelectAllAsync();

        Task<TModel[]> SelectAsync(int skip, int take);

        Task<object> UpdateAsync(params TModel[] models);
    }
}
