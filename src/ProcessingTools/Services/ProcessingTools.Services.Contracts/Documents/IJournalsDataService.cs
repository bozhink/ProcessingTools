// <copyright file="IJournalsDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Documents.Journals;

    /// <summary>
    /// Journals data service.
    /// </summary>
    public interface IJournalsDataService : IDataService<IJournalModel, IJournalDetailsModel, IJournalInsertModel, IJournalUpdateModel>
    {
        /// <summary>
        /// Gets journal publishers for select.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IJournalPublisherModel[]> GetJournalPublishersAsync();
    }
}
