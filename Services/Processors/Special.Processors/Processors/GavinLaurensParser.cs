namespace ProcessingTools.Special.Processors.Processors
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Factories;
    using Contracts.Processors;
    using ProcessingTools.Contracts;

    public class GavinLaurensParser : IGavinLaurensParser
    {
        private readonly ISpecialTransformersFactory transformersFactory;

        public GavinLaurensParser(ISpecialTransformersFactory transformersFactory)
        {
            if (transformersFactory == null)
            {
                throw new ArgumentNullException(nameof(transformersFactory));
            }

            this.transformersFactory = transformersFactory;
        }

        public async Task<object> Parse(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var transformer = this.transformersFactory.GetGavinLaurensTransformer();
            document.Xml = await transformer.Transform(document.Xml);

            return true;
        }
    }
}
