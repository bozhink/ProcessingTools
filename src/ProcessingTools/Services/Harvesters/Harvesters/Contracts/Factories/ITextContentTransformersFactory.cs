namespace ProcessingTools.Harvesters.Contracts.Factories
{
    using ProcessingTools.Processors.Contracts;

    public interface ITextContentTransformersFactory
    {
        IXmlTransformer GetTextContentTransformer();
    }
}
