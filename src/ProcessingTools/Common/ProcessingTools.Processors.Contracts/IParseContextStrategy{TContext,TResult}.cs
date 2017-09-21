// <copyright file="IParseContextStrategy{TContext,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using ProcessingTools.Contracts;

    /// <summary>
    /// Strategy to parse specified context.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object</typeparam>
    /// <typeparam name="TResult">Type of the output result</typeparam>
    public interface IParseContextStrategy<in TContext, TResult> : IContextParser<TContext, TResult>, IStrategy
    {
    }
}
