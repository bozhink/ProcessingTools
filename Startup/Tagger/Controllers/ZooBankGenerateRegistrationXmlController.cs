namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.ZooBank;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Generate xml document for registration in ZooBank.")]
    public class ZooBankGenerateRegistrationXmlController : TaggerControllerFactory, IZooBankGenerateRegistrationXmlController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var generator = new ZoobankRegistrationXmlGenerator(settings.Config, document.OuterXml);

            await generator.Generate();

            document.LoadXml(generator.Xml);
        }
    }
}