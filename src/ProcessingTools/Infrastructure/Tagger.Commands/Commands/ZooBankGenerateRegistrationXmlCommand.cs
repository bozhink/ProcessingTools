namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.ZooBank;

    [Description("Generate XML document for registration in ZooBank.")]
    public class ZooBankGenerateRegistrationXmlCommand : IZooBankGenerateRegistrationXmlCommand
    {
        private readonly IZooBankRegistrationXmlGenerator generator;

        public ZooBankGenerateRegistrationXmlCommand(IZooBankRegistrationXmlGenerator generator)
        {
            this.generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public Task<object> RunAsync(IDocument document, ICommandSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.generator.GenerateAsync(document);
        }
    }
}
