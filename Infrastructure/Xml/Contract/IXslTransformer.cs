namespace ProcessingTools.Xml.Contract
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    public interface IXslTransformer
    {
        Stream TransformToStream(string xml);

        Stream TransformToStream(XmlNode node);

        Stream TransformToStream(XmlReader reader);

        Task<string> Transform(string xml);

        Task<string> Transform(XmlNode node);

        Task<string> Transform(XmlReader reader);
    }
}
