namespace ProcessingTools.Xml.Factories
{
    using System;
    using System.Xml.Xsl;

    using Contracts;

    public abstract class XslTransformAbstractProvider : IXslTransformProvider
    {
        private readonly IXslTransformCache cache;

        public XslTransformAbstractProvider(IXslTransformCache cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            this.cache = cache;
        }

        protected abstract string XslFileName { get; }

        public XslCompiledTransform GetXslTransform() => this.cache[this.XslFileName];
    }
}
