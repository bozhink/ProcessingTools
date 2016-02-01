namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Tag codes.")]
    public class TagCodesController : TaggerControllerFactory, ITagCodesController
    {
        protected override Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() =>
            {
                var dataProvider = new DataProvider(settings.Config, document.OuterXml, logger);

                Codes codes = new Codes(settings.Config, document.OuterXml, logger);
                codes.TagInstitutions(dataProvider);
                codes.TagInstitutionalCodes(dataProvider);

                document.LoadXml(codes.Xml);
            });
        }
    }
}