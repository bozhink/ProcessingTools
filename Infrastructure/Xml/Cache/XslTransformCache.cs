namespace ProcessingTools.Xml.Cache
{
    using System.Collections.Concurrent;
    using System.Xml.Xsl;

    using Abstractions;
    using Contracts.Cache;

    public class XslTransformCache : AbstractGenericTransformCache<XslCompiledTransform>, IXslTransformCache
    {
        private static readonly ConcurrentDictionary<string, XslCompiledTransform> XslCompiledTransformObjects = new ConcurrentDictionary<string, XslCompiledTransform>();

        protected override ConcurrentDictionary<string, XslCompiledTransform> TransformObjects => XslCompiledTransformObjects;

        protected override XslCompiledTransform GetTransformObject(string fileName)
        {
            var transform = new XslCompiledTransform();
            transform.Load(fileName);
            return transform;
        }
    }
}
