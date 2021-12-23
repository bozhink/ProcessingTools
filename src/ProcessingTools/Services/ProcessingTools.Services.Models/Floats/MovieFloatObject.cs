﻿// <copyright file="MovieFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Floats
{
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Models.Floats;

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
        public string Name => this.Description;

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
