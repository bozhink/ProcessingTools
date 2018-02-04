// <copyright file="SupplementaryMaterialFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Models.Contracts.Processors.Floats;
    using ProcessingTools.Enumerations.Nlm;

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
    }
}
