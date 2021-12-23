﻿// <copyright file="SupplementaryMaterialFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Floats
{
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Models.Floats;

    /// <summary>
    /// Supplementary material.
    /// </summary>
    public class SupplementaryMaterialFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//supplementary-material[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.SupplementaryMaterial;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Supplementary material";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Suppl(?:\.\s*|\s+)materials?|Supplementary\s+materials?)";

        /// <inheritdoc/>
        public string InternalReferenceType => "supplementary-material";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeSupplementaryMaterial;

        /// <inheritdoc/>
        public string Description => "Supplementary material";

        /// <inheritdoc/>
        public string Name => this.Description;

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
