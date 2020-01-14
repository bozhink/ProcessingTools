// <copyright file="JournalPublisher.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Mongo.Documents
{
    using ProcessingTools.Contracts.DataAccess.Models.Documents.Journals;

    /// <summary>
    /// Journal publisher.
    /// </summary>
    public class JournalPublisher : IJournalPublisherDataTransferObject
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }
    }
}
