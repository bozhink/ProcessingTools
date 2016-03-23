namespace ProcessingTools.MainProgram.Controllers
{
    using System.Configuration;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;
    using ProcessingTools.Infrastructure.Extensions;

    [Description("Generate xml document for registration in ZooBank.")]
    public class ZooBankGenerateRegistrationXmlController : TaggerControllerFactory, IZooBankGenerateRegistrationXmlController
    {
        protected override Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() =>
            {
                string xslFileName = ConfigurationManager.AppSettings["ZoobankNlmXslPath"];
                document.LoadXml(document.ApplyXslTransform(xslFileName));
            });
        }
    }
}