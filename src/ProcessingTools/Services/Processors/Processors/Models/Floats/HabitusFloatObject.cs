namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Models.Floats;

    /// <summary>
    /// Habitus.
    /// </summary>
    internal class HabitusFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Habitus";

        public string MatchCitationPattern => @"(?:Habitus)";

        public string InternalReferenceType => "habitus";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Habitus";
    }
}
