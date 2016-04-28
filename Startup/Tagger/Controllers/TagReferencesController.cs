namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.References;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag references.")]
    public class TagReferencesController : TaggerControllerFactory, ITagReferencesController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var tagger = new ReferencesTagger(
                document.OuterXml,
                new ReferencesConfiguration
                {
                    ReferencesGetReferencesXmlPath = settings.ReferencesGetReferencesXmlPath
                },
                logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}