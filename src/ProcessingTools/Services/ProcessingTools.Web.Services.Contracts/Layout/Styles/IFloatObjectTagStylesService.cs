// <copyright file="IFloatObjectTagStylesService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object tag styles service.
    /// </summary>
    public interface IFloatObjectTagStylesService
    {
        /// <summary>
        /// Get <see cref="FloatObjectTagStylesIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="FloatObjectTagStylesIndexViewModel"/>.</returns>
        Task<FloatObjectTagStylesIndexViewModel> GetFloatObjectTagStylesIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="FloatObjectTagStyleCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="FloatObjectTagStyleCreateViewModel"/>.</returns>
        Task<FloatObjectTagStyleCreateViewModel> GetFloatObjectTagStyleCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="FloatObjectTagStyleEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the float object tag style.</param>
        /// <returns>Task of <see cref="FloatObjectTagStyleEditViewModel"/>.</returns>
        Task<FloatObjectTagStyleEditViewModel> GetFloatObjectTagStyleEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="FloatObjectTagStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the float object tag style.</param>
        /// <returns>Task of <see cref="FloatObjectTagStyleDeleteViewModel"/>.</returns>
        Task<FloatObjectTagStyleDeleteViewModel> GetFloatObjectTagStyleDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="FloatObjectTagStyleDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the float object tag style.</param>
        /// <returns>Task of <see cref="FloatObjectTagStyleDetailsViewModel"/>.</returns>
        Task<FloatObjectTagStyleDetailsViewModel> GetFloatObjectTagStyleDetailsViewModelAsync(string id);

        /// <summary>
        /// Create float object tag style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateFloatObjectTagStyleAsync(FloatObjectTagStyleCreateRequestModel model);

        /// <summary>
        /// Update float object tag style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateFloatObjectTagStyleAsync(FloatObjectTagStyleUpdateRequestModel model);

        /// <summary>
        /// Delete float object tag style.
        /// </summary>
        /// <param name="id">ID of float object tag style to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteFloatObjectTagStyleAsync(string id);

        /// <summary>
        /// Map <see cref="FloatObjectTagStyleCreateRequestModel"/> to <see cref="FloatObjectTagStyleCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<FloatObjectTagStyleCreateViewModel> MapToViewModelAsync(FloatObjectTagStyleCreateRequestModel model);

        /// <summary>
        /// Map <see cref="FloatObjectTagStyleUpdateRequestModel"/> to <see cref="FloatObjectTagStyleEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<FloatObjectTagStyleEditViewModel> MapToViewModelAsync(FloatObjectTagStyleUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="FloatObjectTagStyleDeleteRequestModel"/> to <see cref="FloatObjectTagStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<FloatObjectTagStyleDeleteViewModel> MapToViewModelAsync(FloatObjectTagStyleDeleteRequestModel model);
    }
}
