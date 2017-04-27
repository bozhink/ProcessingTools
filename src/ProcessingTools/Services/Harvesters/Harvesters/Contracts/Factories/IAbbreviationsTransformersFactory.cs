namespace ProcessingTools.Harvesters.Contracts.Factories
{
    using ProcessingTools.Contracts;

    public interface IAbbreviationsTransformersFactory
    {
        IXmlTransformer GetAbbreviationsTransformer();
    }
}
