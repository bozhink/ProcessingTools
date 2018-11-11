// <copyright file="IIdentifiable{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts
{
    /// <summary>
    /// Model with generic ID.
    /// </summary>
    /// <typeparam name="T">Type of the ID.</typeparam>
    public interface IIdentifiable<out T>
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        T Id { get; }
    }
}
