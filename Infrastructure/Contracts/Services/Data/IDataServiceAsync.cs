namespace ProcessingTools.Contracts.Services.Data
{
    using System.Threading.Tasks;

    public interface IDataServiceAsync<TModel>
    {
        Task<object> InsertAsync(TModel model);

        Task<object> DeleteAsync(object id);

        Task<TModel> GetByIdAsync(object id);

        Task<object> UpdateAsync(TModel model);
    }
}
