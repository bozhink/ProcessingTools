﻿// <copyright file="FloatObjectInsertTagStyleModel.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Layout.Styles.Floats
{
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Services.Models.Layout.Styles.Floats;

    /// <summary>
    /// Float object insert tag style model.
    /// </summary>
    public class FloatObjectInsertTagStyleModel : IFloatObjectInsertTagStyleModel
    {
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
