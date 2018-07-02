// <copyright file="FloatObjectTagStylesService.cs" company="ProcessingTools">
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
    /// Float object tag styles service.
    /// </summary>
    public class FloatObjectTagStylesService : ProcessingTools.Web.Services.Contracts.Layout.Styles.IFloatObjectTagStylesService
    {
        private readonly IFloatObjectTagStylesDataService floatObjectTagStylesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloatObjectTagStylesService"/> class.
        /// </summary>
        /// <param name="floatObjectTagStylesDataService">Instance of <see cref="IFloatObjectTagStylesDataService"/>.</param>
        /// <param name="userContext">User context.</param>
        public FloatObjectTagStylesService(IFloatObjectTagStylesDataService floatObjectTagStylesDataService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.floatObjectTagStylesDataService = floatObjectTagStylesDataService ?? throw new ArgumentNullException(nameof(floatObjectTagStylesDataService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<FloatObjectTagStyleCreateRequestModel, FloatObjectTagStyleCreateViewModel>();
                c.CreateMap<FloatObjectTagStyleUpdateRequestModel, FloatObjectTagStyleEditViewModel>();
                c.CreateMap<FloatObjectTagStyleDeleteRequestModel, FloatObjectTagStyleDeleteViewModel>();

                c.CreateMap<IFloatObjectTagStyleModel, FloatObjectTagStyleDeleteViewModel>();
                c.CreateMap<IFloatObjectTagStyleModel, FloatObjectTagStyleDetailsViewModel>();
                c.CreateMap<IFloatObjectTagStyleModel, FloatObjectTagStyleEditViewModel>();
                c.CreateMap<IFloatObjectTagStyleModel, FloatObjectTagStyleIndexViewModel>();
                c.CreateMap<IFloatObjectDetailsTagStyleModel, FloatObjectTagStyleDeleteViewModel>();
                c.CreateMap<IFloatObjectDetailsTagStyleModel, FloatObjectTagStyleDetailsViewModel>();
                c.CreateMap<IFloatObjectDetailsTagStyleModel, FloatObjectTagStyleEditViewModel>();
                c.CreateMap<IFloatObjectDetailsTagStyleModel, FloatObjectTagStyleIndexViewModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            return new FloatObjectTagStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleDeleteViewModel> GetFloatObjectTagStyleDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.floatObjectTagStylesDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.floatObjectTagStylesDataService.SelectCountAsync().ConfigureAwait(false);

            var styles = data?.Select(this.mapper.Map<IFloatObjectTagStyleModel, FloatObjectTagStyleIndexViewModel>).ToArray() ?? new FloatObjectTagStyleIndexViewModel[] { };

            return new FloatObjectTagStylesIndexViewModel(userContext, count, take, skip / take, styles);
        }

        /// <inheritdoc/>
        public async Task<FloatObjectTagStyleCreateViewModel> MapToViewModelAsync(FloatObjectTagStyleCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
