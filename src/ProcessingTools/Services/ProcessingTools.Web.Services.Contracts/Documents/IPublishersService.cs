// <copyright file="IPublishersService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Documents.Publishers;

    /// <summary>
    /// Publishers service.
    /// </summary>
    public interface IPublishersService
    {
        /// <summary>
        /// Get <see cref="PublishersIndexViewModel"/>.
        /// </summary>
        /// <param name="skip">Number of items to skip.</param>
        /// <param name="take">Number of items to take.</param>
        /// <returns>Task of <see cref="PublishersIndexViewModel"/>.</returns>
        Task<PublishersIndexViewModel> GetPublishersIndexViewModelAsync(int skip, int take);

        /// <summary>
        /// Get <see cref="PublisherCreateViewModel"/>.
        /// </summary>
        /// <returns>Task of <see cref="PublisherCreateViewModel"/>.</returns>
        Task<PublisherCreateViewModel> GetPublisherCreateViewModelAsync();

        /// <summary>
        /// Get <see cref="PublisherEditViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the publisher.</param>
        /// <returns>Task of <see cref="PublisherEditViewModel"/>.</returns>
        Task<PublisherEditViewModel> GetPublisherEditViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="PublisherDeleteViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the publisher.</param>
        /// <returns>Task of <see cref="PublisherDeleteViewModel"/>.</returns>
        Task<PublisherDeleteViewModel> GetPublisherDeleteViewModelAsync(string id);

        /// <summary>
        /// Get <see cref="PublisherDetailsViewModel"/>.
        /// </summary>
        /// <param name="id">Object ID of the publisher.</param>
        /// <returns>Task of <see cref="PublisherDetailsViewModel"/>.</returns>
        Task<PublisherDetailsViewModel> GetPublisherDetailsViewModelAsync(string id);

        /// <summary>
        /// Create publisher.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> CreatePublisherAsync(PublisherCreateRequestModel model);

        /// <summary>
        /// Update publisher.
        /// </summary>
        /// <param name="model">Model for the operation.</param>
        /// <returns>Success status.</returns>
        Task<bool> UpdatePublisherAsync(PublisherUpdateRequestModel model);

        /// <summary>
        /// Delete publisher.
        /// </summary>
        /// <param name="id">ID of publisher to be deleted.</param>
        /// <returns>Success status.</returns>
        Task<bool> DeletePublisherAsync(string id);

        /// <summary>
        /// Map <see cref="PublisherCreateRequestModel"/> to <see cref="PublisherCreateViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<PublisherCreateViewModel> MapToViewModelAsync(PublisherCreateRequestModel model);

        /// <summary>
        /// Map <see cref="PublisherUpdateRequestModel"/> to <see cref="PublisherEditViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<PublisherEditViewModel> MapToViewModelAsync(PublisherUpdateRequestModel model);

        /// <summary>
        /// Map <see cref="PublisherDeleteRequestModel"/> to <see cref="PublisherDeleteViewModel"/>.
        /// </summary>
        /// <param name="model">The model to be mapped to view model.</param>
        /// <returns>The mapped view model.</returns>
        Task<PublisherDeleteViewModel> MapToViewModelAsync(PublisherDeleteRequestModel model);
    }
}
