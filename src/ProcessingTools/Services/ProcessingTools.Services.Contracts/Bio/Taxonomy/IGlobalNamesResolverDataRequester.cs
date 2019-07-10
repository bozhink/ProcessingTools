﻿// <copyright file="IGlobalNamesResolverDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using System.Xml;

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    /// <summary>
    /// Global Names Resolver data requester.
    /// </summary>
    public interface IGlobalNamesResolverDataRequester
    {
        /// <summary>
        /// Search with Global Names Resolver with GET request.
        /// </summary>
        /// <param name="scientificNames">Scientific names to be searched.</param>
        /// <param name="sourceId">Source IDs to use in search. NULL if default.</param>
        /// <returns>Task of <see cref="XmlDocument"/>.</returns>
        Task<XmlDocument> SearchWithGlobalNamesResolverGet(string[] scientificNames, int[] sourceId);

        /// <summary>
        /// Search with Global Names Resolver with POST request.
        /// </summary>
        /// <param name="scientificNames">Scientific names to be searched.</param>
        /// <param name="sourceId">Source IDs to use in search. NULL if default.</param>
        /// <returns>Task of <see cref="XmlDocument"/>.</returns>
        Task<XmlDocument> SearchWithGlobalNamesResolverPost(string[] scientificNames, int[] sourceId);

        /// <summary>
        /// Search with Global Names Resolver with POST request.
        /// </summary>
        /// <param name="scientificNames">Scientific names to be searched.</param>
        /// <param name="sourceId">Source IDs to use in search. NULL if default.</param>
        /// <returns>Task of <see cref="XmlDocument"/>.</returns>
        Task<XmlDocument> SearchWithGlobalNamesResolverPostNewerRequestVersion(string[] scientificNames, int[] sourceId);
    }
}
