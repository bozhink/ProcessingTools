namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    /// <summary>
    /// Plate.
    /// </summary>
    public class PlateFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Plate";

        public string MatchCitationPattern => @"(?:Plates?)";

        public string RefType => "plate";
    }
}