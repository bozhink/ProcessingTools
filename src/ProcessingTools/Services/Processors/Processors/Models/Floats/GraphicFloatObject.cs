namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Contracts.Models.Floats;

    /// <summary>
    /// Graphic.
    /// </summary>
    internal class GraphicFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.Figure;

        public string FloatTypeNameInLabel => "Graphic";

        public string MatchCitationPattern => @"(?:Graphics?)";

        public string InternalReferenceType => "graphic";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Graphic";
    }
}
