namespace ProcessingTools.BaseLibrary
{
    using System.Configuration;
    using System.Xml;
    using ProcessingTools.Infrastructure.Extensions;

    /// <summary>
    /// This class provides extension methods for transformation of Taxpub NLM to system XML schemas.
    /// </summary>
    public static class XmlNormalizer
    {
        /// <summary>
        /// Transforms a given XML string to system Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToSystemXml(this string xml)
        {
            string formatNlmToSystemXslPath = ConfigurationManager.AppSettings["FormatNlmToSystemXslPath"];
            return xml.ApplyXslTransform(formatNlmToSystemXslPath);
        }

        /// <summary>
        /// Transforms a given XmlDocument object to system Xml Schema.
        /// </summary>
        /// <param name="xml">XmlDocument object to be transformed.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToSystemXml(this XmlDocument xml)
        {
            string formatNlmToSystemXslPath = ConfigurationManager.AppSettings["FormatNlmToSystemXslPath"];
            return xml.ApplyXslTransform(formatNlmToSystemXslPath);
        }

        /// <summary>
        /// Transforms a given XML string to Taxpub NLM Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToNlmXml(this string xml)
        {
            string formatSystemToNlmXslPath = ConfigurationManager.AppSettings["FormatSystemToNlmXslPath"];
            return xml.ApplyXslTransform(formatSystemToNlmXslPath);
        }

        /// <summary>
        /// Transforms a given XmlDocument object to Taxpub NLM Xml Schema.
        /// </summary>
        /// <param name="xml">XmlDocument object to be transformed.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToNlmXml(this XmlDocument xml)
        {
            string formatSystemToNlmXslPath = ConfigurationManager.AppSettings["FormatSystemToNlmXslPath"];
            return xml.ApplyXslTransform(formatSystemToNlmXslPath);
        }

        /// <summary>
        /// Transforms a given XML string to Taxpub NLM Xml Schema or system Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <param name="config">Config object which provides the path to Xsl file to be used and the NlmStyle boolean.</param>
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
        /// <param name="config">Config object which provides the path to Xsl file to be used and the NlmStyle boolean.</param>
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