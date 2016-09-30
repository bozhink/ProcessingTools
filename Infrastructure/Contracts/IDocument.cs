namespace ProcessingTools.Contracts
{
    using System.Linq;
    using System.Text;
    using System.Xml;

    using ProcessingTools.Contracts.Types;

    public interface IDocument
    {
        Encoding Encoding { get; }

        XmlNamespaceManager NamespaceManager { get; }

        NameTable NameTable { get; }

        SchemaType SchemaType { get; set; }

        string Xml { get; set; }

        XmlDocument XmlDocument { get; }

        IQueryable<XmlNode> SelectNodes(string xpath);

        XmlNode SelectSingleNode(string xpath);
    }
}
