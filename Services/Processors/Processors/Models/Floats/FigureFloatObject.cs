namespace ProcessingTools.Processors.Models.Floats
{
    using Types;

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

        public string ResultantReferenceType => "fig";

        public string Description => "Figure";
    }
}
