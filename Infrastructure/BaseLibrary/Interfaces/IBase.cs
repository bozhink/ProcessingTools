namespace ProcessingTools.BaseLibrary
{
    using System.Xml;
    using Configurator;

    public interface IBase
    {
        string Xml { get; set; }

        XmlDocument XmlDocument { get; }

        Config Config { get; }

        XmlNamespaceManager NamespaceManager { get; }
    }
}
