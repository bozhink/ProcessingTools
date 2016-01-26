namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Abbreviations;
    using ProcessingTools.Contracts;

    [Description("Tag abbreviations.")]
    public class TagAbbreviationsController : TaggerControllerFactory, ITagAbbreviationsController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var tagger = new AbbreviationsTagger(settings.Config, document.OuterXml);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}