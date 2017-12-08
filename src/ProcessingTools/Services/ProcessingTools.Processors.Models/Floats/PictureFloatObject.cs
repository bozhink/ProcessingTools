// <copyright file="PictureFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Processors.Models.Floats;
    using ProcessingTools.Enumerations.Nlm;

    /// <summary>
    /// Picture
    /// </summary>
    public class PictureFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.Figure;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Picture";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Pict\.|Pictures?)";

        /// <inheritdoc/>
        public string InternalReferenceType => "picture";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        /// <inheritdoc/>
        public string Description => "Picture";
    }
}
