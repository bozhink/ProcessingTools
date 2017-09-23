// <copyright file="IIdentifiable{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with generic ID.
    /// </summary>
    public interface IIdentifiable<out T>
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        T Id { get; }
    }
}
