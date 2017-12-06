// <copyright file="IGeoSynonymisableDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Data.Geo
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Geo-synonymisable data service.
    /// </summary>
    /// <typeparam name="TModel">Type of service model.</typeparam>
    /// <typeparam name="TSynonym">Type of synonym model.</typeparam>
    /// <typeparam name="TSynonymFilter">Type of synonyms filter.</typeparam>
    public interface IGeoSynonymisableDataService<TModel, TSynonym, TSynonymFilter>
        where TModel : IGeoSynonymisable<TSynonym>
        where TSynonym : IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        /// <summary>
        /// Inserts synonyms to specified service model.
        /// </summary>
        /// <param name="model">Service model to be updated.</param>
        /// <param name="synonyms">Synonyms to be inserted.</param>
        /// <returns>Task</returns>
        Task<object> InsertAsync(TModel model, params TSynonym[] synonyms);

        /// <summary>
        /// Adds synonyms to specified service model.
        /// </summary>
        /// <param name="modelId">ID of the service model.</param>
        /// <param name="synonyms">Synonyms to be added.</param>
        /// <returns>Task</returns>
        Task<object> AddSynonymsAsync(int modelId, params TSynonym[] synonyms);

        /// <summary>
        /// Removes synonyms from specified service model.
        /// </summary>
        /// <param name="modelId">ID of the service model.</param>
        /// <param name="synonymIds">IDs of the synonyms to be removed.</param>
        /// <returns>Task</returns>
        Task<object> RemoveSynonymsAsync(int modelId, params int[] synonymIds);

        /// <summary>
        /// Updates synonyms of specified service model.
        /// </summary>
        /// <param name="modelId">ID of the service model.</param>
        /// <param name="synonyms">Synonyms to be updated.</param>
        /// <returns>Task</returns>
        Task<object> UpdateSynonymsAsync(int modelId, params TSynonym[] synonyms);

        /// <summary>
        /// Selects synonyms of specified service model.
        /// </summary>
        /// <param name="modelId">ID of the service model.</param>
        /// <param name="filter">Synonym filter for the selection.</param>
        /// <returns>Array of synonyms.</returns>
        Task<TSynonym[]> SelectSynonymsAsync(int modelId, TSynonymFilter filter);

        /// <summary>
        /// Gets the count of synonyms of specified service model.
        /// </summary>
        /// <param name="modelId">ID of the service model.</param>
        /// <param name="filter">Synonym filter for the selection.</param>
        /// <returns>Number of synonyms.</returns>
        Task<long> SelectSynonymCountAsync(int modelId, TSynonymFilter filter);

        /// <summary>
        /// Gets synonym by ID.
        /// </summary>
        /// <param name="modelId">ID of the service model.</param>
        /// <param name="id">ID of the synonym.</param>
        /// <returns>Synonym.</returns>
        Task<TSynonym> GetSynonymByIdAsync(int modelId, int id);
    }
}
