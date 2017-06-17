namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Filters;

    public interface IMultiDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
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
