namespace ProcessingTools.Harvesters.Harvesters.Abbreviations
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Models.Harvesters.Abbreviations;
    using ProcessingTools.Harvesters.Abstractions;
    using ProcessingTools.Harvesters.Contracts.Factories;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Abbreviations;
    using ProcessingTools.Harvesters.Models.Abbreviations;
    using ProcessingTools.Xml.Contracts.Serialization;
    using ProcessingTools.Xml.Contracts.Wrappers;

    public class AbbreviationsHarvester : AbstractEnumerableXmlHarvester<IAbbreviationModel>, IAbbreviationsHarvester
    {
        private readonly IXmlTransformDeserializer serializer;
        private readonly IAbbreviationsTransformersFactory transformersFactory;

        public AbbreviationsHarvester(IXmlContextWrapper contextWrapper, IXmlTransformDeserializer serializer, IAbbreviationsTransformersFactory transformersFactory)
            : base(contextWrapper)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        protected override async Task<IAbbreviationModel[]> RunAsync(XmlDocument document)
        {
            var transformer = this.transformersFactory.GetAbbreviationsTransformer();
            var model = await this.serializer.Deserialize<AbbreviationsXmlModel>(transformer, document.DocumentElement);

            return model?.Abbreviations;
        }
    }
}
