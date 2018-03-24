// <copyright file="JournalPublisherModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Documents.Journals
{
    using ProcessingTools.Services.Models.Contracts.Documents.Journals;

    /// <summary>
    /// Journal publisher model.
    /// </summary>
    public class JournalPublisherModel : IJournalPublisherModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string AbbreviatedName { get; set; }
    }
}
