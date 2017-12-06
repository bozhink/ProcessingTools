// <copyright file="IEnumerableHarvester{TContext,TModel}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Harvesters
{
    /// <summary>
    /// Generic harvester with enumerable return object.
    /// </summary>
    /// <typeparam name="TContext">Type of the context to harvest.</typeparam>
    /// <typeparam name="TModel">Type of the harvester model.</typeparam>
    public interface IEnumerableHarvester<TContext, TModel> : IHarvester<TContext, TModel[]>
    {
    }
}
