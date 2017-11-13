namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Picture
    /// </summary>
    internal class PictureFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//fig[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.Figure;

        public string FloatTypeNameInLabel => "Picture";

        public string MatchCitationPattern => @"(?:Pict\.|Pictures?)";

        public string InternalReferenceType => "picture";

        public string ResultantReferenceType => AttributeValues.RefTypeFigure;

        public string Description => "Picture";
    }
}
