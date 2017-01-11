namespace ProcessingTools.Bio.Taxonomy.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts;

    public interface IBioTaxonomyTransformersFactory
    {
        IXmlTransformer GetRemoveTaxonNamePartsTransformer();

        IXmlTransformer GetParseTreatmentMetaWithInternalInformationTransformer();
    }
}
