namespace ProcessingTools.Processors.Models.Floats
{
    using Types;

    /// <summary>
    /// Picture
    /// </summary>
    internal class PictureFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Figure;

        public string FloatTypeNameInLabel => "Picture";

        public string MatchCitationPattern => @"(?:Pict\.|Pictures?)";

        public string InternalReferenceType => "picture";

        public string ResultantReferenceType => "fig";

        public string Description => "Picture";
    }
}
