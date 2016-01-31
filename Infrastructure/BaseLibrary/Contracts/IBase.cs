namespace ProcessingTools.BaseLibrary.Contracts
{
    using System.Xml;

    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;

    public interface IBase : IDocument
    {
        Config Config { get; }
    }
}
