// <copyright file="XmlExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;

    /// <summary>
    /// XML Extensions.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Validate XML with XSD.
        /// </summary>
        /// <param name="xsdFileName">XSL  file name.</param>
        /// <param name="namespaceName">Namespace name.</param>
        /// <param name="xml">XML as string.</param>
        /// <param name="message">Validation message.</param>
        /// <returns>Validation status.</returns>
        public static bool ValidateWithXsd(string xsdFileName, string namespaceName, string xml, out string message)
        {
            message = string.Empty;
            var settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Ignore
            };

            try
            {
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml), settings))
                {
                    var document = XDocument.Load(xmlReader);
                    var schemas = new XmlSchemaSet();
                    schemas.Add(namespaceName, xsdFileName);

                    using (var stream = File.OpenRead(xsdFileName))
                    {
                        using (var reader = XmlReader.Create(stream, settings))
                        {
                            schemas.Add(@"http://www.w3.org/2000/09/xmldsig#", reader);
                        }

                        stream.Close();
                    }

                    document.Validate(schemas, null);

                    return true;
                }
            }
            catch (XmlSchemaValidationException ex)
            {
                message = ex.Message;
            }

            return false;
        }
    }
}
