namespace ProcessingTools.Contracts.Processors.Factories
{
    using ProcessingTools.Contracts.Xml;

    public interface IReferencesTransformersFactory
    {
        IXmlTransformer GetReferencesTagTemplateTransformer();

        IXmlTransformer GetReferencesGetReferencesTransformer();
    }
}
