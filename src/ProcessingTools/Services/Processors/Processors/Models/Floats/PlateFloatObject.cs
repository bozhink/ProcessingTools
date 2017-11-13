namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Plate.
    /// </summary>
    internal class PlateFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.Figure;

        public string FloatTypeNameInLabel => "Plate";

        public string MatchCitationPattern => @"(?:Plates?)";

        public string InternalReferenceType => "plate";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Plate";
    }
}
