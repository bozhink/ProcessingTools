namespace ProcessingTools.Contracts.Processors.Factories
{
    using ProcessingTools.Contracts;

    public interface IReferencesTransformersFactory
    {
        IXmlTransformer GetReferencesTagTemplateTransformer();

        IXmlTransformer GetReferencesGetReferencesTransformer();
    }
}
