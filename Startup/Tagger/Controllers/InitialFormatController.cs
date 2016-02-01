namespace ProcessingTools.MainProgram.Controllers
{
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
            switch (settings.Config.ArticleSchemaType)
            {
                case SchemaType.Nlm:
                    {
                        string xml = document.ApplyXslTransform(settings.Config.NlmInitialFormatXslTransform);
                        var formatter = new NlmInitialFormatter(settings.Config, xml);
                        await formatter.Format();
                        document.LoadXml(formatter.Xml);
                    }

                    break;

                default:
                    {
                        string xml = document.ApplyXslTransform(settings.Config.SystemInitialFormatXslTransform);
                        var formatter = new SystemInitialFormatter(settings.Config, xml);
                        await formatter.Format();
                        document.LoadXml(formatter.Xml);
                    }

                    break;
            }
        }
    }
}
