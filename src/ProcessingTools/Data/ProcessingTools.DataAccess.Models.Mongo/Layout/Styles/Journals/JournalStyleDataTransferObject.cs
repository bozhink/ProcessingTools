// <copyright file="JournalStyleDataTransferObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DataAccess.Models.Mongo.Layout.Styles.Journals
{
    using System;
    using System.Collections.Generic;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles.Journals;

    /// <summary>
    /// Journal style data transfer object (DTO).
    /// </summary>
    public class JournalStyleDataTransferObject : IJournalStyleDataTransferObject
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public Guid ObjectId { get; set; }

        /// <inheritdoc/>
        public IList<string> FloatObjectParseStyleIds { get; set; }

        /// <inheritdoc/>
        public IList<string> FloatObjectTagStyleIds { get; set; }

        /// <inheritdoc/>
        public IList<string> ReferenceParseStyleIds { get; set; }

        /// <inheritdoc/>
        public IList<string> ReferenceTagStyleIds { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

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
