// <copyright file="IContextParser{TContext,TStyle,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Layout.Styles;

    /// <summary>
    /// Generic context parser.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    /// <typeparam name="TStyle">Type of style rules to apply.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IContextParser<in TContext, in TStyle, TResult>
        where TStyle : IStyleModel
    {
        /// <summary>
        /// Executes parsing operation over the context.
        /// </summary>
        /// <param name="context">Context object to be processed.</param>
        /// <param name="styles">Style rules to apply.</param>
        /// <returns>Task of result.</returns>
        Task<TResult> ParseAsync(TContext context, IEnumerable<TStyle> styles);
    }
}
