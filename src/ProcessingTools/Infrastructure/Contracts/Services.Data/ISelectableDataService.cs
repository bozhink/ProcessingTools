namespace ProcessingTools.Contracts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Enumerations;

    public interface ISelectableDataService<TModel>
        where TModel : IServiceModel
    {
        Task<long> Count(Expression<Func<TModel, bool>> filter = null);

        Task<IEnumerable<TModel>> Select(int skip, int take, Expression<Func<TModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TModel, bool>> filter = null);

        Task<IEnumerable<TModel>> Select(Expression<Func<TModel, bool>> filter = null);
    }
}
