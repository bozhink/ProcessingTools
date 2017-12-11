// <copyright file="IJournal.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Models.Documents
{
    using System;
    using ProcessingTools.Contracts.Models;

    /// <summary>
    /// Journal.
    /// </summary>
    public interface IJournal : IAbbreviatedNameableGuidIdentifiable, ICreated, IModified
    {
        /// <summary>
        /// Gets Journal ID.
        /// </summary>
        string JournalId { get; }

        /// <summary>
        /// Gets print ISSN.
        /// </summary>
        string PrintIssn { get; }

        /// <summary>
        /// Gets electronic ISSN.
        /// </summary>
        string ElectronicIssn { get; }

        /// <summary>
        /// Gets publisher ID.
        /// </summary>
        Guid PublisherId { get; }
    }
}
