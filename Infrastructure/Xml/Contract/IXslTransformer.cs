namespace ProcessingTools.Xml.Contract
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;

    public interface IXslTransformer : IProcessor
    {
        Task<string> Transform(string xml);

        Task<string> Transform(XmlNode node);

        Task<string> Transform(XmlReader reader, bool closeReader);

        Stream TransformToStream(string xml);

        Stream TransformToStream(XmlNode node);

        Stream TransformToStream(XmlReader reader);
    }
}
