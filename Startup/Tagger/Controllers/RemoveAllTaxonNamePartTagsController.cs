namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Remove all taxon-name-part tags.")]
    public class RemoveAllTaxonNamePartTagsController : TaggerControllerFactory, IRemoveAllTaxonNamePartTagsController
    {
        protected override Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() => document.RemoveTaxonNamePartTags());
        }
    }
}