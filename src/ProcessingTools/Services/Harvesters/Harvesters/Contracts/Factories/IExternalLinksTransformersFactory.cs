namespace ProcessingTools.Harvesters.Contracts.Factories
{
    using ProcessingTools.Processors.Contracts;

    public interface IExternalLinksTransformersFactory
    {
        IXmlTransformer GetExternalLinksTransformer();
    }
}
