// <copyright file="PublishersService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Models.Contracts.Documents.Publishers;
    using ProcessingTools.Web.Models.Documents.Publishers;
    using ProcessingTools.Web.Models.Shared;
    using ProcessingTools.Web.Services.Contracts.Documents;

    /// <summary>
    /// Publishers service.
    /// </summary>
    public class PublishersService : IPublishersService
    {
        private readonly IPublishersDataService publishersDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishersService"/> class.
        /// </summary>
        /// <param name="publishersDataService">Instance of <see cref="IPublishersDataService"/>.</param>
        /// <param name="userContext">User context.</param>
        public PublishersService(IPublishersDataService publishersDataService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.publishersDataService = publishersDataService ?? throw new ArgumentNullException(nameof(publishersDataService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<PublisherCreateRequestModel, PublisherCreateViewModel>();
                c.CreateMap<PublisherUpdateRequestModel, PublisherEditViewModel>();
                c.CreateMap<PublisherDeleteRequestModel, PublisherDeleteViewModel>();

                c.CreateMap<IPublisherModel, PublisherDeleteViewModel>();
                c.CreateMap<IPublisherModel, PublisherDetailsViewModel>();
                c.CreateMap<IPublisherModel, PublisherEditViewModel>();
                c.CreateMap<IPublisherModel, PublisherIndexViewModel>();
                c.CreateMap<IPublisherDetailsModel, PublisherDeleteViewModel>();
                c.CreateMap<IPublisherDetailsModel, PublisherDetailsViewModel>();
                c.CreateMap<IPublisherDetailsModel, PublisherEditViewModel>();
                c.CreateMap<IPublisherDetailsModel, PublisherIndexViewModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<bool> CreatePublisherAsync(PublisherCreateRequestModel model)
        {
            if (model == null)
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
            if (model == null)
            {
                return false;
            }

            var result = await this.publishersDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<PublisherCreateViewModel> GetPublisherCreateViewModelAsync()
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            return new PublisherCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherDeleteViewModel> GetPublisherDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.publishersDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.publishersDataService.SelectCountAsync().ConfigureAwait(false);

            var publishers = data?.Select(this.mapper.Map<IPublisherModel, PublisherIndexViewModel>).ToArray() ?? new PublisherIndexViewModel[] { };

            return new PublishersIndexViewModel(userContext, count, take, skip / take, publishers);
        }

        /// <inheritdoc/>
        public async Task<PublisherCreateViewModel> MapToViewModelAsync(PublisherCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
