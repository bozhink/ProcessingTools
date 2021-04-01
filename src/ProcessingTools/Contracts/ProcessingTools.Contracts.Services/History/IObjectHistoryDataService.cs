﻿// <copyright file="IObjectHistoryDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.History
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.History;

    /// <summary>
    /// Object histories data service.
    /// </summary>
    public interface IObjectHistoryDataService
    {
        /// <summary>
        /// Adds history entry of specified object.
        /// </summary>
        /// <param name="objectId">ID of the object to be added to history.</param>
        /// <param name="source">Source object to be added to history.</param>
        /// <returns>Task.</returns>
        Task<object> AddAsync(object objectId, object source);

        /// <summary>
        /// Gets deserialized object histories of object specifies by Id and <see cref="Type"/>.
        /// </summary>
        /// <param name="objectId">ID of the object in history.</param>
        /// <param name="objectType"><see cref="Type"/> of the object in history.</param>
        /// <returns>Deserialized objects from history.</returns>
        Task<IList<object>> GetAsync(object objectId, Type objectType);

        /// <summary>
        /// Gets deserialized object histories of object specifies by Id and <see cref="Type"/>.
        /// </summary>
        /// <param name="objectId">ID of the object in history.</param>
        /// <param name="objectType"><see cref="Type"/> of the object in history.</param>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Deserialized objects from history.</returns>
        Task<IList<object>> GetAsync(object objectId, Type objectType, int skip, int take);

        /// <summary>
        /// Gets object histories of object specifies by Id.
        /// </summary>
        /// <param name="objectId">ID of the object in history.</param>
        /// <returns>Object histories.</returns>
        Task<IList<IObjectHistory>> GetHistoriesAsync(object objectId);

        /// <summary>
        /// Gets object histories of object specifies by Id.
        /// </summary>
        /// <param name="objectId">ID of the object in history.</param>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Object histories.</returns>
        Task<IList<IObjectHistory>> GetHistoriesAsync(object objectId, int skip, int take);
    }
}
