namespace ProcessingTools.Xml.Contracts
{
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;

    public interface IXslTransformer : IProcessor
    {
        Task<string> Transform(string xml, IXslTransformProvider xslTransformProvider);

        Task<string> Transform(XmlNode node, IXslTransformProvider xslTransformProvider);

        Task<string> Transform(XmlReader reader, bool closeReader, IXslTransformProvider xslTransformProvider);

        Stream TransformToStream(string xml, IXslTransformProvider xslTransformProvider);

        Stream TransformToStream(XmlNode node, IXslTransformProvider xslTransformProvider);

        Stream TransformToStream(XmlReader reader, IXslTransformProvider xslTransformProvider);
    }
}
