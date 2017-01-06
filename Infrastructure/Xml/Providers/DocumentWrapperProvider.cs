namespace ProcessingTools.Xml.Providers
{
    using Contracts.Providers;

    public class DocumentWrapperProvider : IDocumentWrapperProvider
    {
        public string DocumentWrapper => Resources.DocumentWrapper;
    }
}
