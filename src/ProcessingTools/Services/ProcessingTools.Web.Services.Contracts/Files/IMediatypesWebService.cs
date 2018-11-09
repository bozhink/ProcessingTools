// <copyright file="IMediatypesWebService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Files
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatypes web service.
    /// </summary>
    public interface IMediatypesWebService : IWebPresenter
    {
        /// <summary>
        /// Get <see cref="MediatypesIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="MediatypesIndexViewModel"/>.</returns>
        Task<MediatypesIndexViewModel> GetMediatypesIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="MediatypeCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="MediatypeCreateViewModel"/>.</returns>
        Task<MediatypeCreateViewModel> GetMediatypeCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="MediatypeEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the mediatype.</param>
        /// <returns>Task of <see cref="MediatypeEditViewModel"/>.</returns>
        Task<MediatypeEditViewModel> GetMediatypeEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="MediatypeDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the mediatype.</param>
        /// <returns>Task of <see cref="MediatypeDeleteViewModel"/>.</returns>
        Task<MediatypeDeleteViewModel> GetMediatypeDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="MediatypeDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the mediatype.</param>
        /// <returns>Task of <see cref="MediatypeDetailsViewModel"/>.</returns>
        Task<MediatypeDetailsViewModel> GetMediatypeDetailsViewModelAsync(string id);

        /// <summary>
        /// Create mediatype.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreateMediatypeAsync(MediatypeCreateRequestModel model);

        /// <summary>
        /// Update mediatype.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdateMediatypeAsync(MediatypeUpdateRequestModel model);

        /// <summary>
        /// Delete mediatype.
        /// </summary>
        /// <param name="id">ID of mediatype to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeleteMediatypeAsync(string id);

        /// <summary>
        /// Map <see cref="MediatypeCreateRequestModel"/> to <see cref="MediatypeCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<MediatypeCreateViewModel> MapToViewModelAsync(MediatypeCreateRequestModel model);

        /// <summary>
        /// Map <see cref="MediatypeUpdateRequestModel"/> to <see cref="MediatypeEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<MediatypeEditViewModel> MapToViewModelAsync(MediatypeUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="MediatypeDeleteRequestModel"/> to <see cref="MediatypeDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<MediatypeDeleteViewModel> MapToViewModelAsync(MediatypeDeleteRequestModel model);
    }
}
