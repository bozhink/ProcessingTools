namespace ProcessingTools.Harvesters
{
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;

    using ProcessingTools.Harvesters.Common.Factories;
    using ProcessingTools.Xml.Extensions;

    public class ExternalLinksHarvester : GenericHarvesterFactory<ExternalLinkModel>, IExternalLinksHarvester
    {
        private const string ExternalLinksXslFilePathKey = "ExternalLinksXslFilePath";

        private string externalLinksXslFileName;

        public ExternalLinksHarvester()
        {
            this.externalLinksXslFileName = ConfigurationManager.AppSettings[ExternalLinksXslFilePathKey];
        }

        protected override async Task<IQueryable<ExternalLinkModel>> Run(XmlDocument document)
        {
            var items = await document.DeserializeXslTransformOutput<ExternalLinksModel>(this.externalLinksXslFileName);

            return items.ExternalLinks.AsQueryable();
        }
    }
}