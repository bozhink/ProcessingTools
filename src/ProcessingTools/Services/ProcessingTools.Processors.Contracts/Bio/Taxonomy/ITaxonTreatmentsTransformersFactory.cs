namespace ProcessingTools.Processors.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Xml;

    public interface ITaxonTreatmentsTransformersFactory
    {
        IXmlTransformer GetTaxonTreatmentFormatTransformer();

        IXmlTransformer GetTaxonTreatmentExtractMaterialsTransformer();
    }
}
