// <copyright file="IReferenceTagStylesWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Layout.Styles.References;

    /// <summary>
    /// Reference tag styles web service.
    /// </summary>
    public interface IReferenceTagStylesWebService : IWebPresenter
    {
        /// <summary>
        /// Get <see cref="ReferenceTagStylesIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="ReferenceTagStylesIndexViewModel"/>.</returns>
        Task<ReferenceTagStylesIndexViewModel> GetReferenceTagStylesIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="ReferenceTagStyleCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="ReferenceTagStyleCreateViewModel"/>.</returns>
        Task<ReferenceTagStyleCreateViewModel> GetReferenceTagStyleCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="ReferenceTagStyleEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the reference tag style.</param>
        /// <returns>Task of <see cref="ReferenceTagStyleEditViewModel"/>.</returns>
        Task<ReferenceTagStyleEditViewModel> GetReferenceTagStyleEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="ReferenceTagStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the reference tag style.</param>
        /// <returns>Task of <see cref="ReferenceTagStyleDeleteViewModel"/>.</returns>
        Task<ReferenceTagStyleDeleteViewModel> GetReferenceTagStyleDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="ReferenceTagStyleDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the reference tag style.</param>
        /// <returns>Task of <see cref="ReferenceTagStyleDetailsViewModel"/>.</returns>
        Task<ReferenceTagStyleDetailsViewModel> GetReferenceTagStyleDetailsViewModelAsync(string id);

        /// <summary>
        /// Create reference tag style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateReferenceTagStyleAsync(ReferenceTagStyleCreateRequestModel model);

        /// <summary>
        /// Update reference tag style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateReferenceTagStyleAsync(ReferenceTagStyleUpdateRequestModel model);

        /// <summary>
        /// Delete reference tag style.
        /// </summary>
        /// <param name="id">ID of reference tag style to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteReferenceTagStyleAsync(string id);

        /// <summary>
        /// Map <see cref="ReferenceTagStyleCreateRequestModel"/> to <see cref="ReferenceTagStyleCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ReferenceTagStyleCreateViewModel> MapToViewModelAsync(ReferenceTagStyleCreateRequestModel model);

        /// <summary>
        /// Map <see cref="ReferenceTagStyleUpdateRequestModel"/> to <see cref="ReferenceTagStyleEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ReferenceTagStyleEditViewModel> MapToViewModelAsync(ReferenceTagStyleUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="ReferenceTagStyleDeleteRequestModel"/> to <see cref="ReferenceTagStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<ReferenceTagStyleDeleteViewModel> MapToViewModelAsync(ReferenceTagStyleDeleteRequestModel model);
    }
}
