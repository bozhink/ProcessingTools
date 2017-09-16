// <copyright file="IFactory{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    /// <summary>
    /// Represents simple factory.
    /// </summary>
    /// <typeparam name="T">Type of the result</typeparam>
    public interface IFactory<out T>
    {
        /// <summary>
        /// Creates the default instance for the resultant type.
        /// </summary>
        /// <returns>Default instance for the resultant type</returns>
        T Create();
    }
}
