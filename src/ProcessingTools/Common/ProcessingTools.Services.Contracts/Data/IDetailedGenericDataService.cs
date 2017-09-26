namespace ProcessingTools.Contracts.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts;

    public interface IDetailedGenericDataService<TModel, TDetailedModel> : IGenericDataService<TModel>
        where TModel : IServiceModel
        where TDetailedModel : TModel, IDetailedModel
    {
        Task<TDetailedModel> GetDetailsAsync(object userId, object id);

        Task<IEnumerable<TDetailedModel>> SelectDetailsAsync(object userId, int skip, int take, Expression<Func<TDetailedModel, object>> sort, SortOrder order = SortOrder.Ascending, Expression<Func<TDetailedModel, bool>> filter = null);
    }
}
