namespace ProcessingTools.Processors.Models.Floats
{
    using Types;

    /// <summary>
    /// Table Appendix object.
    /// </summary>
    internal class TableAppendixFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Table;

        public string FloatTypeNameInLabel => "Appendix";

        public string MatchCitationPattern => @"(?:App\.|Appendix|Appendices)";

        public string InternalReferenceType => "table-appendix";

        public string ResultantReferenceType => "table";

        public string Description => "Table Appendix";
    }
}
