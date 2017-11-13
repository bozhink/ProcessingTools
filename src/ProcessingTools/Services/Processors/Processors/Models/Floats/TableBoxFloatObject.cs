namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Text-box of type table.
    /// </summary>
    internal class TableBoxFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.Table;

        public string FloatTypeNameInLabel => "Box";

        public string MatchCitationPattern => @"(?:Box|Boxes)";

        public string InternalReferenceType => "table";

        public string ResultantReferenceType => AttributeValues.RefTypeTable;

        public string Description => "Box";
    }
}
