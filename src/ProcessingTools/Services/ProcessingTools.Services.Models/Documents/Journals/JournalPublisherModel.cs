// <copyright file="JournalPublisherModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Documents.Journals;

namespace ProcessingTools.Services.Models.Documents.Journals
{
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
