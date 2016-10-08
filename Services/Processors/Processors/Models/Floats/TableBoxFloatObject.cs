﻿namespace ProcessingTools.Processors.Models.Floats
{
    using Types;

    /// <summary>
    /// Text-box of type table.
    /// </summary>
    internal class TableBoxFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Table;

        public string FloatTypeNameInLabel => "Box";

        public string MatchCitationPattern => @"(?:Box|Boxes)";

        public string InternalReferenceType => "table";

        public string ResultantReferenceType => "table";

        public string Description => "Box";
    }
}
