namespace ProcessingTools.Special.Processors.Processors
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Special;

    public class GavinLaurensParser : IGavinLaurensParser
    {
        private readonly ISpecialTransformersFactory transformersFactory;

        public GavinLaurensParser(ISpecialTransformersFactory transformersFactory)
        {
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        public async Task<object> ParseAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var transformer = this.transformersFactory.GetGavinLaurensTransformer();
            context.Xml = await transformer.TransformAsync(context.Xml);

            return true;
        }
    }
}
