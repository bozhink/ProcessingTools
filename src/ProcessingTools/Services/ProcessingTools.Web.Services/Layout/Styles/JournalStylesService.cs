// <copyright file="JournalStylesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Layout.Styles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Layout.Styles.Floats;
    using ProcessingTools.Models.Contracts.Layout.Styles.References;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals;
    using ProcessingTools.Web.Models.Layout.Styles.Journals;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journal styles service.
    /// </summary>
    public class JournalStylesService : ProcessingTools.Web.Services.Contracts.Layout.Styles.IJournalStylesService
    {
        private readonly IJournalStylesService journalStylesService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalStylesService"/> class.
        /// </summary>
        /// <param name="journalStylesService">Instance of <see cref="IJournalStylesService"/>.</param>
        /// <param name="userContext">User context.</param>
        public JournalStylesService(IJournalStylesService journalStylesService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.journalStylesService = journalStylesService ?? throw new ArgumentNullException(nameof(journalStylesService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            MapperConfiguration mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<JournalStyleCreateRequestModel, JournalStyleCreateViewModel>();
                c.CreateMap<JournalStyleUpdateRequestModel, JournalStyleEditViewModel>();
                c.CreateMap<JournalStyleDeleteRequestModel, JournalStyleDeleteViewModel>();

                c.CreateMap<IIdentifiedStyleModel, StyleSelectViewModel>();
                c.CreateMap<IFloatObjectDetailsParseStyleModel, StyleSelectViewModel>();
                c.CreateMap<IFloatObjectDetailsTagStyleModel, StyleSelectViewModel>();
                c.CreateMap<IReferenceDetailsParseStyleModel, StyleSelectViewModel>();
                c.CreateMap<IReferenceDetailsTagStyleModel, StyleSelectViewModel>();
                c.CreateMap<IList<IFloatObjectDetailsParseStyleModel>, IList<StyleSelectViewModel>>();
                c.CreateMap<IList<IFloatObjectDetailsTagStyleModel>, IList<StyleSelectViewModel>>();
                c.CreateMap<IList<IReferenceDetailsParseStyleModel>, IList<StyleSelectViewModel>>();
                c.CreateMap<IList<IReferenceDetailsTagStyleModel>, IList<StyleSelectViewModel>>();

                c.CreateMap<IJournalStyleModel, JournalStyleDeleteViewModel>();
                c.CreateMap<IJournalStyleModel, JournalStyleDetailsViewModel>();
                c.CreateMap<IJournalStyleModel, JournalStyleEditViewModel>();
                c.CreateMap<IJournalStyleModel, JournalStyleIndexViewModel>();
                c.CreateMap<IJournalDetailsStyleModel, JournalStyleDeleteViewModel>()
                    .ForMember(vm => vm.FloatObjectParseStyles, o => o.MapFrom(sm => sm.FloatObjectParseStyles))
                    .ForMember(vm => vm.FloatObjectTagStyles, o => o.MapFrom(sm => sm.FloatObjectTagStyles))
                    .ForMember(vm => vm.ReferenceParseStyles, o => o.MapFrom(sm => sm.ReferenceParseStyles))
                    .ForMember(vm => vm.ReferenceTagStyles, o => o.MapFrom(sm => sm.ReferenceTagStyles));
                c.CreateMap<IJournalDetailsStyleModel, JournalStyleDetailsViewModel>()
                    .ForMember(vm => vm.FloatObjectParseStyles, o => o.MapFrom(sm => sm.FloatObjectParseStyles))
                    .ForMember(vm => vm.FloatObjectTagStyles, o => o.MapFrom(sm => sm.FloatObjectTagStyles))
                    .ForMember(vm => vm.ReferenceParseStyles, o => o.MapFrom(sm => sm.ReferenceParseStyles))
                    .ForMember(vm => vm.ReferenceTagStyles, o => o.MapFrom(sm => sm.ReferenceTagStyles));
                c.CreateMap<IJournalDetailsStyleModel, JournalStyleEditViewModel>();
                c.CreateMap<IJournalDetailsStyleModel, JournalStyleIndexViewModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<bool> CreateJournalStyleAsync(JournalStyleCreateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.journalStylesService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteJournalStyleAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.journalStylesService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateJournalStyleAsync(JournalStyleUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.journalStylesService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<JournalStyleCreateViewModel> GetJournalStyleCreateViewModelAsync()
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var floatObjectParseStyles = await this.journalStylesService.GetFloatObjectParseStylesForSelectAsync().ConfigureAwait(false);
            var floatObjectTagStyles = await this.journalStylesService.GetFloatObjectTagStylesForSelectAsync().ConfigureAwait(false);
            var referenceParseStyles = await this.journalStylesService.GetReferenceParseStylesForSelectAsync().ConfigureAwait(false);
            var referenceTagStyles = await this.journalStylesService.GetReferenceTagStylesForSelectAsync().ConfigureAwait(false);

            var viewModel = new JournalStyleCreateViewModel(userContext)
            {
                FloatObjectParseStyles = floatObjectParseStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                FloatObjectTagStyles = floatObjectTagStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                ReferenceParseStyles = referenceParseStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                ReferenceTagStyles = referenceTagStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList()
            };

            return viewModel;
        }

        /// <inheritdoc/>
        public async Task<JournalStyleDeleteViewModel> GetJournalStyleDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.journalStylesService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new JournalStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new JournalStyleDeleteViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<JournalStyleDetailsViewModel> GetJournalStyleDetailsViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.journalStylesService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new JournalStyleDetailsViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new JournalStyleDetailsViewModel(userContext);
        }

        /// <inheritdoc/>
        public async Task<JournalStyleEditViewModel> GetJournalStyleEditViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var floatObjectParseStyles = await this.journalStylesService.GetFloatObjectParseStylesForSelectAsync().ConfigureAwait(false);
            var floatObjectTagStyles = await this.journalStylesService.GetFloatObjectTagStylesForSelectAsync().ConfigureAwait(false);
            var referenceParseStyles = await this.journalStylesService.GetReferenceParseStylesForSelectAsync().ConfigureAwait(false);
            var referenceTagStyles = await this.journalStylesService.GetReferenceTagStylesForSelectAsync().ConfigureAwait(false);

            var viewModel = new JournalStyleEditViewModel(userContext)
            {
                FloatObjectParseStyles = floatObjectParseStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                FloatObjectTagStyles = floatObjectTagStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                ReferenceParseStyles = referenceParseStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                ReferenceTagStyles = referenceTagStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList()
            };

            if (!string.IsNullOrWhiteSpace(id))
            {
                var style = await this.journalStylesService.GetByIdAsync(id).ConfigureAwait(false);
                if (style != null)
                {
                    this.mapper.Map(style, viewModel);
                }
            }

            return viewModel;
        }

        /// <inheritdoc/>
        public async Task<JournalStylesIndexViewModel> GetJournalStylesIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.journalStylesService.SelectAsync(skip, take).ConfigureAwait(false);
            var count = await this.journalStylesService.SelectCountAsync().ConfigureAwait(false);

            var styles = data?.Select(this.mapper.Map<IJournalStyleModel, JournalStyleIndexViewModel>).ToArray() ?? new JournalStyleIndexViewModel[] { };

            return new JournalStylesIndexViewModel(userContext, count, take, skip / take, styles);
        }

        /// <inheritdoc/>
        public async Task<JournalStyleCreateViewModel> MapToViewModelAsync(JournalStyleCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var floatObjectParseStyles = await this.journalStylesService.GetFloatObjectParseStylesForSelectAsync().ConfigureAwait(false);
            var floatObjectTagStyles = await this.journalStylesService.GetFloatObjectTagStylesForSelectAsync().ConfigureAwait(false);
            var referenceParseStyles = await this.journalStylesService.GetReferenceParseStylesForSelectAsync().ConfigureAwait(false);
            var referenceTagStyles = await this.journalStylesService.GetReferenceTagStylesForSelectAsync().ConfigureAwait(false);

            var viewModel = new JournalStyleCreateViewModel(userContext)
            {
                FloatObjectParseStyles = floatObjectParseStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                FloatObjectTagStyles = floatObjectTagStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                ReferenceParseStyles = referenceParseStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                ReferenceTagStyles = referenceTagStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList()
            };

            if (model != null)
            {
                this.mapper.Map(model, viewModel);
            }

            return viewModel;
        }

        /// <inheritdoc/>
        public async Task<JournalStyleEditViewModel> MapToViewModelAsync(JournalStyleUpdateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var floatObjectParseStyles = await this.journalStylesService.GetFloatObjectParseStylesForSelectAsync().ConfigureAwait(false);
            var floatObjectTagStyles = await this.journalStylesService.GetFloatObjectTagStylesForSelectAsync().ConfigureAwait(false);
            var referenceParseStyles = await this.journalStylesService.GetReferenceParseStylesForSelectAsync().ConfigureAwait(false);
            var referenceTagStyles = await this.journalStylesService.GetReferenceTagStylesForSelectAsync().ConfigureAwait(false);

            var viewModel = new JournalStyleEditViewModel(userContext)
            {
                FloatObjectParseStyles = floatObjectParseStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                FloatObjectTagStyles = floatObjectTagStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                ReferenceParseStyles = referenceParseStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList(),
                ReferenceTagStyles = referenceTagStyles.Select(this.mapper.Map<IIdentifiedStyleModel, StyleSelectViewModel>).ToList()
            };

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.journalStylesService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    this.mapper.Map(model, viewModel);

                    viewModel.CreatedBy = style.CreatedBy;
                    viewModel.CreatedOn = style.CreatedOn;
                    viewModel.ModifiedBy = style.ModifiedBy;
                    viewModel.ModifiedOn = style.ModifiedOn;
                }
            }

            return viewModel;
        }

        /// <inheritdoc/>
        public async Task<JournalStyleDeleteViewModel> MapToViewModelAsync(JournalStyleDeleteRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var style = await this.journalStylesService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (style != null)
                {
                    var viewModel = new JournalStyleDeleteViewModel(userContext);
                    this.mapper.Map(style, viewModel);

                    return viewModel;
                }
            }

            return new JournalStyleDeleteViewModel(userContext);
        }
    }
}
