// <copyright file="IJournalsDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Documents
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Journals;
    using ProcessingTools.Contracts.Models.Documents.Journals;

    /// <summary>
    /// Journals data access object.
    /// </summary>
    public interface IJournalsDataAccessObject : IDataAccessObject<IJournalDataTransferObject, IJournalDetailsDataTransferObject, IJournalInsertModel, IJournalUpdateModel>
    {
        /// <summary>
        /// Gets journal publishers.
        /// </summary>
        /// <returns>Array of journal publishers.</returns>
        Task<IJournalPublisherDataTransferObject[]> GetJournalPublishersAsync();
    }
}
