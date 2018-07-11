// <copyright file="ReferenceTagStylesService.cs" company="ProcessingTools">
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
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Web.Models.Layout.Styles.References;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Reference tag styles service.
    /// </summary>
    public class ReferenceTagStylesService : ProcessingTools.Web.Services.Contracts.Layout.Styles.IReferenceTagStylesService
    {
        private readonly IReferenceTagStylesDataService referenceTagStylesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTagStylesService"/> class.
        /// </summary>
        /// <param name="referenceTagStylesDataService">Instance of <see cref="IReferenceTagStylesDataService"/>.</param>
        /// <param name="userContext">User context.</param>
        public ReferenceTagStylesService(IReferenceTagStylesDataService referenceTagStylesDataService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.referenceTagStylesDataService = referenceTagStylesDataService ?? throw new ArgumentNullException(nameof(referenceTagStylesDataService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ReferenceTagStyleCreateRequestModel, ReferenceTagStyleCreateViewModel>();
                c.CreateMap<ReferenceTagStyleUpdateRequestModel, ReferenceTagStyleEditViewModel>();
                c.CreateMap<ReferenceTagStyleDeleteRequestModel, ReferenceTagStyleDeleteViewModel>();

                c.CreateMap<IReferenceTagStyleModel, ReferenceTagStyleDeleteViewModel>();
                c.CreateMap<IReferenceTagStyleModel, ReferenceTagStyleDetailsViewModel>();
                c.CreateMap<IReferenceTagStyleModel, ReferenceTagStyleEditViewModel>();
                c.CreateMap<IReferenceTagStyleModel, ReferenceTagStyleIndexViewModel>();
                c.CreateMap<IReferenceDetailsTagStyleModel, ReferenceTagStyleDeleteViewModel>();
                c.CreateMap<IReferenceDetailsTagStyleModel, ReferenceTagStyleDetailsViewModel>();
                c.CreateMap<IReferenceDetailsTagStyleModel, ReferenceTagStyleEditViewModel>();
                c.CreateMap<IReferenceDetailsTagStyleModel, ReferenceTagStyleIndexViewModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            return new ReferenceTagStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleDeleteViewModel> GetReferenceTagStyleDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.referenceTagStylesDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.referenceTagStylesDataService.SelectCountAsync().ConfigureAwait(false);

            var styles = data?.Select(this.mapper.Map<IReferenceTagStyleModel, ReferenceTagStyleIndexViewModel>).ToArray() ?? Array.Empty<ReferenceTagStyleIndexViewModel>();

            return new ReferenceTagStylesIndexViewModel(userContext, count, take, skip / take, styles);
        }

        /// <inheritdoc/>
        public async Task<ReferenceTagStyleCreateViewModel> MapToViewModelAsync(ReferenceTagStyleCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
