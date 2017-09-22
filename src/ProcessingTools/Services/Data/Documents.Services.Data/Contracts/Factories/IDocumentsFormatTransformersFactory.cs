namespace ProcessingTools.Documents.Services.Data.Contracts.Factories
{
    using ProcessingTools.Processors.Contracts;

    public interface IDocumentsFormatTransformersFactory
    {
        IXmlTransformer GetFormatXmlToHtmlTransformer();

        IXmlTransformer GetFormatHtmlToXmlTransformer();
    }
}
