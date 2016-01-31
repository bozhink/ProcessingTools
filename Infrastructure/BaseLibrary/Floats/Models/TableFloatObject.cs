namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    /// <summary>
    /// Table.
    /// </summary>
    public class TableFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Table;

        public string FloatTypeNameInLabel => "Table";

        public string MatchCitationPattern => @"(?:Tab\.|Tabs|Tables?)";

        public string InternalReferenceType => "table";
    }
}