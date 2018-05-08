// <copyright file="IReferenceParseStylesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Layout.Styles.References;

    /// <summary>
    /// Reference parse styles service.
    /// </summary>
    public interface IReferenceParseStylesService
    {
        /// <summary>
        /// Get <see cref="ReferenceParseStylesIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="ReferenceParseStylesIndexViewModel"/>.</returns>
        Task<ReferenceParseStylesIndexViewModel> GetReferenceParseStylesIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="ReferenceParseStyleCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="ReferenceParseStyleCreateViewModel"/>.</returns>
        Task<ReferenceParseStyleCreateViewModel> GetReferenceParseStyleCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="ReferenceParseStyleEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the reference parse style.</param>
        /// <returns>Task of <see cref="ReferenceParseStyleEditViewModel"/>.</returns>
        Task<ReferenceParseStyleEditViewModel> GetReferenceParseStyleEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="ReferenceParseStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the reference parse style.</param>
        /// <returns>Task of <see cref="ReferenceParseStyleDeleteViewModel"/>.</returns>
        Task<ReferenceParseStyleDeleteViewModel> GetReferenceParseStyleDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="ReferenceParseStyleDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the reference parse style.</param>
        /// <returns>Task of <see cref="ReferenceParseStyleDetailsViewModel"/>.</returns>
        Task<ReferenceParseStyleDetailsViewModel> GetReferenceParseStyleDetailsViewModelAsync(string id);

        /// <summary>
        /// Create reference parse style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateReferenceParseStyleAsync(ReferenceParseStyleCreateRequestModel model);

        /// <summary>
        /// Update reference parse style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateReferenceParseStyleAsync(ReferenceParseStyleUpdateRequestModel model);

        /// <summary>
        /// Delete reference parse style.
        /// </summary>
        /// <param name="id">ID of reference parse style to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteReferenceParseStyleAsync(string id);

        /// <summary>
        /// Map <see cref="ReferenceParseStyleCreateRequestModel"/> to <see cref="ReferenceParseStyleCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ReferenceParseStyleCreateViewModel> MapToViewModelAsync(ReferenceParseStyleCreateRequestModel model);

        /// <summary>
        /// Map <see cref="ReferenceParseStyleUpdateRequestModel"/> to <see cref="ReferenceParseStyleEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ReferenceParseStyleEditViewModel> MapToViewModelAsync(ReferenceParseStyleUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="ReferenceParseStyleDeleteRequestModel"/> to <see cref="ReferenceParseStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ReferenceParseStyleDeleteViewModel> MapToViewModelAsync(ReferenceParseStyleDeleteRequestModel model);
    }
}
