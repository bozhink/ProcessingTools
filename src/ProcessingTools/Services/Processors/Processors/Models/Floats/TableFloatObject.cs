namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Models.Floats;

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

        public string ResultantReferenceType => AttributeValues.RefTypeTable;

        public string Description => "Table";
    }
}
