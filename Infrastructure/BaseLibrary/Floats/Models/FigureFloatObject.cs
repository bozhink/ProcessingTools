namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    public class FigureFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Figure";

        public string LabelPattern => "Fig\\.|Figs|Figures?";

        public string RefType => "fig";
    }
}