namespace ProcessingTools.Harvesters.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Xml.Contracts.Providers;

    public abstract class AbstractGenericQueryableXmlHarvester<T> : IGenericQueryableXmlHarvester<T>
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

        public Task<IQueryable<T>> Harvest(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var document = this.contextWrapperProvider.Create(context);

            return this.Run(document);
        }

        protected abstract Task<IQueryable<T>> Run(XmlDocument document);
    }
}
