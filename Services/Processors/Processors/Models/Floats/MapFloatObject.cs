namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using Types;

    /// <summary>
    /// Map.
    /// </summary>
    internal class MapFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Map";

        public string MatchCitationPattern => @"(?:Maps?)";

        public string InternalReferenceType => "map";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Map";
    }
}
