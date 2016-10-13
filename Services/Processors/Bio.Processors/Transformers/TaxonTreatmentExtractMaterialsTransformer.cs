namespace ProcessingTools.Bio.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class TaxonTreatmentExtractMaterialsTransformer : XslTransformer<ITaxonTreatmentExtractMaterialsXslTransformProvider>, ITaxonTreatmentExtractMaterialsTransformer
    {
        public TaxonTreatmentExtractMaterialsTransformer(ITaxonTreatmentExtractMaterialsXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}
