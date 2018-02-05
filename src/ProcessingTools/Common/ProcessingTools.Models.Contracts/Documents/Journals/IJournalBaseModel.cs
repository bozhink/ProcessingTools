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
        /// Gets or sets the journal's abbreviated name.
        /// </summary>
        string AbbreviatedName { get; set; }

        /// <summary>
        /// Gets or sets the journal's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the journal's ID.
        /// </summary>
        string JournalId { get; set; }

        /// <summary>
        /// Gets or sets the journal's print ISSN.
        /// </summary>
        string PrintIssn { get; set; }

        /// <summary>
        /// Gets or sets the journal's electronic ISSN.
        /// </summary>
        string ElectronicIssn { get; set; }

        /// <summary>
        /// Gets or sets the ID of the publisher of the journal.
        /// </summary>
        string PublisherId { get; set; }
    }
}
