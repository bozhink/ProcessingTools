// <copyright file="IJournalStylesService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Layout.Styles.Journals;

    /// <summary>
    /// Journal styles service.
    /// </summary>
    public interface IJournalStylesService
    {
        /// <summary>
        /// Get <see cref="JournalStylesIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="JournalStylesIndexViewModel"/>.</returns>
        Task<JournalStylesIndexViewModel> GetJournalStylesIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="JournalStyleCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="JournalStyleCreateViewModel"/>.</returns>
        Task<JournalStyleCreateViewModel> GetJournalStyleCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="JournalStyleEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Task of <see cref="JournalStyleEditViewModel"/>.</returns>
        Task<JournalStyleEditViewModel> GetJournalStyleEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="JournalStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Task of <see cref="JournalStyleDeleteViewModel"/>.</returns>
        Task<JournalStyleDeleteViewModel> GetJournalStyleDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="JournalStyleDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the journal style.</param>
        /// <returns>Task of <see cref="JournalStyleDetailsViewModel"/>.</returns>
        Task<JournalStyleDetailsViewModel> GetJournalStyleDetailsViewModelAsync(string id);

        /// <summary>
        /// Create journal style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateJournalStyleAsync(JournalStyleCreateRequestModel model);

        /// <summary>
        /// Update journal style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateJournalStyleAsync(JournalStyleUpdateRequestModel model);

        /// <summary>
        /// Delete journal style.
        /// </summary>
        /// <param name="id">ID of journal style to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteJournalStyleAsync(string id);

        /// <summary>
        /// Map <see cref="JournalStyleCreateRequestModel"/> to <see cref="JournalStyleCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<JournalStyleCreateViewModel> MapToViewModelAsync(JournalStyleCreateRequestModel model);

        /// <summary>
        /// Map <see cref="JournalStyleUpdateRequestModel"/> to <see cref="JournalStyleEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<JournalStyleEditViewModel> MapToViewModelAsync(JournalStyleUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="JournalStyleDeleteRequestModel"/> to <see cref="JournalStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<JournalStyleDeleteViewModel> MapToViewModelAsync(JournalStyleDeleteRequestModel model);
    }
}
