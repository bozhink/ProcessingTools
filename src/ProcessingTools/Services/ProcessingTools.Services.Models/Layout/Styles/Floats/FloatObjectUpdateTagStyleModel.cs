// <copyright file="FloatObjectUpdateTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.Floats
{
    using ProcessingTools.Common.Enumerations.Nlm;

    /// <summary>
    /// Float object update tag style model.
    /// </summary>
    public class FloatObjectUpdateTagStyleModel : ProcessingTools.Services.Models.Contracts.Layout.Styles.Floats.IFloatObjectUpdateTagStyleModel
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
    }
}
