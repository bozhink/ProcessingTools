namespace ProcessingTools.Contracts.Services.Data.Documents
{
    using ProcessingTools.Contracts.Processors;

    public interface IDocumentsFormatTransformersFactory
    {
        IXmlTransformer GetFormatXmlToHtmlTransformer();

        IXmlTransformer GetFormatHtmlToXmlTransformer();
    }
}
