namespace ProcessingTools.Services.Contracts.Data.Documents
{
    using ProcessingTools.Processors.Contracts;

    public interface IDocumentsFormatTransformersFactory
    {
        IXmlTransformer GetFormatXmlToHtmlTransformer();

        IXmlTransformer GetFormatHtmlToXmlTransformer();
    }
}
