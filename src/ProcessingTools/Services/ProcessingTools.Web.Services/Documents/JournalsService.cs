// <copyright file="JournalsService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Models.Contracts.Documents.Journals;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;
    using ProcessingTools.Web.Models.Documents.Journals;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journals service.
    /// </summary>
    public class JournalsService : ProcessingTools.Web.Services.Contracts.Documents.IJournalsService
    {
        private readonly IJournalsService journalsService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsService"/> class.
        /// </summary>
        /// <param name="journalsService">Instance of <see cref="IJournalsDataService"/>.</param>
        /// <param name="userContext">User context.</param>
        public JournalsService(IJournalsService journalsService, IUserContext userContext)
        {
            if (userContext == null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.journalsService = journalsService ?? throw new ArgumentNullException(nameof(journalsService));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<JournalCreateRequestModel, JournalCreateViewModel>();
                c.CreateMap<JournalUpdateRequestModel, JournalEditViewModel>();
                c.CreateMap<JournalDeleteRequestModel, JournalDeleteViewModel>();

                c.CreateMap<IJournalPublisherModel, JournalPublisherViewModel>();
                c.CreateMap<IIdentifiedStyleModel, JournalStyleViewModel>();

                c.CreateMap<IJournalModel, JournalDeleteViewModel>();
                c.CreateMap<IJournalModel, JournalDetailsViewModel>();
                c.CreateMap<IJournalModel, JournalEditViewModel>();
                c.CreateMap<IJournalModel, JournalIndexViewModel>();
                c.CreateMap<IJournalDetailsModel, JournalDeleteViewModel>();
                c.CreateMap<IJournalDetailsModel, JournalDetailsViewModel>();
                c.CreateMap<IJournalDetailsModel, JournalEditViewModel>();
                c.CreateMap<IJournalDetailsModel, JournalIndexViewModel>()
                    .ForMember(vm => vm.Publisher, o => o.MapFrom(m => m.Publisher));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<bool> CreateJournalAsync(JournalCreateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.journalsService.InsertAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteJournalAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var result = await this.journalsService.DeleteAsync(id).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateJournalAsync(JournalUpdateRequestModel model)
        {
            if (model == null)
            {
                return false;
            }

            var result = await this.journalsService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<JournalCreateViewModel> GetJournalCreateViewModelAsync()
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var publishers = await this.GetJournalPublishersViewModelsAsync().ConfigureAwait(false);
            var journalStyles = await this.GetJournalStyleViewModelsAsync().ConfigureAwait(false);

            return new JournalCreateViewModel(userContext, publishers, journalStyles);
        }

        /// <inheritdoc/>
        public async Task<JournalDeleteViewModel> GetJournalDeleteViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var journal = await this.journalsService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.mapper.Map<IJournalPublisherModel, JournalPublisherViewModel>(journal.Publisher);
                    var journalStyle = await this.GetJournalStyleViewModelAsync(journal.JournalStyleId).ConfigureAwait(false);

                    var viewModel = new JournalDeleteViewModel(userContext, publisher, journalStyle);
                    this.mapper.Map(journal, viewModel);

                    return viewModel;
                }
            }

            return new JournalDeleteViewModel(userContext, new JournalPublisherViewModel(), new JournalStyleViewModel());
        }

        /// <inheritdoc/>
        public async Task<JournalDetailsViewModel> GetJournalDetailsViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var journal = await this.journalsService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.mapper.Map<IJournalPublisherModel, JournalPublisherViewModel>(journal.Publisher);
                    var journalStyle = await this.GetJournalStyleViewModelAsync(journal.JournalStyleId).ConfigureAwait(false);

                    var viewModel = new JournalDetailsViewModel(userContext, publisher, journalStyle);
                    this.mapper.Map(journal, viewModel);

                    return viewModel;
                }
            }

            return new JournalDetailsViewModel(userContext, new JournalPublisherViewModel(), new JournalStyleViewModel());
        }

        /// <inheritdoc/>
        public async Task<JournalEditViewModel> GetJournalEditViewModelAsync(string id)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var journal = await this.journalsService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publishers = await this.GetJournalPublishersViewModelsAsync().ConfigureAwait(false);
                    var journalStyles = await this.GetJournalStyleViewModelsAsync().ConfigureAwait(false);

                    var viewModel = new JournalEditViewModel(userContext, publishers, journalStyles);
                    this.mapper.Map(journal, viewModel);

                    return viewModel;
                }
            }

            return new JournalEditViewModel(userContext, Array.Empty<JournalPublisherViewModel>(), Array.Empty<JournalStyleViewModel>());
        }

        /// <inheritdoc/>
        public async Task<JournalsIndexViewModel> GetJournalsIndexViewModelAsync(int skip, int take)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var data = await this.journalsService.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            var count = await this.journalsService.SelectCountAsync().ConfigureAwait(false);

            var journals = data?.Select(this.mapper.Map<IJournalDetailsModel, JournalIndexViewModel>).ToArray() ?? Array.Empty<JournalIndexViewModel>();

            return new JournalsIndexViewModel(userContext, count, take, skip / take, journals);
        }

        /// <inheritdoc/>
        public async Task<JournalCreateViewModel> MapToViewModelAsync(JournalCreateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            var publishers = await this.GetJournalPublishersViewModelsAsync().ConfigureAwait(false);
            var journalStyles = await this.GetJournalStyleViewModelsAsync().ConfigureAwait(false);

            if (model != null)
            {
                var viewModel = new JournalCreateViewModel(userContext, publishers, journalStyles);
                this.mapper.Map(model, viewModel);

                return viewModel;
            }

            return new JournalCreateViewModel(userContext, publishers, journalStyles);
        }

        /// <inheritdoc/>
        public async Task<JournalEditViewModel> MapToViewModelAsync(JournalUpdateRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var journal = await this.journalsService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publishers = await this.GetJournalPublishersViewModelsAsync().ConfigureAwait(false);
                    var journalStyles = await this.GetJournalStyleViewModelsAsync().ConfigureAwait(false);

                    var viewModel = new JournalEditViewModel(userContext, publishers, journalStyles);
                    this.mapper.Map(model, viewModel);

                    viewModel.CreatedBy = journal.CreatedBy;
                    viewModel.CreatedOn = journal.CreatedOn;
                    viewModel.ModifiedBy = journal.ModifiedBy;
                    viewModel.ModifiedOn = journal.ModifiedOn;

                    return viewModel;
                }
            }

            return new JournalEditViewModel(userContext, Array.Empty<JournalPublisherViewModel>(), Array.Empty<JournalStyleViewModel>());
        }

        /// <inheritdoc/>
        public async Task<JournalDeleteViewModel> MapToViewModelAsync(JournalDeleteRequestModel model)
        {
            var userContext = await this.userContextFactory.Invoke().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var journal = await this.journalsService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.mapper.Map<IJournalPublisherModel, JournalPublisherViewModel>(journal.Publisher);
                    var journalStyle = await this.GetJournalStyleViewModelAsync(journal.JournalStyleId).ConfigureAwait(false);

                    var viewModel = new JournalDeleteViewModel(userContext, publisher, journalStyle);
                    this.mapper.Map(journal, viewModel);

                    return viewModel;
                }
            }

            return new JournalDeleteViewModel(userContext, new JournalPublisherViewModel(), new JournalStyleViewModel());
        }

        private async Task<JournalPublisherViewModel[]> GetJournalPublishersViewModelsAsync()
        {
            var publishers = await this.journalsService.GetJournalPublishersForSelectAsync().ConfigureAwait(false);
            return publishers?.Select(this.mapper.Map<IJournalPublisherModel, JournalPublisherViewModel>).ToArray() ?? Array.Empty<JournalPublisherViewModel>();
        }

        private async Task<JournalStyleViewModel> GetJournalStyleViewModelAsync(string id)
        {
            var style = await this.journalsService.GetJournalStyleByIdAsync(id).ConfigureAwait(false);
            if (style == null)
            {
                return new JournalStyleViewModel();
            }

            return this.mapper.Map<IIdentifiedStyleModel, JournalStyleViewModel>(style);
        }

        private async Task<JournalStyleViewModel[]> GetJournalStyleViewModelsAsync()
        {
            var styles = await this.journalsService.GetJournalStylesForSelectAsync().ConfigureAwait(false);

            return styles?.Select(this.mapper.Map<IIdentifiedStyleModel, JournalStyleViewModel>).ToArray() ?? Array.Empty<JournalStyleViewModel>();
        }
    }
}
