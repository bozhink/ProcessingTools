namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    /// <summary>
    /// Map.
    /// </summary>
    public class MapFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Map";

        public string MatchCitationPattern => @"(?:Maps?)";

        public string RefType => "map";
    }
}