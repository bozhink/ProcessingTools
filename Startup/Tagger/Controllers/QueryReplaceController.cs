namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
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
            int numberOfFileNames = settings.FileNames.Count();

            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The query file name should be set.");
            }

            string queryFileName = settings.FileNames.ElementAt(2);

            return Task.Run(() => document.LoadXml(QueryReplace.Replace(document.OuterXml, queryFileName)));
        }
    }
}