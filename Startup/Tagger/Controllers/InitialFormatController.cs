namespace ProcessingTools.MainProgram.Controllers
{
    using System.Configuration;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.Format;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;
    using ProcessingTools.Infrastructure.Extensions;

    [Description("Initial format.")]
    public class InitialFormatController : TaggerControllerFactory, IInitialFormatController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            string xslFileName = null;
            switch (settings.Config.ArticleSchemaType)
            {
                case SchemaType.Nlm:
                    xslFileName = ConfigurationManager.AppSettings["NlmInitialFormatXslPath"];
                    break;

                default:
                    xslFileName = ConfigurationManager.AppSettings["SystemInitialFormatXslPath"];
                    break;
            }

            string xml = document.ApplyXslTransform(xslFileName);
            var formatter = new NlmInitialFormatter(settings.Config, xml);
            await formatter.Format();
            document.LoadXml(formatter.Xml);
        }
    }
}
