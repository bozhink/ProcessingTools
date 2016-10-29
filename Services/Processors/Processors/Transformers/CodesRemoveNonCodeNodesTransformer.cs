namespace ProcessingTools.Processors.Transformers
{
    using Contracts.Transformers;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Transformers;

    public class CodesRemoveNonCodeNodesTransformer : XslTransformer<ICodesRemoveNonCodeNodesXslTransformProvider>, ICodesRemoveNonCodeNodesTransformer
    {
        public CodesRemoveNonCodeNodesTransformer(ICodesRemoveNonCodeNodesXslTransformProvider xslTransformProvider)
            : base(xslTransformProvider)
        {
        }
    }
}
