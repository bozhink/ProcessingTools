namespace ProcessingTools.Processors.Models.Floats
{
    using Contracts.Models.Floats;
    using ProcessingTools.Constants.Schema;
    using Types;

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
