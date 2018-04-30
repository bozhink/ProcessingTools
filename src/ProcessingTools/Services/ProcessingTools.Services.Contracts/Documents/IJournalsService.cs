// <copyright file="IJournalsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Documents.Journals;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;

    /// <summary>
    /// Journals service.
    /// </summary>
    public interface IJournalsService
    {
        /// <summary>
        /// Inserts new journal.
        /// </summary>
        /// <param name="model">Journal model for insert operation.</param>
        /// <returns>Resultant object.</returns>
        Task<object> InsertAsync(IJournalInsertModel model);

        /// <summary>
        /// Updates journal.
        /// </summary>
        /// <param name="model">Journal model for update operation.</param>
        /// <returns>Resultant object.</returns>
        Task<object> UpdateAsync(IJournalUpdateModel model);

        /// <summary>
        /// Deletes journal specified by ID.
        /// </summary>
        /// <param name="id">Object ID of the journal.</param>
        /// <returns>Resultant object.</returns>
        Task<object> DeleteAsync(object id);

        /// <summary>
        /// Gets journal specified by ID.
        /// </summary>
        /// <param name="id">Object ID of the journal.</param>
        /// <returns>Journal.</returns>
        Task<IJournalModel> GetByIdAsync(object id);

        /// <summary>
        /// Gets journal details specified by ID.
        /// </summary>
        /// <param name="id">Object ID of the journal.</param>
        /// <returns>Journal details.</returns>
        Task<IJournalDetailsModel> GetDetailsByIdAsync(object id);

        /// <summary>
        /// Select journals for pagination.
        /// </summary>
        /// <param name="skip">Number of journals to skip.</param>
        /// <param name="take">Number of journals to take.</param>
        /// <returns>Array of journals.</returns>
        Task<IJournalModel[]> SelectAsync(int skip, int take);

        /// <summary>
        /// Select journal details for pagination.
        /// </summary>
        /// <param name="skip">Number of journal details to skip.</param>
        /// <param name="take">Number of journal details to take.</param>
        /// <returns>Array of journal details.</returns>
        Task<IJournalDetailsModel[]> SelectDetailsAsync(int skip, int take);

        /// <summary>
        /// Gets the number of journals.
        /// </summary>
        /// <returns>Number of journals.</returns>
        Task<long> SelectCountAsync();

        /// <summary>
        /// Gets journal publishers for select.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IJournalPublisherModel[]> GetJournalPublishersForSelectAsync();

        /// <summary>
        /// Gets journal style specified by ID.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Journal style.</returns>
        Task<IIdentifiedStyleModel> GetJournalStyleByIdAsync(object id);

        /// <summary>
        /// Gets journal styles for select.
        /// </summary>
        /// <returns>Array of journal styles.</returns>
        Task<IIdentifiedStyleModel[]> GetJournalStylesForSelectAsync();
    }
}
