namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;

    [Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsController : TaggerControllerFactory, IRemoveAllTaxonNamePartTagsController
    {
        public RemoveAllTaxonNamePartTagsController(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected override Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() => document.XmlDocument.RemoveTaxonNamePartTags());
        }
    }
}