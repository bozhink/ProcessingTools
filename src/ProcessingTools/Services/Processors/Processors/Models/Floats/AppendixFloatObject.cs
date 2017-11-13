namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations.Nlm;
    using ProcessingTools.Processors.Models.Contracts.Floats;

    /// <summary>
    /// Appendix.
    /// </summary>
    internal class AppendixFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//app[contains(string(title),'{this.FloatTypeNameInLabel}')]";

        public ReferenceType FloatReferenceType => ReferenceType.Appendix;

        public string FloatTypeNameInLabel => "Append";

        public string MatchCitationPattern => @"(?:Append(?:\.|[a-z]+))";

        public string InternalReferenceType => "app";

        public string ResultantReferenceType => AttributeValues.RefTypeAppendix;

        public string Description => "Appendix";
    }
}
