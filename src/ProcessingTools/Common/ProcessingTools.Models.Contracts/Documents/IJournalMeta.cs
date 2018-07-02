// <copyright file="IJournalMeta.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Models.Contracts.Documents
{
    /// <summary>
    /// Journal metadata.
    /// </summary>
    public interface IJournalMeta : IPermalinkable, IStringIdentifiable
    {
        /// <summary>
        /// Gets journal ID.
        /// </summary>
        string JournalId { get; }

        /// <summary>
        /// Gets journal title.
        /// </summary>
        string JournalTitle { get; }

        /// <summary>
        /// Gets abbreviated journal title.
        /// </summary>
        string AbbreviatedJournalTitle { get; }

        /// <summary>
        /// Gets print ISSN.
        /// </summary>
        string IssnPPub { get; }

        /// <summary>
        /// Gets electronic ISSN.
        /// </summary>
        string IssnEPub { get; }

        /// <summary>
        /// Gets publisher name.
        /// </summary>
        string PublisherName { get; }

        /// <summary>
        /// Gets file name pattern. Pattern contains: {0} = volume, {1} = issue, {2} = id, {3} = first page.
        /// </summary>
        string FileNamePattern { get; }

        /// <summary>
        /// Gets the name pattern of the output archive file.
        /// </summary>
        string ArchiveNamePattern { get; }
    }
}
