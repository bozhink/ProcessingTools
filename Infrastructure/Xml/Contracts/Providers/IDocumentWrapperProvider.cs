namespace ProcessingTools.Xml.Contracts.Providers
{
    using ProcessingTools.Contracts;

    public interface IDocumentWrapperProvider
    {
        IDocument Create();
    }
}
