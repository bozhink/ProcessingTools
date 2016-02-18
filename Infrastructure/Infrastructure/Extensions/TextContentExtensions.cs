namespace ProcessingTools.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;
    using System.Xml;

    public static class TextContentExtensions
    {
        public static string GetTextContent(this XmlDocument xmlDocument, string xslFileName)
        {
            string text = xmlDocument.ApplyXslTransform(xslFileName);
            text = Regex.Replace(text, @"(?<=\n)\s+", string.Empty);

            return text;
        }
    }
}
