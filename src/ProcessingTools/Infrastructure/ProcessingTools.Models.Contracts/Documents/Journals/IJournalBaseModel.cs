// <copyright file="IJournalBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Journals
{
    /// <summary>
    /// Journal base model.
    /// </summary>
    public interface IJournalBaseModel : IJournalMetaModel
    {
        /// <summary>
        /// Gets the ID of the publisher of the journal.
        /// </summary>
        string PublisherId { get; }

        /// <summary>
        /// Gets the ID of the journal style.
        /// </summary>
        string JournalStyleId { get; }
    }
}
