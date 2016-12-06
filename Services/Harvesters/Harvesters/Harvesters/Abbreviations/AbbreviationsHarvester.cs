namespace ProcessingTools.Harvesters.Harvesters.Abbreviations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Harvesters.Abstractions;
    using ProcessingTools.Harvesters.Contracts.Abbreviations;
    using ProcessingTools.Harvesters.Contracts.Transformers;
    using ProcessingTools.Harvesters.Models.Abbreviations;
    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Serialization;

    public class AbbreviationsHarvester : AbstractGenericQueryableXmlHarvester<IAbbreviationModel>, IAbbreviationsHarvester
    {
        private readonly IXmlTransformDeserializer serializer;
        private readonly IGetAbbreviationsTransformer transformer;

        public AbbreviationsHarvester(
            IXmlContextWrapperProvider contextWrapperProvider,
            IXmlTransformDeserializer serializer,
            IGetAbbreviationsTransformer transformer)
            : base(contextWrapperProvider)
        {
            if (serializer == null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            this.serializer = serializer;
            this.transformer = transformer;
        }

        protected override async Task<IQueryable<IAbbreviationModel>> Run(XmlDocument document)
        {
            var items = await this.serializer.Deserialize<AbbreviationsXmlModel>(this.transformer, document.DocumentElement);

            if (items?.Abbreviations == null)
            {
                return null;
            }

            var result = new HashSet<IAbbreviationModel>(items.Abbreviations);

            return result.AsQueryable();
        }
    }
}
