// <copyright file="TableFloatObject.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.Floats;

namespace ProcessingTools.Services.Models.Floats
{
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Common.Enumerations.Nlm;

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

        /// <inheritdoc/>
        public string Name => this.Description;

        /// <inheritdoc/>
        public string TargetXPath => "./*";
    }
}
