// <copyright file="HabitusFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Habitus.
    /// </summary>
    public class HabitusFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.Figure;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Habitus";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Habitus)";

        /// <inheritdoc/>
        public string InternalReferenceType => "habitus";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        /// <inheritdoc/>
        public string Description => "Habitus";

        /// <inheritdoc/>
        public string Name => this.Description;

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
