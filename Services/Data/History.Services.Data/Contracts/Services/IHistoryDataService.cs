namespace ProcessingTools.History.Services.Data.Contracts.Services
{
    using System;
    using System.Collections;
    using System.Threading.Tasks;

    public interface IHistoryDataService
    {
        Task<object> AddItemToHistory(object userId, object objectId, object item);

        Task<object> AddItemToHistory(object userId, object objectId, DateTime dateModified, object item);

        Task<IEnumerable> GetAll(object objectId, Type objectType);

        Task<IEnumerable> Get(object objectId, Type objectType, int skip, int take);
    }
}
