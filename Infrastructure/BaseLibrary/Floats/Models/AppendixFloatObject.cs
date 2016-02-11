namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    /// <summary>
    /// Appendix.
    /// </summary>
    public class AppendixFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//app[contains(string(title),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.Appendix;

        public string FloatTypeNameInLabel => "Append";

        public string MatchCitationPattern => @"(?:Append(?:\.|[a-z]+))";

        public string InternalReferenceType => "app";

        public string ResultantReferenceType => "app";

        public string Description => "Appendix";
    }
}
