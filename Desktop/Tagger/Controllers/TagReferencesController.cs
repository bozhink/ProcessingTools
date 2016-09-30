namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.References;
    using ProcessingTools.Contracts;

    [Description("Tag references.")]
    public class TagReferencesController : TaggerControllerFactory, ITagReferencesController
    {
        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var tagger = new ReferencesTagger(
                document.Xml,
                
                // TODO: unneeded?
                new ReferencesConfiguration
                {
                    ReferencesGetReferencesXmlPath = settings.ReferencesGetReferencesXmlPath
                },
                logger);

            await tagger.Tag();

            document.Xml = tagger.Xml;
        }
    }
}