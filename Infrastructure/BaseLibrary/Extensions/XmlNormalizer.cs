namespace ProcessingTools.BaseLibrary
{
    using System.Configuration;
    using System.Xml;

    using ProcessingTools.Common;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Infrastructure.Extensions;

    /// <summary>
    /// This class provides extension methods for transformation of Taxpub NLM to system XML schemas.
    /// </summary>
    public static class XmlNormalizer
    {
        private const string FormatNlmToSystemXslPathKey = "FormatNlmToSystemXslPath";
        private const string FormatSystemToNlmXslPathKey = "FormatSystemToNlmXslPath";

        private static string FormatNlmToSystemXslPath => Dictionaries.FileNames.GetOrAdd(FormatNlmToSystemXslPathKey, ConfigurationManager.AppSettings[FormatNlmToSystemXslPathKey]);

        private static string FormatSystemToNlmXslPath => Dictionaries.FileNames.GetOrAdd(FormatSystemToNlmXslPathKey, ConfigurationManager.AppSettings[FormatSystemToNlmXslPathKey]);

        /// <summary>
        /// Transforms a given XML string to system Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToSystemXml(this string xml)
        {
            return xml.ApplyXslTransform(FormatNlmToSystemXslPath);
        }

        /// <summary>
        /// Transforms a given XmlDocument object to system Xml Schema.
        /// </summary>
        /// <param name="xml">XmlDocument object to be transformed.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToSystemXml(this XmlDocument xml)
        {
            return xml.ApplyXslTransform(FormatNlmToSystemXslPath);
        }

        /// <summary>
        /// Transforms a given XML string to Taxpub NLM Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToNlmXml(this string xml)
        {
            return xml.ApplyXslTransform(FormatSystemToNlmXslPath);
        }

        /// <summary>
        /// Transforms a given XmlDocument object to Taxpub NLM Xml Schema.
        /// </summary>
        /// <param name="xml">XmlDocument object to be transformed.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToNlmXml(this XmlDocument xml)
        {
            return xml.ApplyXslTransform(FormatSystemToNlmXslPath);
        }

        /// <summary>
        /// Transforms a given XML string to Taxpub NLM Xml Schema or system Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <param name="articleSchemaType">SchemaType of the document.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToCurrentXml(this string xml, SchemaType articleSchemaType)
        {
            switch (articleSchemaType)
            {
                case SchemaType.Nlm:
                    return xml.NormalizeXmlToNlmXml();

                default:
                    return xml.NormalizeXmlToSystemXml();
            }
        }

        /// <summary>
        /// Transforms a given XmlDocument object to Taxpub NLM Xml Schema or system Xml Schema.
        /// </summary>
        /// <param name="xml">XmlDocument object to be transformed.</param>
        /// <param name="articleSchemaType">SchemaType of the document.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToCurrentXml(this XmlDocument xml, SchemaType articleSchemaType)
        {
            switch (articleSchemaType)
            {
                case SchemaType.Nlm:
                    return xml.NormalizeXmlToNlmXml();

                default:
                    return xml.NormalizeXmlToSystemXml();
            }
        }
    }
}