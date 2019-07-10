// <copyright file="IJournalsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Contracts.Services.Models.Documents.Journals;

namespace ProcessingTools.Contracts.Services.Documents
{
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
