namespace ProcessingTools.Processors.Models.Floats
{
    using Contracts;
    using Types;

    /// <summary>
    /// Supplementary material.
    /// </summary>
    public class SupplementaryMaterialFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//supplementary-material[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.SupplementaryMaterial;

        public string FloatTypeNameInLabel => "Supplementary material";

        public string MatchCitationPattern => @"(?:Suppl(?:\.\s*|\s+)materials?|Supplementary\s+materials?)";

        public string InternalReferenceType => "supplementary-material";

        public string ResultantReferenceType => "supplementary-material";

        public string Description => "Supplementary material";
    }
}
