namespace ProcessingTools.Xml.Abstractions
{
    using System;
    using Contracts;
    using Contracts.Cache;
    using Contracts.Providers;

    public abstract class AbstractXQueryTransformProvider : IXQueryTransformProvider
    {
        private readonly IXQueryTransformCache cache;

        public AbstractXQueryTransformProvider(IXQueryTransformCache cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            this.cache = cache;
        }

        protected abstract string XQueryFileName { get; }

        public IXQueryTransform GetXQueryTransform() => this.cache[this.XQueryFileName];
    }
}
