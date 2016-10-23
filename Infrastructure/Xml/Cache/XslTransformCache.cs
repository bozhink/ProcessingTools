namespace ProcessingTools.Xml.Cache
{
    using System.Collections.Concurrent;
    using System.Xml.Xsl;

    using Contracts.Cache;

    public class XslTransformCache : IXslTransformCache
    {
        private static readonly ConcurrentDictionary<string, XslCompiledTransform> XslCompiledTransformObjects = new ConcurrentDictionary<string, XslCompiledTransform>();

        public XslCompiledTransform this[string fileName]
        {
            get
            {
                var transform = XslCompiledTransformObjects.GetOrAdd(fileName, this.GetXslCompiledTransform);
                return transform;
            }
        }

        public bool Remove(string fileName)
        {
            XslCompiledTransform value = null;
            var result = XslCompiledTransformObjects.TryRemove(fileName, out value);
            return result;
        }

        public bool RemoveAll()
        {
            var result = true;
            foreach (var key in XslCompiledTransformObjects.Keys)
            {
                XslCompiledTransform value = null;
                result &= XslCompiledTransformObjects.TryRemove(key, out value);
            }

            return result;
        }

        private XslCompiledTransform GetXslCompiledTransform(string fileName)
        {
            var transform = new XslCompiledTransform();
            transform.Load(fileName);
            return transform;
        }
    }
}
