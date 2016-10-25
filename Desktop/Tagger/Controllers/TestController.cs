namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Special.Processors.Contracts;

    [Description("Test.")]
    public class TestController : TaggerControllerFactory, ITestController
    {
        private readonly ITestFeaturesProvider provider;

        public TestController(IDocumentFactory documentFactory, ITestFeaturesProvider provider)
            : base(documentFactory)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        protected override Task Run(IDocument document, IProgramSettings settings)
        {
            return Task.Run(() =>
            {
                this.provider.RenumerateFootNotes(document);
            });
        }
    }
}
