// <copyright file="JournalsService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Contracts.Documents.Journals;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;

    /// <summary>
    /// Journals service.
    /// </summary>
    public class JournalsService : IJournalsService
    {
        private readonly IJournalsDataService journalsDataService;
        private readonly IJournalStylesDataService journalStylesDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalsService"/> class.
        /// </summary>
        /// <param name="journalsDataService">Journals data service.</param>
        /// <param name="journalStylesDataService">Journal styles data service.</param>
        public JournalsService(IJournalsDataService journalsDataService, IJournalStylesDataService journalStylesDataService)
        {
            this.journalsDataService = journalsDataService ?? throw new ArgumentNullException(nameof(journalsDataService));
            this.journalStylesDataService = journalStylesDataService ?? throw new ArgumentNullException(nameof(journalStylesDataService));
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(IJournalInsertModel model) => this.journalsDataService.InsertAsync(model);

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IJournalUpdateModel model) => this.journalsDataService.UpdateAsync(model);

        /// <inheritdoc/>
        public Task<object> DeleteAsync(object id) => this.journalsDataService.DeleteAsync(id);

        /// <inheritdoc/>
        public Task<IJournalModel> GetByIdAsync(object id) => this.journalsDataService.GetByIdAsync(id);

        /// <inheritdoc/>
        public Task<IJournalDetailsModel> GetDetailsByIdAsync(object id) => this.journalsDataService.GetDetailsByIdAsync(id);

        /// <inheritdoc/>
        public Task<IJournalModel[]> SelectAsync(int skip, int take) => this.journalsDataService.SelectAsync(skip, take);

        /// <inheritdoc/>
        public Task<IJournalDetailsModel[]> SelectDetailsAsync(int skip, int take) => this.journalsDataService.SelectDetailsAsync(skip, take);

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.journalsDataService.SelectCountAsync();

        /// <inheritdoc/>
        public Task<IJournalPublisherModel[]> GetJournalPublishersForSelectAsync() => this.journalsDataService.GetJournalPublishersAsync();

        /// <inheritdoc/>
        public Task<IIdentifiedStyleModel> GetJournalStyleByIdAsync(object id) => this.journalStylesDataService.GetStyleByIdAsync(id);

        /// <inheritdoc/>
        public Task<IIdentifiedStyleModel[]> GetJournalStylesForSelectAsync() => this.journalStylesDataService.GetStylesForSelectAsync();
    }
}
