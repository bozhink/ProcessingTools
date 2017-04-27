namespace ProcessingTools.Xml.Contracts.Factories
{
    using ProcessingTools.Xml.Contracts.Transformers;

    public interface IXslTransformerFactory
    {
        IXslTransformer CreateTransformer(string xslFileName);
    }
}
