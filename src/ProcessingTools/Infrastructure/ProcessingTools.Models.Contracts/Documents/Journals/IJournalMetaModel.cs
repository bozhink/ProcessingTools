// <copyright file="IJournalMetaModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents.Journals
{
    /// <summary>
    /// Journal meta model.
    /// </summary>
    public interface IJournalMetaModel
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
    }
}
