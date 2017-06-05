namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Models.Floats;

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

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Picture";
    }
}
