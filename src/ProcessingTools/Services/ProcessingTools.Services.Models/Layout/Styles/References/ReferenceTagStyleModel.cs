// <copyright file="ReferenceTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.References
{
    using System;

    /// <summary>
    /// Reference tag style model.
    /// </summary>
    public class ReferenceTagStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.References.IReferenceTagStyleModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public string Script { get; set; }

        /// <inheritdoc/>
        public string ReferenceXPath { get; set; }

        /// <inheritdoc/>
        public string TargetXPath { get; set; }

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
