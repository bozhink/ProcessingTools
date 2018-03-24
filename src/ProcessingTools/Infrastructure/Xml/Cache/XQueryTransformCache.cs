namespace ProcessingTools.Xml.Cache
{
    using System.Collections.Concurrent;
    using System.IO;
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Xml.Abstractions;
    using ProcessingTools.Xml.Transform;

    public class XQueryTransformCache : AbstractGenericTransformCache<IXQueryTransform>, IXQueryTransformCache
    {
        private static readonly ConcurrentDictionary<string, IXQueryTransform> XQueryTransformObjects = new ConcurrentDictionary<string, IXQueryTransform>();

        protected override ConcurrentDictionary<string, IXQueryTransform> TransformObjects => XQueryTransformObjects;

        protected override IXQueryTransform GetTransformObject(string fileName)
        {
            var query = File.ReadAllText(fileName);

            var transform = new XQueryTransform();
            transform.Load(query: query);

            return transform;
        }
    }
}
