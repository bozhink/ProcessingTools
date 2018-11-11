// <copyright file="IStrategiesProvider{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Strategies
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents provider of strategies.
    /// </summary>
    /// <typeparam name="T">Type of the strategy.</typeparam>
    public interface IStrategiesProvider<out T>
        where T : IStrategy
    {
        /// <summary>
        /// Gets the provided strategies.
        /// </summary>
        IEnumerable<T> Strategies { get; }
    }
}
