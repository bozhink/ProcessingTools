namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts.Processors;

    public interface IFormatTransformersFactory
    {
        IXmlTransformer GetFormatToNlmTransformer();

        IXmlTransformer GetFormatToSystemTransformer();

        IXmlTransformer GetNlmInitialFormatTransformer();

        IXmlTransformer GetSystemInitialFormatTransformer();
    }
}
