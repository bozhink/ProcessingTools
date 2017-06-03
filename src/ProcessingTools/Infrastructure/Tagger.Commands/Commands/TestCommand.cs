namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Contracts;
    using ProcessingTools.Special.Processors.Contracts.Processors;

    [Description("Test.")]
    public class TestCommand : ITestCommand
    {
        private readonly ITestFeaturesProvider provider;

        public TestCommand(ITestFeaturesProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
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

            this.provider.RenumerateFootNotes(document);
            return Task.FromResult<object>(true);
        }
    }
}
