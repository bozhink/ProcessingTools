namespace ProcessingTools.Globals.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Xsl;

    public static class XslTransformExtrensions
    {
        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml object and returns the result as a string.
        /// </summary>
        /// <param name="xmlDocument">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlDocument xmlDocument, string xslFileName)
        {
            return xmlDocument.OuterXml.ApplyXslTransform(xslFileName);
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml object and returns the result as a string.
        /// </summary>
        /// <param name="xmlDocument">Input document to be transformed.</param>
        /// <param name="xslTransform">XslCompiledTransform object.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Text.EncoderFallbackException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlDocument xmlDocument, XslCompiledTransform xslTransform)
        {
            return xmlDocument.OuterXml.ApplyXslTransform(xslTransform);
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
            string result = string.Empty;
            try
            {
                using (XmlReader xmlReader = xml.ToXmlReader())
                {
                    result = xmlReader.ApplyXslTransform(xslFileName);
                }
            }
            catch
            {
                throw;
            }

            return result;
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
            string result = string.Empty;
            try
            {
                byte[] bytesContent = Encoding.UTF8.GetBytes(xml);
                using (XmlReader xmlReader = XmlReader.Create(new MemoryStream(bytesContent)))
                {
                    result = xmlReader.ApplyXslTransform(xslTransform);
                }
            }
            catch (EncoderFallbackException e)
            {
                throw new EncoderFallbackException("Input document string must be UFT8 encoded.", e);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml.XmlReader object and returns the result as a string.
        /// </summary>
        /// <param name="xmlReader">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Xml.XmlException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlReader xmlReader, string xslFileName)
        {
            string result = string.Empty;
            if (xslFileName == null || xslFileName.Length < 1)
            {
                throw new ArgumentNullException("XSL file name is invalid.");
            }

            try
            {
                XslCompiledTransform xslTransform = new XslCompiledTransform();
                xslTransform.Load(xslFileName);
                result = xmlReader.ApplyXslTransform(xslTransform);
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

            return result;
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the System.Xml.XmlReader object and returns the result as a string.
        /// </summary>
        /// <param name="xmlReader">Input document to be transformed.</param>
        /// <param name="xslTransform">XslCompiledTransform object.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="System.Xml.Xsl.XsltException"></exception>
        /// <exception cref="System.Exception"></exception>
        public static string ApplyXslTransform(this XmlReader xmlReader, XslCompiledTransform xslTransform)
        {
            string result = string.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                StreamReader streamReader = null;
                try
                {
                    xslTransform.Transform(xmlReader, null, memoryStream);
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
    }
}
