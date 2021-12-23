﻿// <copyright file="JournalsWebService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Documents
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Documents;
    using ProcessingTools.Contracts.Web.Services.Documents;
    using ProcessingTools.Web.Models.Documents.Journals;
    using ProcessingTools.Web.Models.Shared;

    /// <summary>
    /// Journals web service.
    /// </summary>
    public class JournalsWebService : IJournalsWebService
    {
        private readonly IJournalsService journalsService;
        private readonly Func<Task<UserContext>> userContextFactory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsWebService"/> class.
        /// </summary>
        /// <param name="journalsService">Instance of <see cref="IJournalsDataService"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        /// <param name="userContext">User context.</param>
        public JournalsWebService(IJournalsService journalsService, IMapper mapper, IUserContext userContext)
        {
            if (userContext is null)
            {
                throw new ArgumentNullException(nameof(userContext));
            }

            this.journalsService = journalsService ?? throw new ArgumentNullException(nameof(journalsService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            this.userContextFactory = () => Task.FromResult(new UserContext(userId: userContext.UserId, userName: userContext.UserName, userEmail: userContext.UserEmail));
        }

        /// <inheritdoc/>
        public Task<UserContext> GetUserContextAsync() => this.userContextFactory.Invoke();

        /// <inheritdoc/>
        public async Task<bool> CreateJournalAsync(JournalCreateRequestModel model)
        {
            if (model is null)
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
            if (model is null)
            {
                return false;
            }

            var result = await this.journalsService.UpdateAsync(model).ConfigureAwait(false);
            return result != null;
        }

        /// <inheritdoc/>
        public async Task<JournalCreateViewModel> GetJournalCreateViewModelAsync()
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            var publishers = await this.GetJournalPublishersViewModelsAsync().ConfigureAwait(false);
            var journalStyles = await this.GetJournalStyleViewModelsAsync().ConfigureAwait(false);

            return new JournalCreateViewModel(userContext, publishers, journalStyles);
        }

        /// <inheritdoc/>
        public async Task<JournalDeleteViewModel> GetJournalDeleteViewModelAsync(string id)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var journal = await this.journalsService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.mapper.Map<JournalPublisherViewModel>(journal.Publisher);
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
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var journal = await this.journalsService.GetDetailsByIdAsync(id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.mapper.Map<JournalPublisherViewModel>(journal.Publisher);
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
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

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
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            var data = await this.journalsService.SelectDetailsAsync(skip, take).ConfigureAwait(false);
            var count = await this.journalsService.SelectCountAsync().ConfigureAwait(false);

            var journals = data?.Select(this.mapper.Map<JournalIndexViewModel>).ToArray() ?? Array.Empty<JournalIndexViewModel>();

            return new JournalsIndexViewModel(userContext, count, take, skip / take, journals);
        }

        /// <inheritdoc/>
        public async Task<JournalCreateViewModel> MapToViewModelAsync(JournalCreateRequestModel model)
        {
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

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
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

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
            var userContext = await this.GetUserContextAsync().ConfigureAwait(false);

            if (model != null && !string.IsNullOrWhiteSpace(model.Id))
            {
                var journal = await this.journalsService.GetDetailsByIdAsync(model.Id).ConfigureAwait(false);
                if (journal != null)
                {
                    var publisher = this.mapper.Map<JournalPublisherViewModel>(journal.Publisher);
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
            return publishers?.Select(this.mapper.Map<JournalPublisherViewModel>).ToArray() ?? Array.Empty<JournalPublisherViewModel>();
        }

        private async Task<JournalStyleViewModel> GetJournalStyleViewModelAsync(string id)
        {
            var style = await this.journalsService.GetJournalStyleByIdAsync(id).ConfigureAwait(false);
            if (style is null)
            {
                return new JournalStyleViewModel();
            }

            return this.mapper.Map<JournalStyleViewModel>(style);
        }

        private async Task<JournalStyleViewModel[]> GetJournalStyleViewModelsAsync()
        {
            var styles = await this.journalsService.GetJournalStylesForSelectAsync().ConfigureAwait(false);

            return styles?.Select(this.mapper.Map<JournalStyleViewModel>).ToArray() ?? Array.Empty<JournalStyleViewModel>();
        }
    }
}
