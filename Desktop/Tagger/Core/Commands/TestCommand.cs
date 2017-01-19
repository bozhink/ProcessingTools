namespace ProcessingTools.Tagger.Commands
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Commands;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Special.Processors.Contracts;

    [Description("Test.")]
    public class TestCommand : ITestCommand
    {
        private readonly ITestFeaturesProvider provider;

        public TestCommand(ITestFeaturesProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public Task<object> Run(IDocument document, IProgramSettings settings)
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
