namespace ProcessingTools.Contracts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Models;
    using ProcessingTools.Enumerations;

    public interface IDetailedGenericDataService<TModel, TDetailedModel> : IGenericDataService<TModel>
        where TModel : IServiceModel
        where TDetailedModel : TModel, IDetailedModel
    {
        Task<TDetailedModel> GetDetails(object userId, object id);

        Task<IEnumerable<TDetailedModel>> SelectDetails(object userId, int skip, int take, Expression<Func<TDetailedModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TDetailedModel, bool>> filter = null);
    }
}
