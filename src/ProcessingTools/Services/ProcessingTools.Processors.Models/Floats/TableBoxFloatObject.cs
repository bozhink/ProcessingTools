// <copyright file="TableBoxFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Common.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Text-box of type table.
    /// </summary>
    public class TableBoxFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.Table;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Box";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Box|Boxes)";

        /// <inheritdoc/>
        public string InternalReferenceType => "table";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeTable;

        /// <inheritdoc/>
        public string Description => "Box";

        /// <inheritdoc/>
        public string Name => this.Description;

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
