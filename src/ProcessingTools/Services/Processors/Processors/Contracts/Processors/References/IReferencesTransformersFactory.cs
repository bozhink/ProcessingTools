namespace ProcessingTools.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts;

    public interface IReferencesTransformersFactory
    {
        IXmlTransformer GetReferencesTagTemplateTransformer();

        IXmlTransformer GetReferencesGetReferencesTransformer();
    }
}
