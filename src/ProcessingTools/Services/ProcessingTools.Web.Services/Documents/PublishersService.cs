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
        private readonly UserContext userContext;

        public PublishersService(IPublishersDataService publishersDataService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.publishersDataService = publishersDataService ?? throw new ArgumentNullException(nameof(publishersDataService));

            this.userContext = new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail);
        }

        /// <inheritdoc/>
        public async Task<bool> CreatePublisherAsync(PublisherCreateRequestModel model)
        {
            var result = await this.publishersDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeletePublisherAsync(string id)
        {
            var result = await this.publishersDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> EditPublisherAsync(PublisherEditRequestModel model)
        {
            var result = await this.publishersDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public Task<PublisherCreateViewModel> GetPublisherCreateViewModelAsync()
        {
            var viewModel = new PublisherCreateViewModel(this.userContext)
            {
            };

            return Task.FromResult(viewModel);
        }

        /// <inheritdoc/>
        public async Task<PublisherDeleteViewModel> GetPublisherDeleteViewModelAsync(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = await this.publishersDataService.GetById(id).ConfigureAwait(false);
                if (model != null)
                {
                    return new PublisherDeleteViewModel(this.userContext)
                    {
                        AbbreviatedName = model.AbbreviatedName,
                        Address = model.Address,
                        CreatedBy = model.CreatedBy,
                        CreatedOn = model.CreatedOn,
                        Id = model.Id,
                        ModifiedBy = model.ModifiedBy,
                        ModifiedOn = model.ModifiedOn,
                        Name = model.Name
                    };
                }
            }

            return new PublisherDeleteViewModel(this.userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherDetailsViewModel> GetPublisherDetailsViewModelAsync(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = await this.publishersDataService.GetById(id).ConfigureAwait(false);
                if (model != null)
                {
                    return new PublisherDetailsViewModel(this.userContext)
                    {
                        AbbreviatedName = model.AbbreviatedName,
                        Address = model.Address,
                        CreatedBy = model.CreatedBy,
                        CreatedOn = model.CreatedOn,
                        Id = model.Id,
                        ModifiedBy = model.ModifiedBy,
                        ModifiedOn = model.ModifiedOn,
                        Name = model.Name
                    };
                }
            }

            return new PublisherDetailsViewModel(this.userContext);
        }

        /// <inheritdoc/>
        public async Task<PublisherEditViewModel> GetPublisherEditViewModelAsync(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = await this.publishersDataService.GetById(id).ConfigureAwait(false);
                if (model != null)
                {
                    return new PublisherEditViewModel(this.userContext)
                    {
                        AbbreviatedName = model.AbbreviatedName,
                        Address = model.Address,
                        CreatedBy = model.CreatedBy,
                        CreatedOn = model.CreatedOn,
                        Id = model.Id,
                        ModifiedBy = model.ModifiedBy,
                        ModifiedOn = model.ModifiedOn,
                        Name = model.Name
                    };
                }
            }

            return new PublisherEditViewModel(this.userContext);
        }

        /// <inheritdoc/>
        public async Task<PublishersIndexViewModel> GetPublishersIndexViewModelAsync(int skip, int take)
        {
            var data = await this.publishersDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.publishersDataService.SelectCountAsync().ConfigureAwait(false);

            var publishers = data?.Select(p => new PublisherIndexViewModel
            {
                Id = p.Id,
                AbbreviatedName = p.AbbreviatedName,
                Address = p.Address,
                Name = p.Name,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                ModifiedBy = p.ModifiedBy,
                ModifiedOn = p.ModifiedOn
            });

            var viewModel = new PublishersIndexViewModel(this.userContext, count, take, skip / take, publishers ?? new PublisherIndexViewModel[] { });
            return viewModel;
        }

        /// <inheritdoc/>
        public Task<PublisherCreateViewModel> MapToViewModelAsync(PublisherCreateRequestModel model)
        {
            var viewModel = new PublisherCreateViewModel(this.userContext)
            {
                Name = model?.Name,
                AbbreviatedName = model?.AbbreviatedName,
                Address = model?.Address
            };

            return Task.FromResult(viewModel);
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
