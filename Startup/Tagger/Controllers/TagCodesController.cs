namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;

    [Description("Tag codes.")]
    public class TagCodesController : TaggerControllerFactory, ITagCodesController
    {
        protected override Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() =>
            {
                var xpathProvider = new XPathProvider(settings.Config);
                var dataProvider = new DataProvider(settings.Config, document.OuterXml, logger);

                Codes codes = new Codes(settings.Config, document.OuterXml, logger);
                codes.TagInstitutions(xpathProvider, dataProvider);
                codes.TagInstitutionalCodes(xpathProvider, dataProvider);

                document.LoadXml(codes.Xml);
            });
        }
    }
}