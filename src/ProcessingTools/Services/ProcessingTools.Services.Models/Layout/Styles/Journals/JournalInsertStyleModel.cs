// <copyright file="JournalInsertStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.Journals
{
    using System.Collections.Generic;

    /// <summary>
    /// Journal insert style model.
    /// </summary>
    public class JournalInsertStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals.IJournalInsertStyleModel
    {
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
