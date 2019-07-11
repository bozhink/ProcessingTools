﻿// <copyright file="TableAppendixFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Floats
{
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Contracts.Services.Models.Floats;

    /// <summary>
    /// Table Appendix object.
    /// </summary>
    public class TableAppendixFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.Table;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Appendix";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:App\.|Appendix|Appendices)";

        /// <inheritdoc/>
        public string InternalReferenceType => "table-appendix";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeTable;

        /// <inheritdoc/>
        public string Description => "Table Appendix";

        /// <inheritdoc/>
        public string Name => this.Description;

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
