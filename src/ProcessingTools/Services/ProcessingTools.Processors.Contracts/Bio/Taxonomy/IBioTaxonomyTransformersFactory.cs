namespace ProcessingTools.Processors.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Xml;

    public interface IBioTaxonomyTransformersFactory
    {
        IXmlTransformer GetRemoveTaxonNamePartsTransformer();

        IXmlTransformer GetParseTreatmentMetaWithInternalInformationTransformer();
    }
}
