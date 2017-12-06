namespace ProcessingTools.Contracts.Processors.Factories.Bio
{
    using ProcessingTools.Contracts;

    public interface ICodesTransformersFactory
    {
        IXmlTransformer GetCodesRemoveNonCodeNodesTransformer();
    }
}
