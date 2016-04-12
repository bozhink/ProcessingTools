namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.Floats;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag floats.")]
    public class TagFloatsController : TaggerControllerFactory, ITagFloatsController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var tagger = new FloatsTagger(settings.Config, document.OuterXml, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}