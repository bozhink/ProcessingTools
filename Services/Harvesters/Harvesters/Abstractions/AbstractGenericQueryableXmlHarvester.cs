namespace ProcessingTools.Harvesters.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Xml.Contracts.Wrappers;

    public abstract class AbstractGenericQueryableXmlHarvester<T> : IGenericEnumerableXmlHarvester<T>
    {
        private readonly IXmlContextWrapperProvider contextWrapperProvider;

        public AbstractGenericQueryableXmlHarvester(IXmlContextWrapperProvider contextWrapperProvider)
        {
            if (contextWrapperProvider == null)
            {
                throw new ArgumentNullException(nameof(contextWrapperProvider));
            }

            this.contextWrapperProvider = contextWrapperProvider;
        }

        public Task<IEnumerable<T>> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var document = this.contextWrapperProvider.Create(context);

            return this.Run(document);
        }

        protected abstract Task<IEnumerable<T>> Run(XmlDocument document);
    }
}
