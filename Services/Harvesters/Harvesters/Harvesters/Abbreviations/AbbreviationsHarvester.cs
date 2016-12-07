namespace ProcessingTools.Harvesters.Harvesters.Abbreviations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Harvesters.Abstractions;
    using ProcessingTools.Harvesters.Contracts.Abbreviations;
    using ProcessingTools.Harvesters.Contracts.Factories;
    using ProcessingTools.Harvesters.Models.Abbreviations;
    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Serialization;

    public class AbbreviationsHarvester : AbstractGenericQueryableXmlHarvester<IAbbreviationModel>, IAbbreviationsHarvester
    {
        private readonly IXmlTransformDeserializer serializer;
        private readonly IAbbreviationsTransformersFactory transformersFactory;

        public AbbreviationsHarvester(
            IXmlContextWrapperProvider contextWrapperProvider,
            IXmlTransformDeserializer serializer,
            IAbbreviationsTransformersFactory transformersFactory)
            : base(contextWrapperProvider)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (transformersFactory == null)
            {
                throw new ArgumentNullException(nameof(transformersFactory));
            }

            this.serializer = serializer;
            this.transformersFactory = transformersFactory;
        }

        protected override async Task<IQueryable<IAbbreviationModel>> Run(XmlDocument document)
        {
            var transformer = this.transformersFactory.GetAbbreviationsTransformer();
            var items = await this.serializer.Deserialize<AbbreviationsXmlModel>(transformer, document.DocumentElement);

            if (items?.Abbreviations == null)
            {
                return null;
            }

            var result = new HashSet<IAbbreviationModel>(items.Abbreviations);

            return result.AsQueryable();
        }
    }
}
