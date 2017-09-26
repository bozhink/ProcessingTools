// <copyright file="IJournal.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents
{
    using System;
    using ProcessingTools.Models.Contracts;

    /// <summary>
    /// Journal.
    /// </summary>
    public interface IJournal : IAbbreviatedNameableGuidIdentifiable, IModelWithUserInformation
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
