// <copyright file="ReferenceTagStylesWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Layout.Styles;
using ProcessingTools.Contracts.Services.Models.Layout.Styles.References;
using ProcessingTools.Contracts.Web.Services.Layout.Styles;

namespace ProcessingTools.Web.Services.Layout.Styles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Web.Models.Layout.Styles.References;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Reference tag styles web service.
    /// </summary>
    public class ReferenceTagStylesWebService : IReferenceTagStylesWebService
    {
        private readonly IReferenceTagStylesDataService referenceTagStylesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStylesWebService"/> class.
        /// </summary>
        /// <param name="referenceTagStylesDataService">Instance of <see cref="IReferenceTagStylesDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        /// <param name="userContext">User context.</param>
        public ReferenceTagStylesWebService(IReferenceTagStylesDataService referenceTagStylesDataService, IMapper mapper, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.referenceTagStylesDataService = referenceTagStylesDataService ?? throw new ArgumentNullException(nameof(referenceTagStylesDataService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<bool> CreateReferenceTagStyleAsync(ReferenceTagStyleCreateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.referenceTagStylesDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteReferenceTagStyleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.referenceTagStylesDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateReferenceTagStyleAsync(ReferenceTagStyleUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.referenceTagStylesDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleCreateViewModel> GetReferenceTagStyleCreateViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            return new ReferenceTagStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleDeleteViewModel> GetReferenceTagStyleDeleteViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.referenceTagStylesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceTagStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new ReferenceTagStyleDeleteViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleDetailsViewModel> GetReferenceTagStyleDetailsViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.referenceTagStylesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceTagStyleDetailsViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new ReferenceTagStyleDetailsViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleEditViewModel> GetReferenceTagStyleEditViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.referenceTagStylesDataService.GetByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceTagStyleEditViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new ReferenceTagStyleEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStylesIndexViewModel> GetReferenceTagStylesIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            var data = await this.referenceTagStylesDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.referenceTagStylesDataService.SelectCountAsync().ConfigureAwait(false);

            var styles = data?.Select(this.mapper.Map<IReferenceTagStyleModel, ReferenceTagStyleIndexViewModel>).ToArray() ?? Array.Empty<ReferenceTagStyleIndexViewModel>();

            return new ReferenceTagStylesIndexViewModel(userContext, count, take, skip / take, styles);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleCreateViewModel> MapToViewModelAsync(ReferenceTagStyleCreateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = new ReferenceTagStyleCreateViewModel(userContext);
                this.mapper.Map(model, viewModel);

                return viewModel;
            }

            return new ReferenceTagStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleEditViewModel> MapToViewModelAsync(ReferenceTagStyleUpdateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.referenceTagStylesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceTagStyleEditViewModel(userContext);
                    this.mapper.Map(model, viewModel);

                    viewModel.CreatedBy = style.CreatedBy;
                    viewModel.CreatedOn = style.CreatedOn;
                    viewModel.ModifiedBy = style.ModifiedBy;
                    viewModel.ModifiedOn = style.ModifiedOn;

                    return viewModel;
                }
            }

            return new ReferenceTagStyleEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleDeleteViewModel> MapToViewModelAsync(ReferenceTagStyleDeleteRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.referenceTagStylesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new ReferenceTagStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new ReferenceTagStyleDeleteViewModel(userContext);
        }
    }
}
