namespace ProcessingTools.BaseLibrary
{
    using System.Xml;
    using Configurator;
    using Globals.Extensions;

    /// <summary>
    /// This class provides extension methods for transformation of Taxpub NLM to system XML schemas.
    /// </summary>
    public static class XmlNormalizer
    {
        /// <summary>
        /// Transforms a given XML string to system Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <param name="config">Config object which provides the path to Xsl file to be used.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToSystemXml(this string xml, Config config)
        {
            return xml.ApplyXslTransform(config.FormatXslNlmToSystem);
        }

        /// <summary>
        /// Transforms a given XmlDocument object to system Xml Schema.
        /// </summary>
        /// <param name="xml">XmlDocument object to be transformed.</param>
        /// <param name="config">Config object which provides the path to Xsl file to be used.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToSystemXml(this XmlDocument xml, Config config)
        {
            return xml.ApplyXslTransform(config.FormatXslNlmToSystem);
        }

        /// <summary>
        /// Transforms a given XML string to Taxpub NLM Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <param name="config">Config object which provides the path to Xsl file to be used.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToNlmXml(this string xml, Config config)
        {
            return xml.ApplyXslTransform(config.FormatXslSystemToNlm);
        }

        /// <summary>
        /// Transforms a given XmlDocument object to Taxpub NLM Xml Schema.
        /// </summary>
        /// <param name="xml">XmlDocument object to be transformed.</param>
        /// <param name="config">Config object which provides the path to Xsl file to be used.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToNlmXml(this XmlDocument xml, Config config)
        {
            return xml.ApplyXslTransform(config.FormatXslSystemToNlm);
        }

        /// <summary>
        /// Transforms a given XML string to Taxpub NLM Xml Schema or system Xml Schema.
        /// </summary>
        /// <param name="xml">XML as string to be transformed.</param>
        /// <param name="config">Config object which provides the path to Xsl file to be used and the NlmStyle boolean.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToCurrentXml(this string xml, Config config)
        {
            switch (config.ArticleSchemaType)
            {
                case SchemaType.Nlm:
                    return xml.NormalizeXmlToNlmXml(config);

                default:
                    return xml.NormalizeXmlToSystemXml(config);
            }
        }

        /// <summary>
        /// Transforms a given XmlDocument object to Taxpub NLM Xml Schema or system Xml Schema.
        /// </summary>
        /// <param name="xml">XmlDocument object to be transformed.</param>
        /// <param name="config">Config object which provides the path to Xsl file to be used and the NlmStyle boolean.</param>
        /// <returns>Transformed XML as string.</returns>
        public static string NormalizeXmlToCurrentXml(this XmlDocument xml, Config config)
        {
            switch (config.ArticleSchemaType)
            {
                case SchemaType.Nlm:
                    return xml.NormalizeXmlToNlmXml(config);

                default:
                    return xml.NormalizeXmlToSystemXml(config);
            }
        }
    }
}