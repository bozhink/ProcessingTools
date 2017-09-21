namespace ProcessingTools.Contracts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Models;
    using ProcessingTools.Enumerations;

    public interface IGenericDataService<TModel>
        where TModel : IServiceModel
    {
        Task<object> Add(object userId, TModel model);

        Task<object> Update(object userId, TModel model);

        Task<object> Delete(object userId, object id);

        Task<TModel> Get(object userId, object id);

        Task<IEnumerable<TModel>> Select(object userId, int skip, int take, Expression<Func<TModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TModel, bool>> filter = null);
    }
}
