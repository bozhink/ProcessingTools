namespace ProcessingTools.Xml.Contracts
{
    using System.IO;
    using System.Xml;

    public interface IXQueryTransform
    {
        XmlDocument Evaluate(XmlNode node);

        void Load(string query);

        void Load(Stream queryStream);
    }
}
