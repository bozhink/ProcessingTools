// <copyright file="IEnvoTerm.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Bio.Environments
{
    /// <summary>
    /// ENVO term model.
    /// </summary>
    public interface IEnvoTerm
    {
        /// <summary>
        /// Gets content.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// Gets entity ID.
        /// </summary>
        string EntityId { get; }

        /// <summary>
        /// Gets ENVO ID.
        /// </summary>
        string EnvoId { get; }
    }
}
