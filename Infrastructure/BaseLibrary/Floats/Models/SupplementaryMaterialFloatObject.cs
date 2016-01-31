namespace ProcessingTools.BaseLibrary.Floats.Models
{
    using Contracts;

    /// <summary>
    /// Supplementary material.
    /// </summary>
    public class SupplementaryMaterialFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $"//supplementary-material[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.SupplementaryMaterial;

        public string FloatTypeNameInLabel => "Supplementary material";

        public string MatchCitationPattern => @"(?:Suppl(?:\.\s*|\s+)materials?|Supplementary\s+materials?)";

        public string RefType => "supplementary-material";
    }
}