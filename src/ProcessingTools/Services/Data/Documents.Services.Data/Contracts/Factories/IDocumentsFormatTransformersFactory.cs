namespace ProcessingTools.Documents.Services.Data.Contracts.Factories
{
    using ProcessingTools.Contracts;

    public interface IDocumentsFormatTransformersFactory
    {
        IXmlTransformer GetFormatXmlToHtmlTransformer();

        IXmlTransformer GetFormatHtmlToXmlTransformer();
    }
}
