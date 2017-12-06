namespace ProcessingTools.Contracts.Processors.Factories.Bio
{
    using ProcessingTools.Contracts;

    public interface ITaxonTreatmentsTransformersFactory
    {
        IXmlTransformer GetTaxonTreatmentFormatTransformer();

        IXmlTransformer GetTaxonTreatmentExtractMaterialsTransformer();
    }
}
