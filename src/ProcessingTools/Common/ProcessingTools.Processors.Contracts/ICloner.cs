// <copyright file="ICloner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Represents source-to-target cloner.
    /// </summary>
    /// <typeparam name="TTarget">Type of the target of cloning</typeparam>
    /// <typeparam name="TSource">Type of the source of information</typeparam>
    /// <typeparam name="TResult">Type of the result</typeparam>
    public interface ICloner<TTarget, TSource, TResult>
    {
        /// <summary>
        /// Clones information from source to target.
        /// </summary>
        /// <param name="target">Target of cloning</param>
        /// <param name="source">Source of the information for cloning</param>
        /// <returns>Result of the cloning operation</returns>
        Task<TResult> CloneAsync(TTarget target, TSource source);
    }
}
