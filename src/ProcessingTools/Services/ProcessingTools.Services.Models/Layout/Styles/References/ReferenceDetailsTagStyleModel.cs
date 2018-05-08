// <copyright file="ReferenceDetailsTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.References
{
    using System;

    /// <summary>
    /// Reference details tag style model.
    /// </summary>
    public class ReferenceDetailsTagStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.References.IReferenceDetailsTagStyleModel
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
