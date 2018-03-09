// <copyright file="JournalMeta.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Meta
{
    using System.Text.RegularExpressions;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Journal meta.
    /// </summary>
    public class JournalMeta : IJournalMeta
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedJournalTitle { get; set; }

        /// <inheritdoc/>
        public string ArchiveNamePattern { get; set; }

        /// <summary>
        /// Gets or sets the file name pattern. Pattern is: {0} = volume, {1} = issue, {2} = id, {3} = first page.
        /// </summary>
        public string FileNamePattern { get; set; }

        /// <inheritdoc/>
        public string IssnEPub { get; set; }

        /// <inheritdoc/>
        public string IssnPPub { get; set; }

        /// <inheritdoc/>
        public string JournalId { get; set; }

        /// <inheritdoc/>
        public string JournalTitle { get; set; }

        /// <inheritdoc/>
        public string Permalink => Regex.Replace(this.AbbreviatedJournalTitle, @"\W+", "_").ToLowerInvariant();

        /// <inheritdoc/>
        public string PublisherName { get; set; }
    }
}
