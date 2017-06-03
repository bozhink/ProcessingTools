namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Processors.Bio.ZooBank;
    using ProcessingTools.Tagger.Commands.Contracts;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;

    [Description("Generate XML document for registration in ZooBank.")]
    public class ZooBankGenerateRegistrationXmlCommand : IZooBankGenerateRegistrationXmlCommand
    {
        private readonly IZooBankRegistrationXmlGenerator generator;

        public ZooBankGenerateRegistrationXmlCommand(IZooBankRegistrationXmlGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public Task<object> Run(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.generator.Generate(document);
        }
    }
}
