namespace ProcessingTools.Xml.Abstracts
{
    using System;
    using System.Xml.Xsl;

    using Contracts.Cache;
    using Contracts.Providers;

    public abstract class AbstractXslTransformProvider : IXslTransformProvider
    {
        private readonly IXslTransformCache cache;

        public AbstractXslTransformProvider(IXslTransformCache cache)
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
