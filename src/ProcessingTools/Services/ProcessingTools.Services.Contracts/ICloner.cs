// <copyright file="ICloner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Represents source-to-target cloner.
    /// </summary>
    /// <typeparam name="TTarget">Type of the target of cloning.</typeparam>
    /// <typeparam name="TSource">Type of the source of information.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface ICloner<in TTarget, in TSource, TResult>
    {
        /// <summary>
        /// Clones information from source to target.
        /// </summary>
        /// <param name="target">Target of cloning.</param>
        /// <param name="source">Source of the information for cloning.</param>
        /// <returns>Result of the cloning operation.</returns>
        Task<TResult> CloneAsync(TTarget target, TSource source);
    }
}
