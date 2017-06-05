namespace ProcessingTools.Processors.Models.Floats
{
    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts.Models.Floats;

    /// <summary>
    /// Supplementary material.
    /// </summary>
    internal class SupplementaryMaterialFloatObject : IFloatObject
    {
        public string FloatObjectXPath => $".//supplementary-material[contains(string(label),'{this.FloatTypeNameInLabel}')]";

        public FloatsReferenceType FloatReferenceType => FloatsReferenceType.SupplementaryMaterial;

        public string FloatTypeNameInLabel => "Supplementary material";

        public string MatchCitationPattern => @"(?:Suppl(?:\.\s*|\s+)materials?|Supplementary\s+materials?)";

        public string InternalReferenceType => "supplementary-material";

        public string ResultantReferenceType => AttributeValues.RefTypeSupplementaryMaterial;

        public string Description => "Supplementary material";
    }
}
