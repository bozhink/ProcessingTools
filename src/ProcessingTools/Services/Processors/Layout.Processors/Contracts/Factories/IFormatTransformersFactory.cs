namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Processors.Contracts;

    public interface IFormatTransformersFactory
    {
        IXmlTransformer GetFormatToNlmTransformer();

        IXmlTransformer GetFormatToSystemTransformer();

        IXmlTransformer GetNlmInitialFormatTransformer();

        IXmlTransformer GetSystemInitialFormatTransformer();
    }
}
