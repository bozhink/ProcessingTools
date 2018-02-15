namespace ProcessingTools.Contracts.Processors.Factories.Bio
{
    using ProcessingTools.Contracts.Xml;

    public interface ITaxonTreatmentsTransformersFactory
    {
        IXmlTransformer GetTaxonTreatmentFormatTransformer();

        IXmlTransformer GetTaxonTreatmentExtractMaterialsTransformer();
    }
}
