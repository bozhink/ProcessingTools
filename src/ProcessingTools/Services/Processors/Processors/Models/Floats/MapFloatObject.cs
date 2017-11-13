namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Map.
    /// </summary>
    internal class MapFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.Figure;

        public string FloatTypeNameInLabel => "Map";

        public string MatchCitationPattern => @"(?:Maps?)";

        public string InternalReferenceType => "map";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Map";
    }
}
