// <copyright file="IGeoSynonymisableRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Geo synonymisable repository.
    /// </summary>
    /// <typeparam name="TModel">Type of the model.</typeparam>
    /// <typeparam name="TSynonym">Type of the synonym.</typeparam>
    /// <typeparam name="TSynonymFilter">Type of the synonym filter.</typeparam>
    public interface IGeoSynonymisableRepository<TModel, TSynonym, TSynonymFilter>
        where TModel : IGeoSynonymisable<TSynonym>
        where TSynonym : IGeoSynonym
        where TSynonymFilter : ISynonymFilter
    {
        /// <summary>
        /// Inserts model and its synonyms at once.
        /// </summary>
        /// <param name="model">Model object to be inserted.</param>
        /// <param name="synonyms">Synonyms to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertAsync(TModel model, params TSynonym[] synonyms);

        /// <summary>
        /// Adds synonyms to existing model.
        /// </summary>
        /// <param name="modelId">ID of the existing model to be updated.</param>
        /// <param name="synonyms">Synonyms to be inserted.</param>
        /// <returns>Task of result.</returns>
        Task<object> AddSynonymsAsync(int modelId, params TSynonym[] synonyms);

        /// <summary>
        /// Remove synonyms from a specified model.
        /// </summary>
        /// <param name="modelId">ID of the model to be updated.</param>
        /// <param name="synonymIds">ID-s of the synonyms to be removed.</param>
        /// <returns>Task of result.</returns>
        Task<object> RemoveSynonymsAsync(int modelId, params int[] synonymIds);

        /// <summary>
        /// Update synonyms of a specified model.
        /// </summary>
        /// <param name="modelId">ID of the model to be updated.</param>
        /// <param name="synonyms">Synonyms to be updated.</param>
        /// <returns>Task of results.</returns>
        Task<object> UpdateSynonymsAsync(int modelId, params TSynonym[] synonyms);

        /// <summary>
        /// Retrieves synonyms of a specified model.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <param name="filter">Filter to be applied on the synonyms.</param>
        /// <returns>Task of synonyms matching filter.</returns>
        Task<TSynonym[]> SelectSynonymsAsync(int modelId, TSynonymFilter filter);

        /// <summary>
        /// Retrieves number synonyms of a specified model.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <param name="filter">Filter to be applied on the synonyms.</param>
        /// <returns>Task of the number of synonyms matching filter.</returns>
        Task<long> SelectSynonymCountAsync(int modelId, TSynonymFilter filter);

        /// <summary>
        /// Gets synonym by id.
        /// </summary>
        /// <param name="modelId">ID of the model.</param>
        /// <param name="id">ID of the synonym.</param>
        /// <returns>Task of result.</returns>
        Task<TSynonym> GetSynonymByIdAsync(int modelId, int id);
    }
}
