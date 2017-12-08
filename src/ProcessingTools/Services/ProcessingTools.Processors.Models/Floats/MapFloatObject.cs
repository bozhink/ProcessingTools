// <copyright file="MapFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Processors.Models.Floats;
    using ProcessingTools.Enumerations.Nlm;

    /// <summary>
    /// Map.
    /// </summary>
    public class MapFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.Figure;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Map";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Maps?)";

        /// <inheritdoc/>
        public string InternalReferenceType => "map";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        /// <inheritdoc/>
        public string Description => "Map";
    }
}
