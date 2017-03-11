namespace ProcessingTools.History.Services.Data.Contracts.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.History.Data.Common.Contracts.Models;

    public interface IHistoryDataService
    {
        Task<object> AddItemToHistory(object userId, object objectId, object item);

        Task<object> AddItemToHistory(object userId, object objectId, DateTime dateModified, object item);

        Task<IEnumerable> Get(object objectId, Type objectType, int skip, int take);

        Task<IEnumerable<IHistoryItem>> Get(object objectId, int skip, int take);

        Task<IEnumerable> GetAll(object objectId, Type objectType);

        Task<IEnumerable<IHistoryItem>> GetAll(object objectId);
    }
}
