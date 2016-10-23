namespace ProcessingTools.Xml.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Xml.Xsl;

    public static class XslTransformExtrensions
    {
        private static readonly ConcurrentDictionary<string, XslCompiledTransform> XslCompiledTransformObjects = new ConcurrentDictionary<string, XslCompiledTransform>();

        public static async Task<T> DeserializeXslTransformOutput<T>(this XmlDocument document, string xslFileName)
            where T : class
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return await document.OuterXml.DeserializeXslTransformOutput<T>(xslFileName);
        }

        private static async Task<T> DeserializeXslTransformOutput<T>(this string xml, string xslFileName)
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

        private static async Task<T> DeserializeXslTransformOutput<T>(this XmlReader reader, string xslFileName)
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

        private static Task<T> DeserializeXslTransformOutput<T>(this XmlReader reader, XslCompiledTransform xslTransform)
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
