// <copyright file="JournalDetailsStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.Journals
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Journal details style model.
    /// </summary>
    public class JournalDetailsStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.Journals.IJournalDetailsStyleModel
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

        /// <inheritdoc/>
        public IEnumerable<IFloatObjectDetailsParseStyleModel> FloatObjectParseStyles { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IFloatObjectDetailsTagStyleModel> FloatObjectTagStyles { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IReferenceDetailsParseStyleModel> ReferenceParseStyles { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IReferenceDetailsTagStyleModel> ReferenceTagStyles { get; set; }

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
