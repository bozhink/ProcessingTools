namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataService<TServiceModel>
    {
        Task<IQueryable<TServiceModel>> All();

        Task<IQueryable<TServiceModel>> Query(int skip, int take);

        Task<IQueryable<TServiceModel>> Get(params object[] ids);

        Task<int> Add(params TServiceModel[] models);

        Task<int> Update(params TServiceModel[] models);

        Task<int> Delete(params TServiceModel[] models);

        Task<int> Delete(params object[] ids);
    }
}
