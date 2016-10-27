namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Special.Processors.Contracts;

    [Description("Test.")]
    public class TestController : ITestController
    {
        private readonly ITestFeaturesProvider provider;

        public TestController(ITestFeaturesProvider provider)
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

            return Task.Run<object>(() =>
            {
                this.provider.RenumerateFootNotes(document);
                return true;
            });
        }
    }
}
