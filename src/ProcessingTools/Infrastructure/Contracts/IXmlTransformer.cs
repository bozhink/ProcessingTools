namespace ProcessingTools.Contracts
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    public interface IXmlTransformer : ITransformer
    {
        Task<string> TransformAsync(string xml);

        Task<string> TransformAsync(XmlNode node);

        Task<string> TransformAsync(XmlReader reader, bool closeReader);

        Stream TransformToStream(string xml);

        Stream TransformToStream(XmlNode node);

        Stream TransformToStream(XmlReader reader);
    }
}
