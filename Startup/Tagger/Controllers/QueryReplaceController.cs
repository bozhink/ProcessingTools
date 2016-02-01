namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Query replace.")]
    public class QueryReplaceController : TaggerControllerFactory, IQueryReplaceController
    {
        protected override Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() => document.LoadXml(QueryReplace.Replace(
                settings.Config,
                document.OuterXml,
                settings.QueryFileName)));
        }
    }
}