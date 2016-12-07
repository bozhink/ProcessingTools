namespace ProcessingTools.Xml.Contracts.Factories
{
    using ProcessingTools.Xml.Contracts.Transformers;

    public interface IXQueryTransformerFactory
    {
        IXQueryTransformer CreateTransformer(string xqueryFileName);
    }
}
