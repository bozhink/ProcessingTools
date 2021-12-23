// <copyright file="JournalStyleDetailsDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Models.Mongo.Layout.Styles.Journals
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;
    using ProcessingTools.Contracts.Models.Layout.Styles.Floats;
    using ProcessingTools.Contracts.Models.Layout.Styles.References;

    /// <summary>
    /// Journal style details data transfer object (DTO).
    /// </summary>
    public class JournalStyleDetailsDataTransferObject : IJournalDetailsStyleDataTransferObject
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public IList<string> FloatObjectParseStyleIds { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IFloatObjectDetailsParseStyleModel> FloatObjectParseStyles { get; set; }

        /// <inheritdoc/>
        public IList<string> FloatObjectTagStyleIds { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IFloatObjectDetailsTagStyleModel> FloatObjectTagStyles { get; set; }

        /// <inheritdoc/>
        public IList<string> ReferenceParseStyleIds { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IReferenceDetailsParseStyleModel> ReferenceParseStyles { get; set; }

        /// <inheritdoc/>
        public IList<string> ReferenceTagStyleIds { get; set; }

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
