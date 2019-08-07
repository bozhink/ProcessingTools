﻿// <copyright file="FloatObjectTagStylesWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Web.Models.Layout.Styles.Floats;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Float object tag styles web service.
    /// </summary>
    public class FloatObjectTagStylesWebService : IFloatObjectTagStylesWebService
    {
        private readonly IFloatObjectTagStylesDataService floatObjectTagStylesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectTagStylesWebService"/> class.
        /// </summary>
        /// <param name="floatObjectTagStylesDataService">Instance of <see cref="IFloatObjectTagStylesDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        /// <param name="userContext">User context.</param>
        public FloatObjectTagStylesWebService(IFloatObjectTagStylesDataService floatObjectTagStylesDataService, IMapper mapper, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.floatObjectTagStylesDataService = floatObjectTagStylesDataService ?? throw new ArgumentNullException(nameof(floatObjectTagStylesDataService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<bool> CreateFloatObjectTagStyleAsync(FloatObjectTagStyleCreateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.floatObjectTagStylesDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteFloatObjectTagStyleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.floatObjectTagStylesDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateFloatObjectTagStyleAsync(FloatObjectTagStyleUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.floatObjectTagStylesDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleCreateViewModel> GetFloatObjectTagStyleCreateViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            return new FloatObjectTagStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleDeleteViewModel> GetFloatObjectTagStyleDeleteViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.floatObjectTagStylesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectTagStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new FloatObjectTagStyleDeleteViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleDetailsViewModel> GetFloatObjectTagStyleDetailsViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.floatObjectTagStylesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectTagStyleDetailsViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new FloatObjectTagStyleDetailsViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleEditViewModel> GetFloatObjectTagStyleEditViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.floatObjectTagStylesDataService.GetByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectTagStyleEditViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new FloatObjectTagStyleEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStylesIndexViewModel> GetFloatObjectTagStylesIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            var data = await this.floatObjectTagStylesDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.floatObjectTagStylesDataService.SelectCountAsync().ConfigureAwait(false);

            var styles = data?.Select(this.mapper.Map<FloatObjectTagStyleIndexViewModel>).ToArray() ?? Array.Empty<FloatObjectTagStyleIndexViewModel>();

            return new FloatObjectTagStylesIndexViewModel(userContext, count, take, skip / take, styles);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleCreateViewModel> MapToViewModelAsync(FloatObjectTagStyleCreateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = new FloatObjectTagStyleCreateViewModel(userContext);
                this.mapper.Map(model, viewModel);

                return viewModel;
            }

            return new FloatObjectTagStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleEditViewModel> MapToViewModelAsync(FloatObjectTagStyleUpdateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.floatObjectTagStylesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectTagStyleEditViewModel(userContext);
                    this.mapper.Map(model, viewModel);

                    viewModel.CreatedBy = style.CreatedBy;
                    viewModel.CreatedOn = style.CreatedOn;
                    viewModel.ModifiedBy = style.ModifiedBy;
                    viewModel.ModifiedOn = style.ModifiedOn;

                    return viewModel;
                }
            }

            return new FloatObjectTagStyleEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleDeleteViewModel> MapToViewModelAsync(FloatObjectTagStyleDeleteRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.floatObjectTagStylesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectTagStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new FloatObjectTagStyleDeleteViewModel(userContext);
        }
    }
}
