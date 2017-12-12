namespace ProcessingTools.Harvesters.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Contracts.Xml;

    public abstract class AbstractEnumerableXmlHarvester<TModel> : IEnumerableXmlHarvester<TModel>
    {
        private readonly IXmlContextWrapper contextWrapper;

        protected AbstractEnumerableXmlHarvester(IXmlContextWrapper contextWrapper)
        {
            this.contextWrapper = contextWrapper ?? throw new ArgumentNullException(nameof(contextWrapper));
        }

        public Task<TModel[]> HarvestAsync(XmlNode context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var document = this.contextWrapper.Create(context);

            return this.RunAsync(document);
        }

        protected abstract Task<TModel[]> RunAsync(XmlDocument document);
    }
}
