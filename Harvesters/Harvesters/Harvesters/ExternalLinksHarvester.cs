namespace ProcessingTools.Harvesters
{
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;
    using ProcessingTools.Infrastructure.Extensions;

    public class ExternalLinksHarvester : IExternalLinksHarvester
    {
        private const string ExternalLinksXslFilePath = "ExternalLinksXslFilePath";

        private string externalLinksXslFileName;

        public ExternalLinksHarvester()
        {
            this.externalLinksXslFileName = ConfigurationManager.AppSettings[ExternalLinksXslFilePath];
        }

        public Task<IQueryable<ExternalLinkModel>> Harvest(XmlNode context)
        {
            return Task.Run(() =>
            {
                XmlDocument document = new XmlDocument
                {
                    PreserveWhitespace = true
                };

                document.LoadXml(Resources.ContextWrapper);

                document.DocumentElement.InnerXml = context.InnerXml;

                var items = document.DeserializeXslTransformOutput<ExternalLinksModel>(this.externalLinksXslFileName);

                return items.ExternalLinks.AsQueryable();
            });
        }
    }
}