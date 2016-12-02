namespace ProcessingTools.Harvesters.Abbreviations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Abstractions;
    using Contracts.Abbreviations;
    using Contracts.Transformers;
    using Models.Abbreviations;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Contracts.Serialization;

    public class AbbreviationsHarvester : AbstractGenericQueryableXmlHarvester<IAbbreviationModel>, IAbbreviationsHarvester
    {
        private readonly IXmlTransformDeserializer<IGetAbbreviationsTransformer> transformer;

        public AbbreviationsHarvester(
            IXmlContextWrapperProvider contextWrapperProvider,
            IXmlTransformDeserializer<IGetAbbreviationsTransformer> transformer)
            : base(contextWrapperProvider)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            this.transformer = transformer;
        }

        protected override async Task<IQueryable<IAbbreviationModel>> Run(XmlDocument document)
        {
            var items = await this.transformer.Deserialize<AbbreviationsXmlModel>(document.DocumentElement);

            if (items?.Abbreviations == null)
            {
                return null;
            }

            var result = new HashSet<IAbbreviationModel>(items.Abbreviations);

            return result.AsQueryable();
        }
    }
}
