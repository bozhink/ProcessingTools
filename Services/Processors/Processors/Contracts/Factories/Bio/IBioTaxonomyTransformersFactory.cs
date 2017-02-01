namespace ProcessingTools.Processors.Contracts.Factories.Bio
{
    using ProcessingTools.Contracts;

    public interface IBioTaxonomyTransformersFactory
    {
        IXmlTransformer GetRemoveTaxonNamePartsTransformer();

        IXmlTransformer GetParseTreatmentMetaWithInternalInformationTransformer();
    }
}
