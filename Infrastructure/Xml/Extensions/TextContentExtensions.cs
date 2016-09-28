namespace ProcessingTools.Xml.Extensions
{
    using System.Text.RegularExpressions;
    using System.Xml;

    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Processors;
    using ProcessingTools.Xml.Providers;

    public static class TextContentExtensions
    {
        public static string GetTextContent(this XmlDocument xmlDocument)
        {
            // TODO: DI
            var transformer = new XslTransformer();

            // TODO: async, DI
            var text = transformer.Transform(xmlDocument, new TextContentXslTransformProvider(new XslTransformCache())).Result;
            text = Regex.Replace(text, @"(?<=\n)\s+", string.Empty);

            return text;
        }
    }
}
