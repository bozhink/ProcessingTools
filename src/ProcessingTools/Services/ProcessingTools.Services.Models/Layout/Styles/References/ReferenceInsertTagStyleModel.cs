// <copyright file="ReferenceInsertTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.References
{
    /// <summary>
    /// Reference insert tag style model.
    /// </summary>
    public class ReferenceInsertTagStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.References.IReferenceInsertTagStyleModel
    {
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
    }
}
