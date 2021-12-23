// <copyright file="IIdentified{T}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models
{
    /// <summary>
    /// Model with generic ID.
    /// </summary>
    /// <typeparam name="T">Type of the ID.</typeparam>
    public interface IIdentified<out T>
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        T Id { get; }
    }
}
