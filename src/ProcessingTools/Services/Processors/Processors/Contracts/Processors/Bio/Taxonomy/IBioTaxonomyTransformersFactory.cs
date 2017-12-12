namespace ProcessingTools.Contracts.Processors.Factories.Bio
{
    using ProcessingTools.Contracts.Xml;

    public interface IBioTaxonomyTransformersFactory
    {
        IXmlTransformer GetRemoveTaxonNamePartsTransformer();

        IXmlTransformer GetParseTreatmentMetaWithInternalInformationTransformer();
    }
}
