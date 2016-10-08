namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Nlm.Publishing.Constants;
    using Types;

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

        public string ResultantReferenceType => RefTypeAttributeValues.Figure;

        public string Description => "Habitus";
    }
}
