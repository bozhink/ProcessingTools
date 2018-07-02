// <copyright file="FloatObjectTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.Floats
{
    using System;
    using ProcessingTools.Enumerations.Nlm;

    /// <summary>
    /// Float object tag style model.
    /// </summary>
    public class FloatObjectTagStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats.IFloatObjectTagStyleModel
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType { get; set; }

        /// <inheritdoc/>
        public string FloatTypeNameInLabel { get; set; }

        /// <inheritdoc/>
        public string MatchCitationPattern { get; set; }

        /// <inheritdoc/>
        public string InternalReferenceType { get; set; }

        /// <inheritdoc/>
        public string ResultantReferenceType { get; set; }

        /// <inheritdoc/>
        public string FloatObjectXPath { get; set; }

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
