namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataService<TServiceModel>
    {
        Task<IQueryable<TServiceModel>> All();

        Task<IQueryable<TServiceModel>> Get(int skip, int take);

        Task<TServiceModel> Get(object id);

        Task Add(TServiceModel model);

        Task Update(TServiceModel model);

        Task Delete(TServiceModel model);

        Task Delete(object id);
    }
}