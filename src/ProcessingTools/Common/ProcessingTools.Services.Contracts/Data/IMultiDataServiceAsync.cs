namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Filters;

    public interface IMultiDataServiceAsync<TModel, TFilter> : ISelectableDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        Task<object> DeleteAsync(params TModel[] models);

        Task<object> DeleteAsync(params object[] ids);

        Task<object> InsertAsync(params TModel[] models);

        Task<object> UpdateAsync(params TModel[] models);
    }
}
