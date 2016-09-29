namespace ProcessingTools.Xml.Extensions
{
    using System.Text.RegularExpressions;
    using System.Xml;

    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Providers;
    using ProcessingTools.Xml.Transformers;

    public static class TextContentExtensions
    {
        public static string GetTextContent(this XmlDocument xmlDocument)
        {
            // TODO: DI
            var transformer = new XslTransformer(new TextContentXslTransformProvider(new XslTransformCache()));

            // TODO: async, DI
            var text = transformer.Transform(xmlDocument).Result;
            text = Regex.Replace(text, @"(?<=\n)\s+", string.Empty);

            return text;
        }
    }
}
