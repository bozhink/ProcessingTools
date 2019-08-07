// <copyright file="IJournalsDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Documents
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Models.Documents.Journals;

    /// <summary>
    /// Journals data service.
    /// </summary>
    public interface IJournalsDataService : IDataService<IJournalModel, IJournalDetailsModel, IJournalInsertModel, IJournalUpdateModel>
    {
        /// <summary>
        /// Gets journal publishers for select.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IList<IJournalPublisherModel>> GetJournalPublishersAsync();
    }
}
