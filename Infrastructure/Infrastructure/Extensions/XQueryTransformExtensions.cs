namespace ProcessingTools.Infrastructure.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;

    using Transform;

    public static class XQueryTransformExtensions
    {
        private static readonly ConcurrentDictionary<string, XQueryTransform> XQueryTransformObjects = new ConcurrentDictionary<string, XQueryTransform>();

        public static async Task<T> DeserializeXQueryTransformOutput<T>(this XmlNode node, string xqueryFileName)
            where T : class
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (string.IsNullOrWhiteSpace(xqueryFileName))
            {
                throw new ArgumentNullException("xqueryFileName");
            }

            var xqueryTransform = XQueryTransformObjects.GetOrAdd(
                xqueryFileName,
                fileName =>
                {
                    var transformer = new XQueryTransform();
                    transformer.Load(new FileStream(fileName, FileMode.Open));
                    return transformer;
                });

            return await node.DeserializeXQueryTransformOutput<T>(xqueryTransform);
        }

        public static Task<T> DeserializeXQueryTransformOutput<T>(this XmlNode node, XQueryTransform xqueryTransform)
            where T : class
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (xqueryTransform == null)
            {
                throw new ArgumentNullException("xqueryTransform");
            }

            return Task.Run(() =>
            {
                var xqueryTransformOutput = xqueryTransform.Evaluate(node);
                T result = xqueryTransformOutput.Deserialize<T>();
                return result;
            });
        }
    }
}