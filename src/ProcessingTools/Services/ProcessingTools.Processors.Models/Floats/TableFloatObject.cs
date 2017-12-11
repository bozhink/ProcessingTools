// <copyright file="TableFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts.Models.Processors.Floats;
    using ProcessingTools.Enumerations.Nlm;

    /// <summary>
    /// Table.
    /// </summary>
    public class TableFloatObject : IFloatObject
    {
        /// <inheritdoc/>
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        /// <inheritdoc/>
        public ReferenceType FloatReferenceType => ReferenceType.Table;

        /// <inheritdoc/>
        public string FloatTypeNameInLabel => "Table";

        /// <inheritdoc/>
        public string MatchCitationPattern => @"(?:Tab\.|Tabs|Tables?)";

        /// <inheritdoc/>
        public string InternalReferenceType => "table";

        /// <inheritdoc/>
        public string ResultantReferenceType => AttributeValues.RefTypeTable;

        /// <inheritdoc/>
        public string Description => "Table";
    }
}
