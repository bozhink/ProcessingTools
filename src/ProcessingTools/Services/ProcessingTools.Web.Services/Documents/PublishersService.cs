// <copyright file="PublishersService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Documents.Publishers;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// Publishers service.
    /// </summary>
    public class PublishersService : IPublishersService
    {
        /// <inheritdoc/>
        public Task<bool> CreatePublisherAsync(PublisherCreateRequestModel model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<bool> DeletePublisherAsync(PublisherEditRequestModel model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<bool> EditPublisherAsync(PublisherEditRequestModel model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<PublisherCreateViewModel> GetPublisherCreateViewModelAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<PublisherDeleteViewModel> GetPublisherDeleteViewModelAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<PublisherDetailsViewModel> GetPublisherDetailsViewModelAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<PublisherEditViewModel> GetPublisherEditViewModelAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<PublishersIndexViewModel> GetPublishersIndexViewModelAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<PublisherCreateViewModel> MapToViewModelAsync(PublisherCreateRequestModel model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<PublisherEditViewModel> MapToViewModelAsync(PublisherEditRequestModel model)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<PublisherDeleteViewModel> MapToViewModelAsync(PublisherDeleteRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
