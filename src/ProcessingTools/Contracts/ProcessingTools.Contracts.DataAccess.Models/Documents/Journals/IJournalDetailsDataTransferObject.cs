// <copyright file="IJournalDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Models.Documents.Journals
{
    using ProcessingTools.Contracts.Models.Documents.Journals;

    /// <summary>
    /// Journal details data transfer object (DTO).
    /// </summary>
    public interface IJournalDetailsDataTransferObject : IJournalDataTransferObject, IJournalDetailsModel
    {
        /// <summary>
        /// Gets the journal publisher.
        /// </summary>
        IJournalPublisherDataTransferObject Publisher { get; }
    }
}
