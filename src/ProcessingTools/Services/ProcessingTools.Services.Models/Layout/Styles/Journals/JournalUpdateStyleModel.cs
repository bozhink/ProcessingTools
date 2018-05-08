// <copyright file="JournalUpdateStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.Journals
{
    using System.Collections.Generic;

    /// <summary>
    /// Journal update style model.
    /// </summary>
    public class JournalUpdateStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals.IJournalUpdateStyleModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public IList<string> FloatObjectParseStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> FloatObjectTagStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> ReferenceParseStyleIds { get; set; } = new List<string>();

        /// <inheritdoc/>
        public IList<string> ReferenceTagStyleIds { get; set; } = new List<string>();
    }
}
