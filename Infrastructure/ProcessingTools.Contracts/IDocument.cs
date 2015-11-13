namespace ProcessingTools.Contracts
{
    using System.Xml;

    public interface IDocument
    {
        XmlNamespaceManager NamespaceManager { get; }

        NameTable NameTable { get; }

        string Xml { get; set; }

        XmlDocument XmlDocument { get; }
    }
}