namespace ProcessingTools.Processors.Models.Floats
{
    using Contracts;
    using Types;

    /// <summary>
    /// Table.
    /// </summary>
    public class TableFloatObject : IFloatObject
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
