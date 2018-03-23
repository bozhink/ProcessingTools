// <copyright file="IJournalsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Documents.Journals;

    /// <summary>
    /// Journals service.
    /// </summary>
    public interface IJournalsService
    {
        /// <summary>
        /// Get <see cref="JournalsIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="JournalsIndexViewModel"/>.</returns>
        Task<JournalsIndexViewModel> GetJournalsIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="JournalCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="JournalCreateViewModel"/>.</returns>
        Task<JournalCreateViewModel> GetJournalCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="JournalEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the journal.</param>
        /// <returns>Task of <see cref="JournalEditViewModel"/>.</returns>
        Task<JournalEditViewModel> GetJournalEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="JournalDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the journal.</param>
        /// <returns>Task of <see cref="JournalDeleteViewModel"/>.</returns>
        Task<JournalDeleteViewModel> GetJournalDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="JournalDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the journal.</param>
        /// <returns>Task of <see cref="JournalDetailsViewModel"/>.</returns>
        Task<JournalDetailsViewModel> GetJournalDetailsViewModelAsync(string id);

        /// <summary>
        /// Create journal.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateJournalAsync(JournalCreateRequestModel model);

        /// <summary>
        /// Update journal.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateJournalAsync(JournalUpdateRequestModel model);

        /// <summary>
        /// Delete journal.
        /// </summary>
        /// <param name="id">ID of the journal to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteJournalAsync(string id);

        /// <summary>
        /// Map <see cref="JournalCreateRequestModel"/> to <see cref="JournalCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<JournalCreateViewModel> MapToViewModelAsync(JournalCreateRequestModel model);

        /// <summary>
        /// Map <see cref="JournalUpdateRequestModel"/> to <see cref="JournalEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<JournalEditViewModel> MapToViewModelAsync(JournalUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="JournalDeleteRequestModel"/> to <see cref="JournalDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<JournalDeleteViewModel> MapToViewModelAsync(JournalDeleteRequestModel model);
    }
}
