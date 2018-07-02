// <copyright file="IGlobalNamesResolverDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Contracts.Bio.Taxonomy
{
    using System.Threading.Tasks;
    using System.Xml;

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
