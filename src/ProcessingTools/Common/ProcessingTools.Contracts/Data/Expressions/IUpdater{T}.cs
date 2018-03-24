// <copyright file="IUpdater{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Expressions
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic updater.
    /// </summary>
    /// <typeparam name="T">Type of the updated object.</typeparam>
    public interface IUpdater<T>
    {
        /// <summary>
        /// Gets the update expression.
        /// </summary>
        IUpdateExpression<T> UpdateExpression { get; }

        /// <summary>
        /// Invokes update expression.
        /// </summary>
        /// <param name="obj">Source object to be updated.</param>
        void Invoke(T obj);

        /// <summary>
        /// Invokes update expression.
        /// </summary>
        /// <param name="obj">Source object to be updated.</param>
        /// <returns>Task.</returns>
        Task InvokeAsync(T obj);
    }
}
