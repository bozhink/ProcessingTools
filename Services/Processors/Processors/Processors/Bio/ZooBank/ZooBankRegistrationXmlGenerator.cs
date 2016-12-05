using System;
using System.Threading.Tasks;
using ProcessingTools.Contracts;
using ProcessingTools.Processors.Contracts.Bio.ZooBank;
using ProcessingTools.Processors.Contracts.Transformers;

namespace ProcessingTools.Processors.Processors.Bio.ZooBank
{
    public class ZooBankRegistrationXmlGenerator : IZooBankRegistrationXmlGenerator
    {
        private readonly IZooBankRegistrationXmlTransformer transformer;

        public ZooBankRegistrationXmlGenerator(IZooBankRegistrationXmlTransformer transformer)
        {
            if (transformer == null)
            {
                throw new ArgumentNullException(nameof(transformer));
            }

            this.transformer = transformer;
        }

        public async Task<object> Generate(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var content = await this.transformer.Transform(document.Xml);
            document.Xml = content;

            return true;
        }
    }
}
