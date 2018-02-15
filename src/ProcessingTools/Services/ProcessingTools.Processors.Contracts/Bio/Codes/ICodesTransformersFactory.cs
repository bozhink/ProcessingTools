namespace ProcessingTools.Processors.Contracts.Bio.Codes
{
    using ProcessingTools.Contracts.Xml;

    public interface ICodesTransformersFactory
    {
        IXmlTransformer GetCodesRemoveNonCodeNodesTransformer();
    }
}
