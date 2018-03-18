// <copyright file="PublishersService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Documents;
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
                var publisher = await this.publishersDataService.GetDetailsById(id).ConfigureAwait(false);
                if (publisher != null)
                {
                    return new PublisherDeleteViewModel(userContext)
                    {
                        Id = publisher.Id,
                        Name = publisher.Name,
                        AbbreviatedName = publisher.AbbreviatedName,
                        Address = publisher.Address,
                        CreatedBy = publisher.CreatedBy,
                        CreatedOn = publisher.CreatedOn,
                        ModifiedBy = publisher.ModifiedBy,
                        ModifiedOn = publisher.ModifiedOn
                    };
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
                var publisher = await this.publishersDataService.GetDetailsById(id).ConfigureAwait(false);
                if (publisher != null)
                {
                    return new PublisherDetailsViewModel(userContext)
                    {
                        Id = publisher.Id,
                        Name = publisher.Name,
                        AbbreviatedName = publisher.AbbreviatedName,
                        Address = publisher.Address,
                        CreatedBy = publisher.CreatedBy,
                        CreatedOn = publisher.CreatedOn,
                        ModifiedBy = publisher.ModifiedBy,
                        ModifiedOn = publisher.ModifiedOn
                    };
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
                var publisher = await this.publishersDataService.GetDetailsById(id).ConfigureAwait(false);
                if (publisher != null)
                {
                    return new PublisherEditViewModel(userContext)
                    {
                        Id = publisher.Id,
                        AbbreviatedName = publisher.AbbreviatedName,
                        Name = publisher.Name,
                        Address = publisher.Address,
                        CreatedBy = publisher.CreatedBy,
                        CreatedOn = publisher.CreatedOn,
                        ModifiedBy = publisher.ModifiedBy,
                        ModifiedOn = publisher.ModifiedOn
                    };
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

            var publishers = data?.Select(p => new PublisherIndexViewModel
            {
                Id = p.Id,
                Name = p.Name,
                AbbreviatedName = p.AbbreviatedName,
                Address = p.Address,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                ModifiedBy = p.ModifiedBy,
                ModifiedOn = p.ModifiedOn
            });

            return new PublishersIndexViewModel(userContext, count, take, skip / take, publishers ?? new PublisherIndexViewModel[] { });
        }

        /// <inheritdoc/>
        public async Task<PublisherCreateViewModel> MapToViewModelAsync(PublisherCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null)
            {
                return new PublisherCreateViewModel(userContext)
                {
                    Name = model.Name,
                    AbbreviatedName = model.AbbreviatedName,
                    Address = model.Address
                };
            }

            return new PublisherCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherEditViewModel> MapToViewModelAsync(PublisherUpdateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var publisher = await this.publishersDataService.GetDetailsById(model.Id).ConfigureAwait(false);
                if (publisher != null)
                {
                    return new PublisherEditViewModel(userContext)
                    {
                        Id = model.Id,
                        Name = model.Name,
                        AbbreviatedName = model.AbbreviatedName,
                        Address = model.Address,
                        CreatedBy = publisher.CreatedBy,
                        CreatedOn = publisher.CreatedOn,
                        ModifiedBy = publisher.ModifiedBy,
                        ModifiedOn = publisher.ModifiedOn
                    };
                }
            }

            return new PublisherEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public Task<PublisherDeleteViewModel> MapToViewModelAsync(PublisherDeleteRequestModel model)
        {
            return this.GetPublisherDeleteViewModelAsync(model?.Id);
        }
    }
}
