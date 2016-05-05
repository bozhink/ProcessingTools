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

    public class AbbreviationsHarvester : GenericHarvesterFactory<AbbreviationModel>, IAbbreviationsHarvester
    {
        private const string AbbreviationsXQueryFilePathKey = "AbbreviationsXQueryFilePath";

        private string abbreviationsXQueryFileName;

        public AbbreviationsHarvester()
        {
            this.abbreviationsXQueryFileName = ConfigurationManager.AppSettings[AbbreviationsXQueryFilePathKey];
        }

        protected override async Task<IQueryable<AbbreviationModel>> Run(XmlDocument document)
        {
            var items = await document.DeserializeXQueryTransformOutput<AbbreviationsModel>(this.abbreviationsXQueryFileName);

            return items.Abbreviations.AsQueryable();
        }
    }
}