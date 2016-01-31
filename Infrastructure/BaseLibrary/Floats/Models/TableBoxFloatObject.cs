namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    /// <summary>
    /// Textbox of type table.
    /// </summary>
    public class TableBoxFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//table-wrap[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Table;

        public string FloatTypeNameInLabel => "Box";

        public string MatchCitationPattern => @"(?:Box|Boxes)";

        public string InternalReferenceType => "table";

        public string ResultantReferenceType => "table";
    }
}