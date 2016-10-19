namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Nlm.Publishing.Constants;
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

        public string ResultantReferenceType => RefTypeAttributeValues.Figure;

        public string Description => "Picture";
    }
}
