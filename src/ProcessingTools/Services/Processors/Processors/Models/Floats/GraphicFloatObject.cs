namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Models.Floats;

    /// <summary>
    /// Graphic.
    /// </summary>
    internal class GraphicFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Graphic";

        public string MatchCitationPattern => @"(?:Graphics?)";

        public string InternalReferenceType => "graphic";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Graphic";
    }
}
