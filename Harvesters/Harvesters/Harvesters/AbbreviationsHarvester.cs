namespace ProcessingTools.Harvesters
{
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;
    using Models.Contracts;

    using ProcessingTools.Harvesters.Common.Factories;
    using ProcessingTools.Infrastructure.Extensions;

    public class AbbreviationsHarvester : GenericHarvesterFactory<IAbbreviationModel>, IAbbreviationsHarvester
    {
        private const string AbbreviationsXQueryFilePath = "AbbreviationsXQueryFilePath";

        private string abbreviationsXQueryFileName;

        public AbbreviationsHarvester()
        {
            this.abbreviationsXQueryFileName = ConfigurationManager.AppSettings[AbbreviationsXQueryFilePath];
        }

        protected override async Task<IQueryable<IAbbreviationModel>> Run(XmlDocument document)
        {
            var items = await document.DeserializeXQueryTransformOutput<AbbreviationsModel>(this.abbreviationsXQueryFileName);

            return items.Abbreviations.AsQueryable<IAbbreviationModel>();
        }
    }
}