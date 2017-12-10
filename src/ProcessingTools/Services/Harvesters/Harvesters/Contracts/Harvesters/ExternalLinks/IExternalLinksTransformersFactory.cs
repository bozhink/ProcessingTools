namespace ProcessingTools.Harvesters.Contracts.Factories
{
    using ProcessingTools.Contracts.Processors;

    public interface IExternalLinksTransformersFactory
    {
        IXmlTransformer GetExternalLinksTransformer();
    }
}
