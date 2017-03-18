namespace ProcessingTools.Contracts.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMultiEntryDataService<TServiceModel>
    {
        Task<IQueryable<TServiceModel>> All();

        Task<IQueryable<TServiceModel>> Query(int skip, int take);

        Task<IQueryable<TServiceModel>> Get(params object[] ids);

        Task<object> Add(params TServiceModel[] models);

        Task<object> Update(params TServiceModel[] models);

        Task<object> Delete(params TServiceModel[] models);

        Task<object> Delete(params object[] ids);
    }
}
