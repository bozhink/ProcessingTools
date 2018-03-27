// <copyright file="IJournalDetailsDataModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Contracts.Documents.Journals
{
    /// <summary>
    /// Journal details data model.
    /// </summary>
    public interface IJournalDetailsDataModel : IJournalDataModel, ProcessingTools.Models.Contracts.Documents.Journals.IJournalDetailsModel
    {
        /// <summary>
        /// Gets the journal publisher.
        /// </summary>
        IJournalPublisherDataModel Publisher { get; }
    }
}
