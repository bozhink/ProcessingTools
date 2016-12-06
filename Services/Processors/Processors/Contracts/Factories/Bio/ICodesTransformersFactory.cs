namespace ProcessingTools.Processors.Contracts.Factories.Bio
{
    using ProcessingTools.Contracts;

    public interface ICodesTransformersFactory
    {
        IXmlTransformer GetCodesRemoveNonCodeNodesTransformer();
    }
}
