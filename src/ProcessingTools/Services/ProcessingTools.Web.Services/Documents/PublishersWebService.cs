// <copyright file="PublishersWebService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Services.Models.Documents.Publishers;
    using ProcessingTools.Contracts.Web.Services.Documents;
    using ProcessingTools.Web.Models.Documents.Publishers;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Publishers web service.
    /// </summary>
    public class PublishersWebService : IPublishersWebService
    {
        private readonly IPublishersDataService publishersDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersWebService"/> class.
        /// </summary>
        /// <param name="publishersDataService">Instance of <see cref="IPublishersDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        /// <param name="userContext">User context.</param>
        public PublishersWebService(IPublishersDataService publishersDataService, IMapper mapper, IUserContext userContext)
        {
            if (userContext is null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.publishersDataService = publishersDataService ?? throw new ArgumentNullException(nameof(publishersDataService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<bool> CreatePublisherAsync(PublisherCreateRequestModel model)
        {
            if (model is null)
            {
                return false;
            }

            var result = await this.publishersDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeletePublisherAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.publishersDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdatePublisherAsync(PublisherUpdateRequestModel model)
        {
            if (model is null)
            {
                return false;
            }

            var result = await this.publishersDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<PublisherCreateViewModel> GetPublisherCreateViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            return new PublisherCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherDeleteViewModel> GetPublisherDeleteViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var publisher = await this.publishersDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (publisher != null)
                {
                    var viewModel = new PublisherDeleteViewModel(userContext);
                    this.mapper.Map(publisher, viewModel);

                    return viewModel;
                }
            }

            return new PublisherDeleteViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherDetailsViewModel> GetPublisherDetailsViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var publisher = await this.publishersDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (publisher != null)
                {
                    var viewModel = new PublisherDetailsViewModel(userContext);
                    this.mapper.Map(publisher, viewModel);

                    return viewModel;
                }
            }

            return new PublisherDetailsViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherEditViewModel> GetPublisherEditViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var publisher = await this.publishersDataService.GetByIdAsync(id).ConfigureAwait(false);
                if (publisher != null)
                {
                    var viewModel = new PublisherEditViewModel(userContext);
                    this.mapper.Map(publisher, viewModel);

                    return viewModel;
                }
            }

            return new PublisherEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<PublishersIndexViewModel> GetPublishersIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            var data = await this.publishersDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.publishersDataService.SelectCountAsync().ConfigureAwait(false);

            var publishers = data?.Select(this.mapper.Map<IPublisherModel, PublisherIndexViewModel>).ToArray() ?? Array.Empty<PublisherIndexViewModel>();

            return new PublishersIndexViewModel(userContext, count, take, skip / take, publishers);
        }

        /// <inheritdoc/>
        public async Task<PublisherCreateViewModel> MapToViewModelAsync(PublisherCreateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = new PublisherCreateViewModel(userContext);
                this.mapper.Map(model, viewModel);

                return viewModel;
            }

            return new PublisherCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherEditViewModel> MapToViewModelAsync(PublisherUpdateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var publisher = await this.publishersDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (publisher != null)
                {
                    var viewModel = new PublisherEditViewModel(userContext);
                    this.mapper.Map(model, viewModel);

                    viewModel.CreatedBy = publisher.CreatedBy;
                    viewModel.CreatedOn = publisher.CreatedOn;
                    viewModel.ModifiedBy = publisher.ModifiedBy;
                    viewModel.ModifiedOn = publisher.ModifiedOn;

                    return viewModel;
                }
            }

            return new PublisherEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherDeleteViewModel> MapToViewModelAsync(PublisherDeleteRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var publisher = await this.publishersDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (publisher != null)
                {
                    var viewModel = new PublisherDeleteViewModel(userContext);
                    this.mapper.Map(publisher, viewModel);

                    return viewModel;
                }
            }

            return new PublisherDeleteViewModel(userContext);
        }
    }
}
