namespace ProcessingTools.Contracts.Processors.Factories.Bio
{
    using ProcessingTools.Contracts;

    public interface IBioTaxonomyTransformersFactory
    {
        IXmlTransformer GetRemoveTaxonNamePartsTransformer();

        IXmlTransformer GetParseTreatmentMetaWithInternalInformationTransformer();
    }
}
