// <copyright file="IJournalDetailsDataModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Journals
{
    /// <summary>
    /// Journal details data model.
    /// </summary>
    public interface IJournalDetailsDataModel : IJournalDataModel, ProcessingTools.Contracts.Models.Documents.Journals.IJournalDetailsModel
    {
        /// <summary>
        /// Gets the journal publisher.
        /// </summary>
        IJournalPublisherDataModel Publisher { get; }
    }
}
