namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;

    public interface IDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        Task<object> DeleteAsync(TModel model);

        Task<object> DeleteAsync(object id);

        Task<TModel> GetByIdAsync(object id);

        Task<object> InsertAsync(TModel model);

        Task<TModel[]> SelectAsync(TFilter filter);

        Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending);

        Task<long> SelectCountAsync(TFilter filter);

        Task<object> UpdateAsync(TModel model);
    }
}
