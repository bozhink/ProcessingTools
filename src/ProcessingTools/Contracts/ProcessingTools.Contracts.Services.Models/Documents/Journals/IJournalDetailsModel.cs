// <copyright file="IJournalDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Models.Documents.Journals
{
    /// <summary>
    /// Journal details model.
    /// </summary>
    public interface IJournalDetailsModel : IJournalModel, ProcessingTools.Contracts.Models.Documents.Journals.IJournalDetailsModel
    {
        /// <summary>
        /// Gets or sets the journal publisher.
        /// </summary>
        IJournalPublisherModel Publisher { get; set; }
    }
}