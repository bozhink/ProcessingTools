// <copyright file="IJournalsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Models.Contracts.Documents.Journals;
    using ProcessingTools.Models.Contracts.Documents.Journals;

    /// <summary>
    /// Journals data access object.
    /// </summary>
    public interface IJournalsDataAccessObject : IDataAccessObject<IJournalDataModel, IJournalDetailsDataModel, IJournalInsertModel, IJournalUpdateModel>
    {
        /// <summary>
        /// Gets journal publishers.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IJournalPublisherDataModel[]> GetJournalPublishersAsync();
    }
}
