// <copyright file="ReferenceParseStylesWebService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Layout.Styles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Layout.Styles;
    using ProcessingTools.Contracts.Web.Services.Layout.Styles;
    using ProcessingTools.Web.Models.Layout.Styles.References;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Reference parse styles web service.
    /// </summary>
    public class ReferenceParseStylesWebService : IReferenceParseStylesWebService
    {
        private readonly IReferenceParseStylesDataService referenceParseStylesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStylesWebService"/> class.
        /// </summary>
        /// <param name="referenceParseStylesDataService">Instance of <see cref="IReferenceParseStylesDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        /// <param name="userContext">User context.</param>
        public ReferenceParseStylesWebService(IReferenceParseStylesDataService referenceParseStylesDataService, IMapper mapper, IUserContext userContext)
        {
            if (userContext is null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.referenceParseStylesDataService = referenceParseStylesDataService ?? throw new ArgumentNullException(nameof(referenceParseStylesDataService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<bool> CreateReferenceParseStyleAsync(ReferenceParseStyleCreateRequestModel model)
        {
            if (model is null)
            {
                return false;
            }

            var result = await this.referenceParseStylesDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteReferenceParseStyleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.referenceParseStylesDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateReferenceParseStyleAsync(ReferenceParseStyleUpdateRequestModel model)
        {
            if (model is null)
            {
                return false;
            }

            var result = await this.referenceParseStylesDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleCreateViewModel> GetReferenceParseStyleCreateViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            return new ReferenceParseStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleDeleteViewModel> GetReferenceParseStyleDeleteViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.referenceParseStylesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceParseStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new ReferenceParseStyleDeleteViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleDetailsViewModel> GetReferenceParseStyleDetailsViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.referenceParseStylesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceParseStyleDetailsViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new ReferenceParseStyleDetailsViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleEditViewModel> GetReferenceParseStyleEditViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.referenceParseStylesDataService.GetByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceParseStyleEditViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new ReferenceParseStyleEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStylesIndexViewModel> GetReferenceParseStylesIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            var data = await this.referenceParseStylesDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.referenceParseStylesDataService.SelectCountAsync().ConfigureAwait(false);

            var styles = data?.Select(this.mapper.Map<ReferenceParseStyleIndexViewModel>).ToArray() ?? Array.Empty<ReferenceParseStyleIndexViewModel>();

            return new ReferenceParseStylesIndexViewModel(userContext, count, take, skip / take, styles);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleCreateViewModel> MapToViewModelAsync(ReferenceParseStyleCreateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = new ReferenceParseStyleCreateViewModel(userContext);
                this.mapper.Map(model, viewModel);

                return viewModel;
            }

            return new ReferenceParseStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleEditViewModel> MapToViewModelAsync(ReferenceParseStyleUpdateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.referenceParseStylesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceParseStyleEditViewModel(userContext);
                    this.mapper.Map(model, viewModel);

                    viewModel.CreatedBy = style.CreatedBy;
                    viewModel.CreatedOn = style.CreatedOn;
                    viewModel.ModifiedBy = style.ModifiedBy;
                    viewModel.ModifiedOn = style.ModifiedOn;

                    return viewModel;
                }
            }

            return new ReferenceParseStyleEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleDeleteViewModel> MapToViewModelAsync(ReferenceParseStyleDeleteRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.referenceParseStylesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceParseStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new ReferenceParseStyleDeleteViewModel(userContext);
        }
    }
}
