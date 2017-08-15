namespace ProcessingTools.Harvesters.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Xml.Contracts.Wrappers;

    public abstract class AbstractGenericEnumerableXmlHarvester<T> : IGenericEnumerableXmlHarvester<T>
    {
        private readonly IXmlContextWrapper contextWrapper;

        protected AbstractGenericEnumerableXmlHarvester(IXmlContextWrapper contextWrapper)
        {
            this.contextWrapper = contextWrapper ?? throw new ArgumentNullException(nameof(contextWrapper));
        }

        public Task<IEnumerable<T>> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var document = this.contextWrapper.Create(context);

            return this.Run(document);
        }

        protected abstract Task<IEnumerable<T>> Run(XmlDocument document);
    }
}
