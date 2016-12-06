namespace ProcessingTools.Processors.Contracts.Factories.Bio
{
    using ProcessingTools.Contracts;

    public interface ITaxonTreatmentsTransformersFactory
    {
        IXmlTransformer GetTaxonTreatmentFormatTransformer();

        IXmlTransformer GetTaxonTreatmentExtractMaterialsTransformer();
    }
}
