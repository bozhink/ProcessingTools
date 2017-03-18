namespace ProcessingTools.Contracts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;

    public interface IGenericDataService<TModel>
    {
        Task<object> Add(TModel model);

        Task<object> Update(TModel model);

        Task<object> Delete(object id);

        Task<TModel> Get(object id);

        Task<IEnumerable<TModel>> Select(int skip, int take, Expression<Func<TModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TModel, bool>> filter = null);
    }
}
