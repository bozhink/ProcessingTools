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
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        public async Task<object> Parse(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var transformer = this.transformersFactory.GetGavinLaurensTransformer();
            context.Xml = await transformer.Transform(context.Xml).ConfigureAwait(false);

            return true;
        }
    }
}
