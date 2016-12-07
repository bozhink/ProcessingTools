namespace ProcessingTools.Harvesters.Contracts.Factories
{
    using ProcessingTools.Contracts;

    public interface IExternalLinksTransformersFactory
    {
        IXmlTransformer GetExternalLinksTransformer();
    }
}
