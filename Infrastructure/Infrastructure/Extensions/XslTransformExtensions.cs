namespace ProcessingTools.Infrastructure.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Xsl;

    public static class XslTransformExtrensions
    {
        private static readonly ConcurrentDictionary<string, XslCompiledTransform> XslCompiledTransformObjects = new ConcurrentDictionary<string, XslCompiledTransform>();

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml object and returns the result as a string.
        /// </summary>
        /// <param name="document">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlDocument document, string xslFileName)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            return document.OuterXml.ApplyXslTransform(xslFileName);
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml object and returns the result as a string.
        /// </summary>
        /// <param name="document">Input document to be transformed.</param>
        /// <param name="xslTransform">XslCompiledTransform object.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlDocument document, XslCompiledTransform xslTransform)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            return document.OuterXml.ApplyXslTransform(xslTransform);
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the string object and returns the result as a string.
        /// </summary>
        /// <param name="xml">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this string xml, string xslFileName)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException("xml");
            }

            try
            {
                string result = string.Empty;
                using (XmlReader reader = xml.ToXmlReader())
                {
                    result = reader.ApplyXslTransform(xslFileName);
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the string object and returns the result as a string.
        /// </summary>
        /// <param name="xml">Input document to be transformed.</param>
        /// <param name="xslTransform">XslCompiledTransform object.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this string xml, XslCompiledTransform xslTransform)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException("xml");
            }

            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(xml);
                string result = string.Empty;
                using (XmlReader reader = XmlReader.Create(new MemoryStream(bytes)))
                {
                    result = reader.ApplyXslTransform(xslTransform);
                }

                return result;
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException("Input document string must be UFT8 encoded.", e);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml.XmlReader object and returns the result as a string.
        /// </summary>
        /// <param name="reader">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlReader reader, string xslFileName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (string.IsNullOrWhiteSpace(xslFileName))
            {
                throw new ArgumentNullException("xslFileName", "XSL file name is invalid.");
            }

            try
            {
                var xslTransform = XslCompiledTransformObjects.GetOrAdd(
                    xslFileName,
                    fileName =>
                    {
                        var transform = new XslCompiledTransform();
                        transform.Load(fileName);
                        return transform;
                    });

                string result = reader.ApplyXslTransform(xslTransform);
                return result;
            }
            catch (IOException e)
            {
                throw new IOException("Cannot read XSL file.", e);
            }
            catch (XsltException e)
            {
                throw new XsltException("Invalid XSL file.", e);
            }
            catch (XmlException e)
            {
                throw new XmlException("Invalid XML file.", e);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml.XmlReader object and returns the result as a string.
        /// </summary>
        /// <param name="reader">Input document to be transformed.</param>
        /// <param name="xslTransform">XslCompiledTransform object.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlReader reader, XslCompiledTransform xslTransform)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (xslTransform == null)
            {
                throw new ArgumentNullException("xslTransform");
            }

            string result = string.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                StreamReader streamReader = null;
                try
                {
                    xslTransform.Transform(reader, null, memoryStream);
                    memoryStream.Position = 0;
                    streamReader = new StreamReader(memoryStream);
                }
                catch (XsltException e)
                {
                    throw new XsltException("Invalid XSL file.", e);
                }
                catch (Exception e)
                {
                    throw new Exception("General exception.", e);
                }
                finally
                {
                    if (streamReader != null)
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
            }

            return result;
        }

        public static string GetTextContent(this XmlDocument xmlDocument, XslCompiledTransform xslTransform)
        {
            string text = xmlDocument.ApplyXslTransform(xslTransform);
            text = Regex.Replace(text, @"(?<=\n)\s+", string.Empty);

            return text;
        }
    }
}