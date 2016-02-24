namespace ProcessingTools.Harvesters
{
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

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

                var serializer = new XmlSerializer(typeof(ExternalLinksModel));

                var links = document.ApplyXslTransform(this.externalLinksXslFileName).ToXmlReader();

                var items = (ExternalLinksModel)serializer.Deserialize(links);

                return items.ExternalLinks.AsQueryable();
            });
        }
    }
}