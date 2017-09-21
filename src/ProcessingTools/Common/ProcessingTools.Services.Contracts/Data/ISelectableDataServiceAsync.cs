namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Enumerations;

    public interface ISelectableDataServiceAsync<TModel, TFilter>
        where TFilter : IFilter
    {
        Task<TModel> GetByIdAsync(object id);

        Task<TModel[]> SelectAsync(TFilter filter);

        Task<TModel[]> SelectAsync(TFilter filter, int skip, int take, string sortColumn, SortOrder sortOrder = SortOrder.Ascending);

        Task<long> SelectCountAsync(TFilter filter);
    }
}
