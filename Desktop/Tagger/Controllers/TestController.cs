namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;

    [Description("Test.")]
    public class TestController : TaggerControllerFactory, ITestController
    {
        public TestController(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected override Task Run(IDocument document, ProgramSettings settings)
        {
            return Task.Run(() =>
            {
                var test = new Test(document.Xml);

                test.RenumerateFootNotes();

                document.Xml = test.Xml;
            });
        }
    }
}
