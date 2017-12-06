// <copyright file="IHarvester{TContext,TModel}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Harvesters
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic harvester.
    /// </summary>
    /// <typeparam name="TContext">Type of the context to harvest.</typeparam>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IHarvester<TContext, TModel>
    {
        /// <summary>
        /// Do harvest algorithm over the specified context.
        /// </summary>
        /// <param name="context">Context to be harvested.</param>
        /// <returns>Harvest result.</returns>
        Task<TModel> HarvestAsync(TContext context);
    }
}
