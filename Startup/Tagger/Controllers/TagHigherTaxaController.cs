namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Data.Miners;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag higher taxa.")]
    public class TagHigherTaxaController : TaggerControllerFactory, ITagHigherTaxaController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var miner = new HigherTaxaDataMiner(settings.WhiteList);
            var tagger = new HigherTaxaTagger(settings.Config, document.OuterXml, miner, settings.BlackList, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml.NormalizeXmlToSystemXml(settings.Config));
        }
    }
}
