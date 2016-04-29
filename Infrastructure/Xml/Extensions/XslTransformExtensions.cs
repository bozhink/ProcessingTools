namespace ProcessingTools.Xml.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
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
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="XsltException"></exception>
        /// <exception cref="XmlException"></exception>
        /// <exception cref="Exception"></exception>
        public static string ApplyXslTransform(this XmlDocument document, string xslFileName)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return document.OuterXml.ApplyXslTransform(xslFileName);
        }

        /// <summary>
        /// Executes XSL transform using the input document specified by the string object and returns the result as a string.
        /// </summary>
        /// <param name="xml">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="XsltException"></exception>
        /// <exception cref="XmlException"></exception>
        /// <exception cref="Exception"></exception>
        public static string ApplyXslTransform(this string xml, string xslFileName)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
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
        /// Executes XSL transform using the input document specified by the System.Xml.XmlReader object and returns the result as a string.
        /// </summary>
        /// <param name="reader">Input document to be transformed.</param>
        /// <param name="xslFileName">File name path of the XSL file.</param>
        /// <returns>Transformed document as string.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="XsltException"></exception>
        /// <exception cref="XmlException"></exception>
        /// <exception cref="Exception"></exception>
        public static string ApplyXslTransform(this XmlReader reader, string xslFileName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (string.IsNullOrWhiteSpace(xslFileName))
            {
                throw new ArgumentNullException(nameof(xslFileName));
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
        /// <exception cref="XsltException"></exception>
        /// <exception cref="Exception"></exception>
        public static string ApplyXslTransform(this XmlReader reader, XslCompiledTransform xslTransform)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (xslTransform == null)
            {
                throw new ArgumentNullException(nameof(xslTransform));
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

        public static async Task<T> DeserializeXslTransformOutput<T>(this XmlDocument document, string xslFileName)
            where T : class
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return await document.OuterXml.DeserializeXslTransformOutput<T>(xslFileName);
        }

        public static async Task<T> DeserializeXslTransformOutput<T>(this string xml, string xslFileName)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentNullException(nameof(xml));
            }

            T result = null;
            using (XmlReader reader = xml.ToXmlReader())
            {
                result = await reader.DeserializeXslTransformOutput<T>(xslFileName);
            }

            return result;
        }

        public static async Task<T> DeserializeXslTransformOutput<T>(this XmlReader reader, string xslFileName)
            where T : class
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (string.IsNullOrWhiteSpace(xslFileName))
            {
                throw new ArgumentNullException(nameof(xslFileName));
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

                return await reader.DeserializeXslTransformOutput<T>(xslTransform);
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

        public static Task<T> DeserializeXslTransformOutput<T>(this XmlReader reader, XslCompiledTransform xslTransform)
            where T : class
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (xslTransform == null)
            {
                throw new ArgumentNullException(nameof(xslTransform));
            }

            return Task.Run(() =>
            {
                T result = null;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    try
                    {
                        xslTransform.Transform(reader, null, memoryStream);
                        memoryStream.Position = 0;

                        var serializer = new XmlSerializer(typeof(T));
                        result = (T)serializer.Deserialize(memoryStream);
                    }
                    catch (XsltException e)
                    {
                        throw new XsltException("Invalid XSL file.", e);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("General exception.", e);
                    }
                }

                return result;
            });
        }
    }
}