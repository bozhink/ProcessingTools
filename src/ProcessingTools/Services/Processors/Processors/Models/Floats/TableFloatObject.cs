namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Table.
    /// </summary>
    internal class TableFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.Table;

        public string FloatTypeNameInLabel => "Table";

        public string MatchCitationPattern => @"(?:Tab\.|Tabs|Tables?)";

        public string InternalReferenceType => "table";

        public string ResultantReferenceType => AttributeValues.RefTypeTable;

        public string Description => "Table";
    }
}
