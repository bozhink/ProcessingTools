namespace ProcessingTools.Services.Common.Contracts
{
    using System.Threading.Tasks;

    public interface IDataServiceBase<TServiceModel>
    {
        Task<object> Add(TServiceModel model);

        Task<object> Delete(object id);

        Task<TServiceModel> Get(object id);

        Task<object> Update(TServiceModel model);
    }
}
