namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Models.Floats;

    /// <summary>
    /// Figure.
    /// </summary>
    internal class FigureFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Figure";

        public string MatchCitationPattern => @"(?:Fig\.|Figs|Figures?)";

        public string InternalReferenceType => "fig";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Figure";
    }
}
