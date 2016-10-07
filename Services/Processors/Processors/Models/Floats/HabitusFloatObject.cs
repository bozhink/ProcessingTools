namespace ProcessingTools.Processors.Models.Floats
{
    using Contracts;
    using Types;

    /// <summary>
    /// Habitus.
    /// </summary>
    public class HabitusFloatObject : IFloatObject
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
