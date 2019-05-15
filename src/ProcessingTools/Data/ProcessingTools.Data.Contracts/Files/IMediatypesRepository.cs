﻿// <copyright file="IMediatypesRepository.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Files
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    /// <summary>
    /// Mediatypes repository.
    /// </summary>
    public interface IMediatypesRepository
    {
        /// <summary>
        /// Add new entity.
        /// </summary>
        /// <param name="mediatype">Entity to be added.</param>
        /// <returns>Task of result.</returns>
        Task<object> Add(IMediatypeBaseModel mediatype);

        /// <summary>
        /// Remove entity by file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>Task of result.</returns>
        Task<object> Remove(string fileExtension);

        /// <summary>
        /// Update description of entity specified by its file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <param name="description">Description to be set.</param>
        /// <returns>Task of result.</returns>
        Task<object> UpdateDescription(string fileExtension, string description);

        /// <summary>
        /// Save changed.
        /// </summary>
        /// <returns>Task of result.</returns>
        Task<long> SaveChanges();
    }
}
