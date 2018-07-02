// <copyright file="FloatObjectParseStylesService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Layout.Styles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Web.Models.Layout.Styles.Floats;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Float object parse styles service.
    /// </summary>
    public class FloatObjectParseStylesService : ProcessingTools.Web.Services.Contracts.Layout.Styles.IFloatObjectParseStylesService
    {
        private readonly IFloatObjectParseStylesDataService floatObjectParseStylesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectParseStylesService"/> class.
        /// </summary>
        /// <param name="floatObjectParseStylesDataService">Instance of <see cref="IFloatObjectParseStylesDataService"/>.</param>
        /// <param name="userContext">User context.</param>
        public FloatObjectParseStylesService(IFloatObjectParseStylesDataService floatObjectParseStylesDataService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.floatObjectParseStylesDataService = floatObjectParseStylesDataService ?? throw new ArgumentNullException(nameof(floatObjectParseStylesDataService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<FloatObjectParseStyleCreateRequestModel, FloatObjectParseStyleCreateViewModel>();
                c.CreateMap<FloatObjectParseStyleUpdateRequestModel, FloatObjectParseStyleEditViewModel>();
                c.CreateMap<FloatObjectParseStyleDeleteRequestModel, FloatObjectParseStyleDeleteViewModel>();

                c.CreateMap<IFloatObjectParseStyleModel, FloatObjectParseStyleDeleteViewModel>();
                c.CreateMap<IFloatObjectParseStyleModel, FloatObjectParseStyleDetailsViewModel>();
                c.CreateMap<IFloatObjectParseStyleModel, FloatObjectParseStyleEditViewModel>();
                c.CreateMap<IFloatObjectParseStyleModel, FloatObjectParseStyleIndexViewModel>();
                c.CreateMap<IFloatObjectDetailsParseStyleModel, FloatObjectParseStyleDeleteViewModel>();
                c.CreateMap<IFloatObjectDetailsParseStyleModel, FloatObjectParseStyleDetailsViewModel>();
                c.CreateMap<IFloatObjectDetailsParseStyleModel, FloatObjectParseStyleEditViewModel>();
                c.CreateMap<IFloatObjectDetailsParseStyleModel, FloatObjectParseStyleIndexViewModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<bool> CreateFloatObjectParseStyleAsync(FloatObjectParseStyleCreateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.floatObjectParseStylesDataService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteFloatObjectParseStyleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.floatObjectParseStylesDataService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateFloatObjectParseStyleAsync(FloatObjectParseStyleUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.floatObjectParseStylesDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<FloatObjectParseStyleCreateViewModel> GetFloatObjectParseStyleCreateViewModelAsync()
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            return new FloatObjectParseStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectParseStyleDeleteViewModel> GetFloatObjectParseStyleDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.floatObjectParseStylesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectParseStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new FloatObjectParseStyleDeleteViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectParseStyleDetailsViewModel> GetFloatObjectParseStyleDetailsViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.floatObjectParseStylesDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectParseStyleDetailsViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new FloatObjectParseStyleDetailsViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectParseStyleEditViewModel> GetFloatObjectParseStyleEditViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.floatObjectParseStylesDataService.GetByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectParseStyleEditViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new FloatObjectParseStyleEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectParseStylesIndexViewModel> GetFloatObjectParseStylesIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.floatObjectParseStylesDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.floatObjectParseStylesDataService.SelectCountAsync().ConfigureAwait(false);

            var styles = data?.Select(this.mapper.Map<IFloatObjectParseStyleModel, FloatObjectParseStyleIndexViewModel>).ToArray() ?? new FloatObjectParseStyleIndexViewModel[] { };

            return new FloatObjectParseStylesIndexViewModel(userContext, count, take, skip / take, styles);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectParseStyleCreateViewModel> MapToViewModelAsync(FloatObjectParseStyleCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = new FloatObjectParseStyleCreateViewModel(userContext);
                this.mapper.Map(model, viewModel);

                return viewModel;
            }

            return new FloatObjectParseStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectParseStyleEditViewModel> MapToViewModelAsync(FloatObjectParseStyleUpdateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.floatObjectParseStylesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectParseStyleEditViewModel(userContext);
                    this.mapper.Map(model, viewModel);

                    viewModel.CreatedBy = style.CreatedBy;
                    viewModel.CreatedOn = style.CreatedOn;
                    viewModel.ModifiedBy = style.ModifiedBy;
                    viewModel.ModifiedOn = style.ModifiedOn;

                    return viewModel;
                }
            }

            return new FloatObjectParseStyleEditViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectParseStyleDeleteViewModel> MapToViewModelAsync(FloatObjectParseStyleDeleteRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.floatObjectParseStylesDataService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new FloatObjectParseStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new FloatObjectParseStyleDeleteViewModel(userContext);
        }
    }
}
