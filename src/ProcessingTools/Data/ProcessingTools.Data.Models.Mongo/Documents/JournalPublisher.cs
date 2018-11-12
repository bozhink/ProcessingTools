// <copyright file="JournalPublisher.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Models.Documents.Mongo
{
    using ProcessingTools.Data.Models.Contracts.Documents.Journals;

    /// <summary>
    /// Journal publisher
    /// </summary>
    public class JournalPublisher : IJournalPublisherDataModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }
    }
}
