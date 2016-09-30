namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;

    [Description("Test.")]
    public class TestController : TaggerControllerFactory, ITestController
    {
        protected override Task Run(IDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
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
