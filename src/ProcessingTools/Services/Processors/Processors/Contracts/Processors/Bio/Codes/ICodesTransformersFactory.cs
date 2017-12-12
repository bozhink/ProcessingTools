namespace ProcessingTools.Contracts.Processors.Factories.Bio
{
    using ProcessingTools.Contracts.Xml;

    public interface ICodesTransformersFactory
    {
        IXmlTransformer GetCodesRemoveNonCodeNodesTransformer();
    }
}
