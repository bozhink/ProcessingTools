namespace ProcessingTools.Processors.Contracts.Factories.Bio
{
    using ProcessingTools.Contracts;

    public interface ICodesTransformerFactory
    {
        IXmlTransformer GetCodesRemoveNonCodeNodesTransformer();
    }
}
