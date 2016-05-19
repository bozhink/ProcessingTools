namespace ProcessingTools.Contracts
{
    using System.Text;
    using System.Xml;

    public interface IDocument
    {
        Encoding Encoding { get; }

        XmlNamespaceManager NamespaceManager { get; }

        NameTable NameTable { get; }

        string Xml { get; set; }

        XmlDocument XmlDocument { get; }
    }
}