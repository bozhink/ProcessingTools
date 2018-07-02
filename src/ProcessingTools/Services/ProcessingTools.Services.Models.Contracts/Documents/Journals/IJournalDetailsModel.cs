// <copyright file="IJournalDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Contracts.Documents.Journals
{
    /// <summary>
    /// Journal details model.
    /// </summary>
    public interface IJournalDetailsModel : IJournalModel, ProcessingTools.Models.Contracts.Documents.Journals.IJournalDetailsModel
    {
        /// <summary>
        /// Gets or sets the journal publisher.
        /// </summary>
        IJournalPublisherModel Publisher { get; set; }
    }
}
