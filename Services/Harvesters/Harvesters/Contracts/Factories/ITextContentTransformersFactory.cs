namespace ProcessingTools.Harvesters.Contracts.Factories
{
    using ProcessingTools.Contracts;

    public interface ITextContentTransformersFactory
    {
        IXmlTransformer GetTextContentTransformer();
    }
}
