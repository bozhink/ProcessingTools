// <copyright file="IJournalBaseModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Journals
{
    /// <summary>
    /// Journal base model.
    /// </summary>
    public interface IJournalBaseModel
    {
        /// <summary>
        /// Gets the journal's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the journal's abbreviated name.
        /// </summary>
        string AbbreviatedName { get; }

        /// <summary>
        /// Gets the journal's ID.
        /// </summary>
        string JournalId { get; }

        /// <summary>
        /// Gets the journal's print ISSN.
        /// </summary>
        string PrintIssn { get; }

        /// <summary>
        /// Gets the journal's electronic ISSN.
        /// </summary>
        string ElectronicIssn { get; }

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
