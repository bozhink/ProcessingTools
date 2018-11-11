// <copyright file="AppendixFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Appendix.
    /// </summary>
    public class AppendixFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//app[contains(string(title),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.Appendix;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Append";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Append(?:\.|[a-z]+))";

        /// <inheritdoc/>
        public string InternalReferenceType => "app";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeAppendix;

        /// <inheritdoc/>
        public string Description => "Appendix";

        /// <inheritdoc/>
        public string Name => this.Description;

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
