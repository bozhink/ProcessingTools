// <copyright file="IFloatObjectParseStylesWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object parse styles web service.
    /// </summary>
    public interface IFloatObjectParseStylesWebService : IWebPresenter
    {
        /// <summary>
        /// Get <see cref="FloatObjectParseStylesIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="FloatObjectParseStylesIndexViewModel"/>.</returns>
        Task<FloatObjectParseStylesIndexViewModel> GetFloatObjectParseStylesIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="FloatObjectParseStyleCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="FloatObjectParseStyleCreateViewModel"/>.</returns>
        Task<FloatObjectParseStyleCreateViewModel> GetFloatObjectParseStyleCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="FloatObjectParseStyleEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the float object parse style.</param>
        /// <returns>Task of <see cref="FloatObjectParseStyleEditViewModel"/>.</returns>
        Task<FloatObjectParseStyleEditViewModel> GetFloatObjectParseStyleEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="FloatObjectParseStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the float object parse style.</param>
        /// <returns>Task of <see cref="FloatObjectParseStyleDeleteViewModel"/>.</returns>
        Task<FloatObjectParseStyleDeleteViewModel> GetFloatObjectParseStyleDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="FloatObjectParseStyleDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the float object parse style.</param>
        /// <returns>Task of <see cref="FloatObjectParseStyleDetailsViewModel"/>.</returns>
        Task<FloatObjectParseStyleDetailsViewModel> GetFloatObjectParseStyleDetailsViewModelAsync(string id);

        /// <summary>
        /// Create float object parse style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateFloatObjectParseStyleAsync(FloatObjectParseStyleCreateRequestModel model);

        /// <summary>
        /// Update float object parse style.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateFloatObjectParseStyleAsync(FloatObjectParseStyleUpdateRequestModel model);

        /// <summary>
        /// Delete float object parse style.
        /// </summary>
        /// <param name="id">ID of float object parse style to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteFloatObjectParseStyleAsync(string id);

        /// <summary>
        /// Map <see cref="FloatObjectParseStyleCreateRequestModel"/> to <see cref="FloatObjectParseStyleCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<FloatObjectParseStyleCreateViewModel> MapToViewModelAsync(FloatObjectParseStyleCreateRequestModel model);

        /// <summary>
        /// Map <see cref="FloatObjectParseStyleUpdateRequestModel"/> to <see cref="FloatObjectParseStyleEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<FloatObjectParseStyleEditViewModel> MapToViewModelAsync(FloatObjectParseStyleUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="FloatObjectParseStyleDeleteRequestModel"/> to <see cref="FloatObjectParseStyleDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<FloatObjectParseStyleDeleteViewModel> MapToViewModelAsync(FloatObjectParseStyleDeleteRequestModel model);
    }
}
