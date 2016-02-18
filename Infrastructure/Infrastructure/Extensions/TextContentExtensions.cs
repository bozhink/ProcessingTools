namespace ProcessingTools.Infrastructure.Extensions
{
    using System;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Xml;

    public static class TextContentExtensions
    {
        private const string TextContentXslFileNameKey = "TextContentXslFileName";

        private static string textContentXslFileName;

        static TextContentExtensions()
        {
            TextContentXslFileName = ConfigurationManager.AppSettings[TextContentXslFileNameKey];
        }

        private static string TextContentXslFileName
        {
            get
            {
                return textContentXslFileName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ApplicationException("TextContentXslFileName is null or white-space.");
                }

                textContentXslFileName = value;
            }
        }

        public static string GetTextContent(this XmlDocument xmlDocument)
        {
            string text = xmlDocument.ApplyXslTransform(TextContentXslFileName);
            text = Regex.Replace(text, @"(?<=\n)\s+", string.Empty);

            return text;
        }
    }
}
