namespace ProcessingTools.Processors.Models.Floats
{
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

        public string ResultantReferenceType => "fig";

        public string Description => "Habitus";
    }
}
