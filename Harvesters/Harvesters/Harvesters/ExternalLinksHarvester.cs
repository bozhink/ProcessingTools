namespace ProcessingTools.Harvesters
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;
    using Models.Contracts;
    using ProcessingTools.Harvesters.Common.Factories;
    using ProcessingTools.Infrastructure.Extensions;

    public class ExternalLinksHarvester : GenericHarvesterFactory<IExternalLinkModel>, IExternalLinksHarvester
    {
        private const string ExternalLinksXslFilePath = "ExternalLinksXslFilePath";

        private string externalLinksXslFileName;

        public ExternalLinksHarvester()
        {
            this.externalLinksXslFileName = ConfigurationManager.AppSettings[ExternalLinksXslFilePath];
        }

        protected override Task<IQueryable<IExternalLinkModel>> Run(XmlDocument document)
        {
            return Task.Run(() =>
            {
                var items = document.DeserializeXslTransformOutput<ExternalLinksModel>(this.externalLinksXslFileName);

                return items.ExternalLinks.AsQueryable<IExternalLinkModel>();
            });
        }
    }
}