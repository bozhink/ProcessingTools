namespace ProcessingTools.Processors.Models.Floats
{
    using Types;

    /// <summary>
    /// Table.
    /// </summary>
    internal class TableFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Table;

        public string FloatTypeNameInLabel => "Table";

        public string MatchCitationPattern => @"(?:Tab\.|Tabs|Tables?)";

        public string InternalReferenceType => "table";

        public string ResultantReferenceType => "table";

        public string Description => "Table";
    }
}
