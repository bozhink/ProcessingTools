namespace ProcessingTools.Tagger.Commands.Commands
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Special;

    [System.ComponentModel.Description("Test.")]
    public class TestCommand : ITestCommand
    {
        private readonly ITestFeaturesProvider provider;

        public TestCommand(ITestFeaturesProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
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

            this.provider.RenumerateFootNotes(document);
            return Task.FromResult<object>(true);
        }
    }
}
