﻿// <copyright file="JournalDetailsModel.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Journals
{
    using System;
    using ProcessingTools.Contracts.Services.Models.Documents.Journals;

    /// <summary>
    /// Journal details model.
    /// </summary>
    public class JournalDetailsModel : IJournalDetailsModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }

        /// <inheritdoc/>
        public string JournalId { get; set; }

        /// <inheritdoc/>
        public string PrintIssn { get; set; }

        /// <inheritdoc/>
        public string ElectronicIssn { get; set; }

        /// <inheritdoc/>
        public string PublisherId { get; set; }

        /// <inheritdoc/>
        public IJournalPublisherModel Publisher { get; set; }

        /// <inheritdoc/>
        public string JournalStyleId { get; set; }

        /// <inheritdoc/>
        public long NumberOfArticles { get; set; }

        /// <inheritdoc/>
        public string CreatedBy { get; set; }

        /// <inheritdoc/>
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc/>
        public string ModifiedBy { get; set; }

        /// <inheritdoc/>
        public DateTime ModifiedOn { get; set; }
    }
}
