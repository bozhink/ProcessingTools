namespace ProcessingTools.BaseLibrary.Contracts
{
    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;

    public interface IConfigurableDocument : IDocument
    {
        Config Config { get; }
    }
}