namespace ProcessingTools.Services.Common.Contracts
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataService<TServiceModel> : IDataServiceBase<TServiceModel>
    {
        Task<IQueryable<TServiceModel>> All();
    }
}
