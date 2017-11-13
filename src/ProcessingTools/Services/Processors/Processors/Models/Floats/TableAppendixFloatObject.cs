namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Table Appendix object.
    /// </summary>
    internal class TableAppendixFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.Table;

        public string FloatTypeNameInLabel => "Appendix";

        public string MatchCitationPattern => @"(?:App\.|Appendix|Appendices)";

        public string InternalReferenceType => "table-appendix";

        public string ResultantReferenceType => AttributeValues.RefTypeTable;

        public string Description => "Table Appendix";
    }
}
