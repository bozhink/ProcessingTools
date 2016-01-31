namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    /// <summary>
    /// Figure.
    /// </summary>
    public class FigureFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Figure";

        public string MatchCitationPattern => @"(?:Fig\.|Figs|Figures?)";

        public string InternalReferenceType => "fig";
    }
}