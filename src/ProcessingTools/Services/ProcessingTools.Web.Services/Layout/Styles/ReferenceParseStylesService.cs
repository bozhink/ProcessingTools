// <copyright file="ReferenceParseStylesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
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
    using ProcessingTools.Web.Services.Contracts.Layout.Styles;

    /// <summary>
    /// Reference parse styles service.
    /// </summary>
    public class ReferenceParseStylesService : IReferenceParseStylesService
    {
        private readonly IReferenceParseStylesDataService referenceParseStylesDataService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceParseStylesService"/> class.
        /// </summary>
        /// <param name="referenceParseStylesDataService">Instance of <see cref="IReferenceParseStylesDataService"/>.</param>
        /// <param name="userContext">User context.</param>
        public ReferenceParseStylesService(IReferenceParseStylesDataService referenceParseStylesDataService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.referenceParseStylesDataService = referenceParseStylesDataService ?? throw new ArgumentNullException(nameof(referenceParseStylesDataService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<ReferenceParseStyleCreateRequestModel, ReferenceParseStyleCreateViewModel>();
                c.CreateMap<ReferenceParseStyleUpdateRequestModel, ReferenceParseStyleEditViewModel>();
                c.CreateMap<ReferenceParseStyleDeleteRequestModel, ReferenceParseStyleDeleteViewModel>();

                c.CreateMap<IReferenceParseStyleModel, ReferenceParseStyleDeleteViewModel>();
                c.CreateMap<IReferenceParseStyleModel, ReferenceParseStyleDetailsViewModel>();
                c.CreateMap<IReferenceParseStyleModel, ReferenceParseStyleEditViewModel>();
                c.CreateMap<IReferenceParseStyleModel, ReferenceParseStyleIndexViewModel>();
                c.CreateMap<IReferenceDetailsParseStyleModel, ReferenceParseStyleDeleteViewModel>();
                c.CreateMap<IReferenceDetailsParseStyleModel, ReferenceParseStyleDetailsViewModel>();
                c.CreateMap<IReferenceDetailsParseStyleModel, ReferenceParseStyleEditViewModel>();
                c.CreateMap<IReferenceDetailsParseStyleModel, ReferenceParseStyleIndexViewModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<bool> CreateReferenceParseStyleAsync(ReferenceParseStyleCreateRequestModel model)
        {
            if (model == null)
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
            if (model == null)
            {
                return false;
            }

            var result = await this.referenceParseStylesDataService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleCreateViewModel> GetReferenceParseStyleCreateViewModelAsync()
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            return new ReferenceParseStyleCreateViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleDeleteViewModel> GetReferenceParseStyleDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.referenceParseStylesDataService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.referenceParseStylesDataService.SelectCountAsync().ConfigureAwait(false);

            var styles = data?.Select(this.mapper.Map<IReferenceParseStyleModel, ReferenceParseStyleIndexViewModel>).ToArray() ?? new ReferenceParseStyleIndexViewModel[] { };

            return new ReferenceParseStylesIndexViewModel(userContext, count, take, skip / take, styles);
        }

        /// <inheritdoc/>
        public async Task<ReferenceParseStyleCreateViewModel> MapToViewModelAsync(ReferenceParseStyleCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

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
