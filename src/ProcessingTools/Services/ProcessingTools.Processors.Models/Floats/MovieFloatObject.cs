﻿// <copyright file="MovieFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Movie.
    /// </summary>
    public class MovieFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.Figure;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Movie";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Movies?)";

        /// <inheritdoc/>
        public string InternalReferenceType => "movie";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        /// <inheritdoc/>
        public string Description => "Movie";

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
