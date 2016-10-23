namespace ProcessingTools.Harvesters.ExternalLinks
{
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Abstracts;
    using Contracts.ExternalLinks;
    using Models.ExternalLinks;

    using ProcessingTools.Xml.Contracts.Providers;
    using ProcessingTools.Xml.Extensions;

    public class ExternalLinksHarvester : AbstractGenericQueryableXmlHarvester<IExternalLinkModel>, IExternalLinksHarvester
    {
        private const string ExternalLinksXslFilePathKey = "ExternalLinksXslFilePath";

        private string externalLinksXslFileName;

        public ExternalLinksHarvester(IXmlContextWrapperProvider contextWrapperProvider)
            : base(contextWrapperProvider)
        {
            this.externalLinksXslFileName = ConfigurationManager.AppSettings[ExternalLinksXslFilePathKey];
        }

        protected override async Task<IQueryable<IExternalLinkModel>> Run(XmlDocument document)
        {
            var items = await document.DeserializeXslTransformOutput<ExternalLinksModel>(this.externalLinksXslFileName);

            return items.ExternalLinks?.AsQueryable();
        }
    }
}