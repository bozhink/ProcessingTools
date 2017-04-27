namespace ProcessingTools.Processors.Models.Floats
{
    using Contracts.Models.Floats;
    using ProcessingTools.Constants.Schema;
    using Types;

    /// <summary>
    /// Movie.
    /// </summary>
    internal class MovieFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Movie";

        public string MatchCitationPattern => @"(?:Movies?)";

        public string InternalReferenceType => "movie";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Movie";
    }
}
